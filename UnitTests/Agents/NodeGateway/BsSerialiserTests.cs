using HelloHome.NetGateway.Agents.NodeGateway.Serializer.AttributeBasedSerialization;
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

        [Fact]
        public void SimplePass()
        {
            var c = _sut.Deserialize(new byte[] {0x02, 0x10, 25});

            Assert.NotNull(c);
            Assert.True(c is Square);
            Assert.Equal(0x10, c.Value);
            Assert.Equal(25, (c as Square).SideLenght);
        }
    }
}