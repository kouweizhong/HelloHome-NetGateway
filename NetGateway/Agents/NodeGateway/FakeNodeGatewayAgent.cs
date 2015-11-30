using System;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
	public class FakeNodeGatewayAgent : INodeGatewayAgent
	{
		public FakeNodeGatewayAgent ()
		{
		}

		#region INodeGatewayAgent implementation

		public MessageReceivedHandler OnMessageReceived {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public void Start ()
		{
			
		}

		public void Stop ()
		{
			
		}

		public void Send (HelloHome.NetGateway.Agents.NodeGateway.Domain.OutgoingMessage message)
		{
			
		}

		public bool TryNextMessage (out HelloHome.NetGateway.Agents.NodeGateway.Domain.IncomingMessage message)
		{
			message = null;
			return false;
		}

		public HelloHome.NetGateway.Agents.NodeGateway.Domain.IncomingMessage WaitForNextMessage (int milliseconds = 0)
		{
			return null;
		}

		#endregion
	}
}

