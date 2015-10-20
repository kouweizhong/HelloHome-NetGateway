using System;
using System.Threading.Tasks;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
	public class ConsoleNodeGatewayAgent //: INodeGatewayAgent
	{
		#region INodeGatewayAgent implementation

		public void Send (OutgoingMessage message)
		{
			Console.WriteLine ("ConsoleNodeGateway send :" + message);
		}

		public bool TryNextMessage (out IncomingMessage message)
		{
			Console.WriteLine ("(S)tartup / (P)ulse / (E)nvironment / (N)odeInfo");
			var key = Console.ReadKey (true);
			if ('s' == key.KeyChar) {
				Console.Write ("Signature ?:");
				var sig = Console.ReadLine ();
				message = new NodeStartedReport { Signature = Convert.ToInt32("0x"+sig, 16)};
				return true;
			} else if ('x' == key.KeyChar) {
				message = null;
				return false;
			} else {
				Console.WriteLine ("Unknown message");
				message = null;
				return false;
			}
		}

		public void Start ()
		{
		}

		#endregion
	}
}

