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

namespace HelloHome.NetGateway.Agents.NodeGateway
{

	public class NodeGatewayAgent : INodeGatewayAgent, IDisposable
	{
		readonly SerialPort _serial;
		List<IMessageParser> _parsers;
		List<IMessageEncoder> _encoders;
		readonly ConcurrentQueue<IncomingMessage> _incomingMessages = new ConcurrentQueue<IncomingMessage> ();

		volatile bool read = false;

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
			if (!_serial.IsOpen)
				_serial.Open ();
				
			read = true;
			var readingTask = new Task (ReadFromSerial);
			readingTask.Start ();
		}

		public void Stop ()
		{
			read = false;
		}

		public bool TryNextMessage (out IncomingMessage message)
		{
			return _incomingMessages.TryDequeue (out message);
		}

		public IncomingMessage WaitForNextMessage (int milliseconds = 0)
		{
			var endTime = DateTime.Now.AddMilliseconds (milliseconds);
			IncomingMessage inMsg = null;
			do {
				_incomingMessages.TryDequeue (out inMsg);
			} while(inMsg == null && DateTime.Now < endTime);
			return inMsg;
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

		private void ReadFromSerial ()
		{
			while (read) {
				var byteRecord = ReadData (new byte[] { 0x0D, 0x0A });

				var parser = _parsers.First (_ => _.CanParse (byteRecord));
				var message = parser.Parse (byteRecord);
				_incomingMessages.Enqueue (message);
			}
		}

		private byte[] ReadData (byte[] eof)
		{
			List<byte> bytes = new List<byte> (100);
			int eofMatchCharCount = 0;

			while (eofMatchCharCount < eof.Length) {
				var newByte = _serial.ReadByte ();
				if (newByte == eof [eofMatchCharCount])
					eofMatchCharCount++;
				bytes.Add ((byte)newByte);
			}
			return bytes.ToArray ();
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

