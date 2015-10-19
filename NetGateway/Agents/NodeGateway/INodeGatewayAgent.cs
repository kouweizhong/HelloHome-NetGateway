using System;
using NetHhGateway.Agents.NodeGateway.Domain;

namespace NetHhGateway.Agents.NodeGateway
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

