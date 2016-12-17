using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Agents.NodeGateway.Encoders;
using HelloHome.NetGateway.Agents.NodeGateway.Parsers;
using HelloHome.NetGateway.Configuration;
using NLog;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
    public class NodeMessageReader
    {
        static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        readonly SerialPort _serial;
        readonly List<IMessageParser> _parsers;
        readonly List<IMessageEncoder> _encoders;

        public NodeMessageReader(ISerialConfigurationProvider serialConfig, IEnumerable<IMessageEncoder> encoders)
        {
            _encoders = encoders.ToList();
            _parsers = new List<IMessageParser>
            {
                new CommentParser(),
                new NodeStartedParser(),
                new PulseReportParser(),
                new EnvironmentReportParser(),
                new NodeInfoReportParser(),
                new ParseAllParser(),
            };
            _serial = new SerialPort(serialConfig.Port, 115200, Parity.None, 8, StopBits.One);
        }

        public async Task SendAsync(OutgoingMessage message, CancellationToken cancellationToken)
        {
            var encoder = _encoders.FirstOrDefault(_ => _.CanEncode(message));
            if (encoder == null)
                throw new ApplicationException($"No encoder found for {message.GetType().Name}");
            var bytes = encoder.Encode(message);
            await _serial.BaseStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            await _serial.BaseStream.WriteAsync(new byte[] {0x0D, 0x0A}, 0, 2, cancellationToken);
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
                var byteCount = await ReadAsync(_buffer, _currentBufferIndex, BufferSize - _currentBufferIndex, cancelationToken);
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

                var parser = _parsers.FirstOrDefault(_ => _.CanParse(msgBytes));
                var msg = parser.Parse(msgBytes);
                for (var i = 0; i < byteCount; i++)
                    _buffer[i] = _buffer[_currentBufferIndex - byteCount + i];
                _currentBufferIndex = byteCount;
                return msg;
            }
            return null;
        }

        protected virtual async Task<int> ReadAsync(byte[] buffer, int offset, int count,
            CancellationToken cancellationToken)
        {
            if (!_serial.IsOpen)
            {
                _serial.Open();
                _serial.BaseStream.ReadTimeout = 10;
            }
            return await _serial.BaseStream.ReadAsync(buffer, offset, count, cancellationToken);
        }
    }
}