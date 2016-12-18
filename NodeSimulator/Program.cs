using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace NodeSimulator
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			SerialPort s = new SerialPort("/dev/ptyp0");
			s.Open();
			var nodeStartedMessage = new List<byte> { };
			nodeStartedMessage.Add(99); //from nodeId
			nodeStartedMessage.AddRange(BitConverter.GetBytes(-87)); //Rssi
			nodeStartedMessage.Add(0 + 3 << 2);
			nodeStartedMessage.AddRange(new byte[] { 1, 1 }); //Version
			nodeStartedMessage.AddRange(new byte[] { 1, 1, 1, 1 }); //Version
			nodeStartedMessage.AddRange(new byte[] { 1, 1, 1, 1, 1, 1, 1, 1 }); //Version
			nodeStartedMessage.Add(0); //Need new Rf
			var bytes = nodeStartedMessage.ToArray();
			s.Write(bytes, 0, bytes.Length);
		}
	}
}
