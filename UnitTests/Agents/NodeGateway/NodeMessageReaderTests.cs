using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports;
using HelloHome.NetGateway.Agents.NodeGateway.Encoders;
using HelloHome.NetGateway.Configuration;
using HelloHome.NetGateway.Configuration.AppSettings;
using Moq;
using Xunit;

namespace UnitTests.Agents.NodeGateway
{
    public class ByteStreamMock : IByteStream
    {
        private readonly ConcurrentQueue<byte[]> _byteSeries = new ConcurrentQueue<byte[]>();

        public void EnqueueBytes(byte[] bytes)
        {
            _byteSeries.Enqueue(bytes);
        }

        public async Task<int> ReadAsync(byte[] buffer, int offset, int cout, CancellationToken cToken)
        {
            byte[] bytes;
            if (!_byteSeries.TryDequeue(out bytes))
            {
                await Task.Delay(ReadTimeout, cToken);
                if (!_byteSeries.TryDequeue(out bytes))
                    return 0;
            }
            for (var i = 0; i < Math.Min(bytes.Length, cout); i++)
                buffer[offset + i] = bytes[i];
            return bytes.Length;
        }

        public Task WriteAsync(byte[] buffer, int offset, int cout, CancellationToken cToken)
        {
            throw new NotImplementedException();
        }

        public int ReadTimeout { get; set; }
    }

    public class NodeMessageReaderTests
    {
        private readonly NodeMessageSerialChannel _sut;
        private readonly ByteStreamMock _byteStream;

        public NodeMessageReaderTests()
        {
            _byteStream = new ByteStreamMock();
            _sut = new NodeMessageSerialChannel(_byteStream, new List<IMessageEncoder>());

        }

        [Fact]
        public async void no_eof_returns_null()
        {
            _byteStream.EnqueueBytes(new byte[] { 0x00, 0x10, 0x20 });
            var msg = await _sut.ReadAsync(new CancellationToken());
            Assert.Null(msg);
        }

        [Fact]
        public async void returns_message_when_eof_in_first_baytearray()
        {
            _byteStream.EnqueueBytes(new byte[] { 0x12, 65, 66, 67, 0x0D, 0x0A });
            var msg = await _sut.ReadAsync(new CancellationToken());
            Assert.NotNull(msg);
        }

        [Fact]
        public async void returns_message_when_eof_next_baytearrays()
        {
            _byteStream.EnqueueBytes(new byte[] { 0x12, 65, 66, 67});
            _byteStream.EnqueueBytes(new byte[] { 0x0D, 0x0A });
            var msg = await _sut.ReadAsync(new CancellationToken());
            Assert.NotNull(msg);
        }

        [Fact]
        public async void can_read_node_started()
        {
            _byteStream.EnqueueBytes(new byte[] { 0x12, 0X00, 0x0A, 0 + 1 << 2, 0x02, 0x01 });
            _byteStream.EnqueueBytes(new byte[] { 0x0D, 0x0A, 40, 41 });
            var msg = await _sut.ReadAsync(new CancellationToken());
            Assert.True(msg is PulseReport);
        }

        [Fact]
        public async void can_read_node_started_twice()
        {
            _byteStream.EnqueueBytes(new byte[] { 0x12, 0X00, 0x0A, 0 + 1 << 2, 0x02, 0x01 });
            _byteStream.EnqueueBytes(new byte[] { 0x0D, 0x0A, 0x12, 0X00 });
            _byteStream.EnqueueBytes(new byte[] { 0x0A, 0 + 1 << 2, 0x03, 0x01, 0x0D, 0x0A });
            var msg1 = await _sut.ReadAsync(new CancellationToken());
            Assert.True(msg1 is PulseReport);
            Assert.Equal(2, ((PulseReport)msg1).SubNode);
            var msg2 = await _sut.ReadAsync(new CancellationToken());
            Assert.True(msg2 is PulseReport);
            Assert.Equal(3, ((PulseReport)msg2).SubNode);
        }
    }
}