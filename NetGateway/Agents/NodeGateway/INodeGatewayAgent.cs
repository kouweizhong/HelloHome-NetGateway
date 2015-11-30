using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
	public delegate void MessageReceivedHandler(object sender, IncomingMessage message);

	public interface INodeGatewayAgent
	{
		void Start ();

		void Stop ();

		void Send (OutgoingMessage message);

		MessageReceivedHandler OnMessageReceived { get; set;}

		//bool TryNextMessage (out IncomingMessage message);

		//IncomingMessage WaitForNextMessage (int milliseconds = 0);
	}
}

