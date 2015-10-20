using System;

namespace NetHhGateway.Agents.NodeGateway
{
	public class FakeNodeGatewayAgent : INodeGatewayAgent
	{
		public FakeNodeGatewayAgent ()
		{
		}

		#region INodeGatewayAgent implementation

		public void Start ()
		{
			
		}

		public void Stop ()
		{
			
		}

		public void Send (NetHhGateway.Agents.NodeGateway.Domain.OutgoingMessage message)
		{
			
		}

		public bool TryNextMessage (out NetHhGateway.Agents.NodeGateway.Domain.IncomingMessage message)
		{
			message = null;
			return false;
		}

		public NetHhGateway.Agents.NodeGateway.Domain.IncomingMessage WaitForNextMessage (int milliseconds = 0)
		{
			return null;
		}

		#endregion
	}
}

