using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.NetGateway.MessageChannel;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;
using HelloHome.NetGateway.MessageChannel.Encoders.Factory;
using HelloHome.NetGateway.MessageChannel.Parsers;
using HelloHome.NetGateway.MessageChannel.Parsers.Factory;
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
        private readonly Mock<IMessageParserFactory> _parserFactory;
        private readonly Mock<IEncoderFactory> _encoderFactory;

        public NodeMessageReaderTests()
        {
            _encoderFactory = new Mock<IEncoderFactory>();
            _parserFactory = new Mock<IMessageParserFactory>();
            _byteStream = new ByteStreamMock();
            _sut = new NodeMessageSerialChannel(_byteStream, _parserFactory.Object, _encoderFactory.Object);

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
            var bytes = new byte[] { 0x12, 65, 66, 67, 0x0D, 0x0A };
            _byteStream.EnqueueBytes(bytes);
            _parserFactory.Setup(x => x.Create(bytes)).Returns(new ParseAllParser());
            var msg = await _sut.ReadAsync(new CancellationToken());
            Assert.NotNull(msg);
        }

        [Fact]
        public async void returns_message_when_eof_next_baytearrays()
        {
            var bytes = new byte[] { 0x12, 65, 66, 67, 0x0D, 0x0A };
            _byteStream.EnqueueBytes(bytes.Take(4).ToArray());
            _byteStream.EnqueueBytes(bytes.Skip(4).ToArray());
            _parserFactory.Setup(x => x.Create(bytes)).Returns(new ParseAllParser());
            var msg = await _sut.ReadAsync(new CancellationToken());
            Assert.NotNull(msg);
        }

        [Fact]
        public async void can_read_node_started()
        {
            _byteStream.EnqueueBytes(new byte[] { 0x12, 0X00, 0x0A, 0 + (1 << 2), 0x02, 0x01 });
            _byteStream.EnqueueBytes(new byte[] { 0x0D, 0x0A, 40, 41 });
            _parserFactory.Setup(x => x.Create(new byte[] {0x12, 0X00, 0x0A, 0 + (1 << 2), 0x02, 0x01, 0x0D, 0x0A}))
                .Returns(new PulseReportParser());
            var msg = await _sut.ReadAsync(new CancellationToken());
            Assert.True(msg is PulseReport);
        }

        [Fact]
        public async void can_read_node_started_twice()
        {
            _byteStream.EnqueueBytes(new byte[] { 0x12, 0X00, 0x0A, 0 + 1 << 2, 0x02, 0x01 });
            _byteStream.EnqueueBytes(new byte[] { 0x0D, 0x0A, 0x12, 0X00 });
            _byteStream.EnqueueBytes(new byte[] { 0x0A, 0 + 1 << 2, 0x03, 0x01, 0x0D, 0x0A });
            _parserFactory.Setup(x => x.Create(new byte[] {0x12, 0X00, 0x0A, 0 + (1 << 2),0x02, 0x01, 0x0D, 0x0A})).Returns(new PulseReportParser());
            _parserFactory.Setup(x => x.Create(new byte[] {0x12, 0X00, 0x0A, 0 + (1 << 2),0x03, 0x01, 0x0D, 0x0A})).Returns(new PulseReportParser());
            var msg1 = await _sut.ReadAsync(new CancellationToken());
            Assert.True(msg1 is PulseReport);
            Assert.Equal(2, ((PulseReport)msg1).SubNode);
            var msg2 = await _sut.ReadAsync(new CancellationToken());
            Assert.True(msg2 is PulseReport);
            Assert.Equal(3, ((PulseReport)msg2).SubNode);
        }
    }
}