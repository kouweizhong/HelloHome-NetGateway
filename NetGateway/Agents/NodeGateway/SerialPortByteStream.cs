using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.NetGateway.Configuration;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
    public class SerialPortByteStream : IByteStream, IDisposable
    {
        private readonly Lazy<SerialPort> _serial;

        public SerialPortByteStream(ISerialConfigurationProvider serialConfig)
        {
            _serial = new Lazy<SerialPort>(() =>
                {
                    var s = new SerialPort(serialConfig.Port, 115200, Parity.None, 8, StopBits.One);
                    s.ReadTimeout = 5000;
                    s.Open();
                    return s;
                });
        }

        public async Task<int> ReadAsync(byte[] buffer, int offset, int cout, CancellationToken cToken)
        {
            try
            {
                return await _serial.Value.BaseStream.ReadAsync(buffer, offset, cout, cToken);

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public async Task WriteAsync(byte[] buffer, int offset, int cout, CancellationToken cToken)
        {
            await _serial.Value.BaseStream.WriteAsync(buffer, offset, cout, cToken);
        }


        public void Dispose()
        {
            if (!_serial.IsValueCreated)
                return;
            if (_serial.Value.IsOpen)
                _serial.Value.Close();
            _serial.Value.Dispose();
        }

        public int ReadTimeout {
            get { return _serial.Value.ReadTimeout; }
            set
            {
                if(_serial.IsValueCreated)
                    throw new Exception("Cannot set readTimeout once the serial is in use");
                _serial.Value.ReadTimeout = value;
            }
        }
    }
}