using System;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using HelloHome.NetGateway.Configuration;
using HelloHome.NetGateway.Agents.NodeGateway.Parsers;
using HelloHome.NetGateway.Agents.NodeGateway.Encoders;
using System.Collections.Concurrent;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using NLog;

namespace HelloHome.NetGateway.Agents.NodeGateway
{

	public class NodeGatewayAgent : INodeGatewayAgent, IDisposable
	{
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		private readonly SerialPort _serial;
		private readonly List<IMessageParser> _parsers;
		private readonly List<IMessageEncoder> _encoders;
		private Task _readingTask;
		private volatile bool _read = false;

		public MessageReceivedHandler OnMessageReceived { get; set;}

		public NodeGatewayAgent (ISerialConfigurationProvider serialConfig, IEnumerable<IMessageEncoder> encoders)
		{
			_encoders = encoders.ToList ();
			_parsers = new List<IMessageParser> { 
				new CommentParser (),
				new NodeStartedParser (),
				new PulseReportParser (),
				new EnvironmentReportParser (),
				new NodeInfoReportParser (),
				new ParseAllParser (),
			};
			_serial = new SerialPort (serialConfig.Port, 115200, Parity.None, 8, StopBits.One);
		}

		#region INodeGatewayAgent implementation


		public void Start ()
		{
			if (!_serial.IsOpen) {
				Logger.Debug("Opening serial port {0}", _serial.PortName);
				_serial.Open ();
				Logger.Debug ("Serial port is now open");
			}
				
			_read = true;
			_readingTask = new Task (ListenSerial);
			_readingTask.ContinueWith (t => {
				if(t.Exception != null)
					Logger.Fatal("Exception : {0}", t.Exception);
			});
			_readingTask.Start ();
		}

		public void Stop ()
		{
			_read = false;
			_readingTask.Wait ();
		}

		public void Send (OutgoingMessage message)
		{
			var encoder = _encoders.FirstOrDefault (_ => _.CanEncode (message));
			if (encoder == null)
				throw new ApplicationException ($"No encoder found for {message.GetType().Name}");
			var bytes = encoder.Encode (message);
			_serial.Write (bytes, 0, bytes.Length);
			_serial.WriteLine ("");
		}

		#endregion

		private void ListenSerial ()
		{
			while (_read) {
				var byteRecord = ReadLine (new byte[] { 0x0D, 0x0A });
				Logger.Debug("Rx: {0}", BitConverter.ToString(byteRecord));

				var parser = _parsers.First (_ => _.CanParse (byteRecord));
				Logger.Debug("Found parser: {0}", parser.GetType().Name);
				var message = parser.Parse (byteRecord);
				Logger.Debug("Message successfully parsed: {0}", message);

				OnMessageReceived?.Invoke(this, message);
			}
		}

		private byte [] ReadLine (byte [] eof) {
			List<byte> bytes = new List<byte> (100);

			int eofMatchCharCount = 0;
			while (_read && eofMatchCharCount < eof.Length) {
				var newByte = _serial.BaseStream.ReadByte ();
				if (newByte == -1) continue;	
				if (newByte == eof [eofMatchCharCount])
					eofMatchCharCount++;
				bytes.Add ((byte)newByte);
			}
			if (_read)
				return bytes.ToArray ();
			Logger.Warn("read was found false.Exiting with empty array");
			return new byte[0];
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			_read = false;
			if (_serial.IsOpen)
				_serial.Close ();
		}

		#endregion
	}
}

