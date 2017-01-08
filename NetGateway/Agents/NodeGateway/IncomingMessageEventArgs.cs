using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
	public class IncomingMessageEventArgs : EventArgs
	{
		public IncomingMessageEventArgs (IncomingMessage incomingMessage)
		{
			IncomingMessage = incomingMessage;
		}

		public IncomingMessage IncomingMessage { get; private set; }
		public OutgoingMessage Response { get; set; }
	}
	
}
