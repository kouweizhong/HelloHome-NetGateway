using System;
using HelloHome.NetGateway.Agents.NodeGateway;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests.ScenarioBasedGatewayAgent
{
	public class Scenario : INodeGatewayAgent
	{
		readonly Queue<IncomingMessage> outgoingQueue = new Queue<IncomingMessage> ();
		readonly Queue<OutgoingMessage> incomingQueue = new Queue<OutgoingMessage> ();
		Task rootTask;

		public void Send(IncomingMessage msg){
			var t = new Task(() => 
				outgoingQueue.Enqueue(msg)
			);
			if (rootTask == null)
				rootTask = t;
			else
				rootTask.ContinueWith (_ => t.Start());
		}
			
		public void WaitForReply(Func<OutgoingMessage, bool> test, long timeout = 1000, Action<OutgoingMessage> actionWhenOk = null, Action<OutgoingMessage> actionWhenNok = null)
		{
			var t = new Task (() => {
				var endTime = DateTime.Now.AddMilliseconds (timeout);
				while (incomingQueue.Count == 0 && DateTime.Now < endTime);
				if (incomingQueue.Count == 0)
					throw new Exception ("Timeout waiting for message");
				var msg = incomingQueue.Dequeue ();
				if (test (msg))
				if (actionWhenOk != null)
					actionWhenOk (msg);
				else if (actionWhenNok != null)
					actionWhenNok (msg);
				else
					throw new Exception (String.Format ("WaitFroReplay test failed on {0}", msg));
			});
			if (rootTask == null)
				rootTask = t;
			else
				rootTask.ContinueWith (_ => t.Start());
		}

		public void WaitTillComplete(){
			rootTask.Wait ();
		}
			
		#region INodeGatewayAgent implementation

		public void Start ()
		{
			rootTask.Start ();
		}

		public void Stop ()
		{
			
		}

		public void Send (OutgoingMessage message)
		{
			incomingQueue.Enqueue (message);
		}

		public bool TryNextMessage (out IncomingMessage message)
		{
			message = null;
			if (outgoingQueue.Count > 0)
				message = outgoingQueue.Dequeue ();
			return message != null;
		}

		public IncomingMessage WaitForNextMessage (int milliseconds = 0)
		{
			var endTime = DateTime.Now.AddMilliseconds (milliseconds);
			IncomingMessage msg = null;
			while (!TryNextMessage (out msg) && DateTime.Now < endTime)
				;
			return msg;
		}

		#endregion
	}
}