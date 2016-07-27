using System;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
	public class FakeNodeGatewayAgent : INodeGatewayAgent
	{
		#region INodeGatewayAgent implementation

		public MessageReceivedHandler OnMessageReceived { get; set; }

		public void Start ()
		{
			
		}

		public void Stop ()
		{
			
		}

		public void Send (Domain.OutgoingMessage message)
		{
			
		}

		#endregion

		public void SimulateIncomingMessage (Domain.IncomingMessage message)
		{
			OnMessageReceived (this, message);
		}
	}
}

