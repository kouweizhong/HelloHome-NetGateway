using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
	public interface INodeGatewayAgent
	{
		void Start ();

		void Stop ();

		void Send (OutgoingMessage message);

		bool TryNextMessage (out IncomingMessage message);

		IncomingMessage WaitForNextMessage (int milliseconds = 0);
	}
}

