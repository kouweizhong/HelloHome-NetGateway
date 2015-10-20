using System;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
	public class IncomingMessageEventArgs : EventArgs
	{
		public IncomingMessageEventArgs (Domain.IncomingMessage incomingMessage)
		{
			IncomingMessage = incomingMessage;
		}

		public Domain.IncomingMessage IncomingMessage { get; private set; }
		public Domain.OutgoingMessage Response { get; set; }
	}
	
}
