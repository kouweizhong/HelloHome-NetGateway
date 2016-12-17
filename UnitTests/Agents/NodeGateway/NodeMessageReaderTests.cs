using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Agents.NodeGateway.Encoders;
using HelloHome.NetGateway.Configuration;
using HelloHome.NetGateway.Configuration.AppSettings;
using Moq;
using Xunit;

namespace UnitTests.Agents.NodeGateway
{
    public class TestableNodeMessageReader : NodeMessageReader
    {
        private readonly Queue<byte[]> _byteSeries = new Queue<byte[]>();
        private static readonly Mock<ISerialConfigurationProvider> ConfigMock = new Mock<ISerialConfigurationProvider>();

        public TestableNodeMessageReader(IEnumerable<IMessageEncoder> encoders)
            : base(ConfigMock.Object, encoders)
        {
        }

        protected override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if(_byteSeries.Count == 0)
                return await Task.FromResult(0);
            var bytes = _byteSeries.Dequeue();
            for (var i = 0; i < Math.Min(bytes.Length, count); i++)
                buffer[offset + i] = bytes[i];
            return await Task.FromResult(bytes.Length);
        }

        public void EnqueueBytes(byte[] bytes)
        {
            _byteSeries.Enqueue(bytes);
        }
    }

    public class NodeMessageReaderTests
    {
        private readonly TestableNodeMessageReader _sut;

        public NodeMessageReaderTests()
        {
            _sut = new TestableNodeMessageReader(new List<IMessageEncoder>());

        }

        [Fact]
        public async void no_eof_returns_null()
        {
            _sut.EnqueueBytes(new byte[] { 0x00, 0x10, 0x20 });
            var msg = await _sut.ReadAsync(new CancellationToken());
            Assert.Null(msg);
        }

        [Fact]
        public async void returns_message_when_eof_in_first_baytearray()
        {
            _sut.EnqueueBytes(new byte[] { 0x12, 65, 66, 67, 0x0D, 0x0A });
            var msg = await _sut.ReadAsync(new CancellationToken());
            Assert.NotNull(msg);
        }

        [Fact]
        public async void returns_message_when_eof_next_baytearrays()
        {
            _sut.EnqueueBytes(new byte[] { 0x12, 65, 66, 67});
            _sut.EnqueueBytes(new byte[] { 0x0D, 0x0A });
            var msg = await _sut.ReadAsync(new CancellationToken());
            Assert.NotNull(msg);
        }

        [Fact]
        public async void can_read_node_started_twice()
        {
            _sut.EnqueueBytes(new byte[] { 0x12, 0X00, 0x0A, 0 + 1 << 2, 0x02, 0x01 });
            _sut.EnqueueBytes(new byte[] { 0x0D, 0x0A, 0x12, 0X00 });
            _sut.EnqueueBytes(new byte[] { 0x0A, 0 + 1 << 2, 0x03, 0x01, 0x0D, 0x0A });
            var msg1 = await _sut.ReadAsync(new CancellationToken());
            Assert.True(msg1 is PulseReport);
            Assert.Equal(2, ((PulseReport)msg1).SubNode);
            var msg2 = await _sut.ReadAsync(new CancellationToken());
            Assert.True(msg2 is PulseReport);
            Assert.Equal(3, ((PulseReport)msg2).SubNode);
        }
        [Fact]
        public async void can_read_node_started()
        {
            _sut.EnqueueBytes(new byte[] { 0x12, 0X00, 0x0A, 0 + 1 << 2, 0x02, 0x01 });
            _sut.EnqueueBytes(new byte[] { 0x0D, 0x0A, 40, 41 });
            var msg = await _sut.ReadAsync(new CancellationToken());
            Assert.True(msg is PulseReport);
        }
    }
}