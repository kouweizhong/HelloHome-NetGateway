using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.NetGateway.MessageChannel.Domain.Base;
using HelloHome.NetGateway.MessageChannel.Encoders.Factory;
using HelloHome.NetGateway.MessageChannel.Parsers;
using HelloHome.NetGateway.MessageChannel.Parsers.Factory;
using NLog;

namespace HelloHome.NetGateway.MessageChannel
{
    public class NodeMessageSerialChannel : INodeMessageChannel
    {
        private readonly IByteStream _byteStream;
        private readonly IMessageParserFactory _parserFactory;
        private readonly IEncoderFactory _encoderFactory;
        static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public NodeMessageSerialChannel(IByteStream byteStream, IMessageParserFactory parserFactory, IEncoderFactory encoderFactory)
        {
            _byteStream = byteStream;
            _parserFactory = parserFactory;
            _encoderFactory = encoderFactory;
        }

        public async Task SendAsync(OutgoingMessage message, CancellationToken cancellationToken)
        {
            var encoder = _encoderFactory.Create(message);
            var bytes = encoder.Encode(message);
            await _byteStream.WriteAsync(new byte[] {message.ToNodeId}, 0, 1, cancellationToken);
            await _byteStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
            await _byteStream.WriteAsync(new byte[] {0x0D, 0x0A}, 0, 2, cancellationToken);
        }

        private const int BufferSize = 100;
        readonly byte[] _buffer = new byte[BufferSize];
        int _currentBufferIndex = 0;

        /// <summary>
        /// Return a message or null if timout passes without a complete message ending with Eof can be found
        /// </summary>
        /// <param name="cancelationToken"></param>
        /// <returns></returns>
        public async Task<IncomingMessage> ReadAsync(CancellationToken cancelationToken)
        {
            // at start, some left overs might still be at the begining of the buffer,
            // currentIndex can therefore be greater than 0
            byte[] eof = {0x0D, 0x0A};

            var eofMatchCharCount = 0;
            while (eofMatchCharCount < eof.Length)
            {
                var byteCount = await _byteStream.ReadAsync(_buffer, _currentBufferIndex, BufferSize - _currentBufferIndex, cancelationToken);
                if (byteCount <= 0)
                    return null;

                _currentBufferIndex += byteCount;
                while (eofMatchCharCount < eof.Length && byteCount > 0)
                {
                    if (_buffer[_currentBufferIndex - byteCount] == eof[eofMatchCharCount])
                        eofMatchCharCount++;
                    else
                        eofMatchCharCount = 0;
                    byteCount--;
                }

                if (eofMatchCharCount != eof.Length)
                    continue;

                var msgBytes = new byte[_currentBufferIndex - byteCount];
                for (var i = 0; i < _currentBufferIndex - byteCount; i++)
                    msgBytes[i] = _buffer[i];

                var parser = _parserFactory.Create(msgBytes);
                var msg = parser.Parse(msgBytes);
                for (var i = 0; i < byteCount; i++)
                    _buffer[i] = _buffer[_currentBufferIndex - byteCount + i];
                _currentBufferIndex = byteCount;
                return msg;
            }
            return null;
        }
    }
}