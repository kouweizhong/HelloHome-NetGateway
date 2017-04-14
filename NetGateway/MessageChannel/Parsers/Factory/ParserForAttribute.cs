using System;

namespace HelloHome.NetGateway.MessageChannel.Parsers.Factory
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ParserForAttribute : Attribute
    {
        public byte DiscrimatorByte { get; }

        public ParserForAttribute(byte discrimatorByte)
        {
            DiscrimatorByte = discrimatorByte;
        }
    }
}