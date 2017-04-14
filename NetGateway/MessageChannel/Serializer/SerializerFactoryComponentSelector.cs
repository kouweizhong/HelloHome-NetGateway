using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core.Internal;
using Castle.Facilities.TypedFactory;

namespace HelloHome.NetGateway.MessageChannel.Serializer
{
    public class SerializerFactoryComponentSelector : DefaultTypedFactoryComponentSelector
    {
        readonly ConcurrentDictionary<Type, Type> _typeToSerializerCache;
        readonly ConcurrentDictionary<byte, Type> _byteToSerializerCache;

        public SerializerFactoryComponentSelector()
        {
            var serializerTypes = typeof(IMessageSerializer).Assembly.GetTypes()
                .Where(x => typeof(IMessageSerializer).IsAssignableFrom(x) && x.HasAttribute<SerializerForAttribute>())
                .Select(x => new {serializerType = x, attr = x.GetCustomAttribute<SerializerForAttribute>()})
                .ToList();

            _typeToSerializerCache = new ConcurrentDictionary<Type, Type>(serializerTypes.Select(x => new KeyValuePair<Type, Type>(x.attr.MessageType, x.serializerType)));
            _byteToSerializerCache = new ConcurrentDictionary<byte, Type>(serializerTypes.Select(x => new KeyValuePair<byte, Type>(x.attr.Identifier, x.serializerType)));

        }

        protected override Type GetComponentType(MethodInfo method, object[] arguments)
        {
            return base.GetComponentType(method, arguments);
        }
    }
}