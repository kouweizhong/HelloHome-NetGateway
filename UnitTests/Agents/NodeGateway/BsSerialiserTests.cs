using System;
using HelloHome.NetGateway.MessageChannel.Serializer.AttributeBasedSerialization;
using Xunit;

namespace UnitTests.Agents.NodeGateway
{
    public class BsSerialiserTests
    {
        private readonly BsSerializer<Geometry> _sut;

        [BsSerializable(2, new byte[] { 0x01 })]
        [BsKnownChild(typeof(Square))]
        public class Geometry
        {
            [BsPart(1, 1)]
            public byte Value { get; set; }
        }

        [BsSerializable(3, new byte[] { 0x02 })]
        public class Square : Geometry
        {
            [BsPart(2, 1)]
            public int SideLenght { get; set; }
        }

        public BsSerialiserTests()
        {
            _sut = new BsSerializer<Geometry>();
        }

        [Fact(Skip = "NotImpl")]
        public void KnownTypeMustInheritsDeclaringType()
        {
        }

        [Fact]
        public void CanSerializeByte()
        {
            var g = new Geometry();
            var bytes = _sut.Serialize(g);
            Assert.Equal(0x01, bytes[0]);
        }
    }
}