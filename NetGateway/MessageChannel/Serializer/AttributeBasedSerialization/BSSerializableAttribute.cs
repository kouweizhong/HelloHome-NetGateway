using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel.Configuration;
using System.Text;
using Castle.MicroKernel.ModelBuilder.Inspectors;
using HelloHome.NetGateway.Commands;

namespace HelloHome.NetGateway.Agents.NodeGateway.Serializer.AttributeBasedSerialization
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BsSerializableAttribute : Attribute
    {
        public BsSerializableAttribute(int messageLenght, byte[] header)
        {
            MessageLenght = messageLenght;
            Header = header;
        }

        public int MessageLenght { get; set; }
        public byte[] Header { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class BsPart : Attribute
    {
        public BsPart(int start, int lenght)
        {
            Start = start;
            Lenght = lenght;
        }

        public int Start { get; set; }
        public int Lenght { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class BsKnownChildAttribute : Attribute
    {
        public BsKnownChildAttribute(Type type)
        {
            if (type.GetCustomAttribute<BsSerializableAttribute>() == null)
                throw new InvalidOperationException("BsKnownTypes should have BsSerializable atttribute on them.");
            Type = type;
        }

        public Type Type { get; set; }
    }

    public class BsSerializer<T> where T : class
    {
        public BsSerializer()
        {
        }

        public byte[] Serialize(T o)
        {

        }

        public T Deserialize(byte[] bytes)
        {
            var knownTypes = typeof(T).GetCustomAttributes<BsKnownChildAttribute>().Select(_ => _.Type).ToList();
            knownTypes.Add(typeof(T));

            var knownTypesSpecsIs = knownTypes
                .Select(_ => new {Type = _, ktAttr = _.GetCustomAttribute<BsSerializableAttribute>(inherit: false)})
                .ToList();
            if(knownTypesSpecsIs.Any(_ => _.ktAttr == null)) throw new Exception("All bsKnownTypes should have BsSerializable attribute on them");

            var knownTypesSpecs = knownTypesSpecsIs
                .Select(_ => new {_.Type, _.ktAttr.MessageLenght, _.ktAttr.Header})
                .ToList();

            var candidates = knownTypesSpecs
                .Where(_ => _.MessageLenght == bytes.Length && HeaderMatch(bytes, _.Header))
                .ToList();

            //If multiple match, only want the one witht the longer headers (greediest match)
            candidates = candidates
                .Where(_ => _.MessageLenght == candidates.Max(x => x.MessageLenght))
                .ToList();

            if (candidates.Count == 0)
                throw new Exception(
                    "Can't deserialize byte array because none of the known type's headers matches it.");
            if (candidates.Count > 1)
                throw new Exception("Can't deserialize byte array because more than one types's header matches it.");

            var theOne = candidates.Single();

            var fieldsSpecs = theOne.Type.GetProperties()
                .Select(_ => new {Field = _, BsAttr = _.GetCustomAttribute<BsPart>(inherit: false)})
                .Where(_ => _.BsAttr != null)
                .Select(_ => new {_.Field, _.BsAttr.Start, _.BsAttr.Lenght})
                .ToList();

            var o = (T) Activator.CreateInstance(theOne.Type);
            foreach (var f in fieldsSpecs)
            {
                object value = Read(bytes, f.Field.PropertyType, f.Start, f.Lenght);
                f.Field.SetValue(o, value);
            }
            return o;
        }

        public bool HeaderMatch(byte[] message, byte[] header)
        {
            if (message.Length < header.Length)
                return false;
            for (int i = 0; i < header.Length; i++)
                if (message[i] != header[i])
                    return false;
            return true;
        }

        public object Read(byte[] bytes, Type type, int start, int lenght)
        {
            if (start + lenght > bytes.Length)
                throw new IndexOutOfRangeException(
                    $"Cannot read {lenght} byte starting ar {start} from an array of {bytes.Length} bytes");

            var bs = bytes.Skip(start).Take(lenght).ToList();

            if (type == typeof(byte))
                if (lenght != 1)
                    throw new IndexOutOfRangeException($"Lenght {lenght} missmatches type {type.Name}");
                else
                    return bytes[start];

            if (type == typeof(Int32))
            {
                bs.AddRange(new byte[] {0, 0, 0});
                return BitConverter.ToInt32(bs.ToArray(), 0);
            }

            if (type == typeof(string))
                return Encoding.ASCII.GetString(bytes.Skip(start).Take(lenght).ToArray());

            throw new NotImplementedException($"Reading {type.Name} from byte[] is not yet implemented.");
        }
    }
}