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
using log4net;
using System.Threading;

namespace HelloHome.NetGateway.Agents.NodeGateway
{

	public class NodeGatewayAgent : INodeGatewayAgent, IDisposable
	{
		static readonly ILog log = LogManager.GetLogger(typeof(NodeGatewayAgent).FullName);

		readonly SerialPort _serial;
		List<IMessageParser> _parsers;
		List<IMessageEncoder> _encoders;
		Task readingTask;
		volatile bool read = false;

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
				log.DebugFormat ("Opening serial port {0}", _serial.PortName);
				_serial.Open ();
				log.Debug ("Serial port is now open");
			}
				
			read = true;
			readingTask = new Task (ListenSerial);
			readingTask.ContinueWith (t => {
				if(t.Exception != null)
					log.FatalFormat("Exception : {0}", t.Exception);
			});
			readingTask.Start ();
		}

		public void Stop ()
		{
			read = false;
			readingTask.Wait ();
		}

		public void Send (OutgoingMessage message)
		{
			var encoder = _encoders.FirstOrDefault (_ => _.CanEncode (message));
			if (encoder == null)
				throw new ApplicationException (String.Format ("No encoder found for {0}", message.GetType ().Name));
			var bytes = encoder.Encode (message);
			_serial.Write (bytes, 0, bytes.Length);
			_serial.WriteLine ("");
		}

		#endregion

		private void ListenSerial ()
		{
			while (read) {
				var byteRecord = ReadData (new byte[] { 0x0D, 0x0A });
				log.DebugFormat ("Rx: {0}", BitConverter.ToString(byteRecord));

				var parser = _parsers.First (_ => _.CanParse (byteRecord));
				log.DebugFormat ("Found parser: {0}", parser.GetType().Name);
				var message = parser.Parse (byteRecord);
				log.DebugFormat ("Message successfully parsed: {0}", message);

				if (OnMessageReceived != null)
					OnMessageReceived (this, message);
			}
		}

		private byte[] ReadData (byte[] eof)
		{
			List<byte> bytes = new List<byte> (100);
			int eofMatchCharCount = 0;

			while (eofMatchCharCount < eof.Length) {
				var newByte = _serial.ReadByte();
				if (newByte == eof [eofMatchCharCount])
					eofMatchCharCount++;
				bytes.Add ((byte)newByte);
			}
			if(eofMatchCharCount == eof.Length)
				return bytes.ToArray ();
			log.WarnFormat("bytes found without eof {0} after timeout", BitConverter.ToString(bytes.ToArray()));
			return null;
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			read = false;
			if (_serial.IsOpen)
				_serial.Close ();
		}

		#endregion
	}
}

