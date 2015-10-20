using System;
using HelloHome.NetGateway.Agents.NodeGateway;
using System.Collections.Generic;
using HelloHome.NetGateway.Processors;
using System.Linq;
using System.Threading;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using System.Threading.Tasks;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;

namespace HelloHome.NetGateway
{
	public class GatewayPipeline
	{

		readonly INodeGatewayAgent _nodeGatewayAgent;
		readonly List<IMessageProcessor> _processors;
		Task _mainTask;
		bool running = false;
		HelloHomeDbContext _dbContext;
		IRfNodeIdGenerationStrategy _rfNodeIdGenerationStrategy;

		public GatewayPipeline (INodeGatewayAgent nodeGatewayAgent, IList<IMessageProcessor> processors, HelloHomeDbContext dbContext, IRfNodeIdGenerationStrategy rfNodeIdGenerationStrategy)
		{
			_rfNodeIdGenerationStrategy = rfNodeIdGenerationStrategy;
			_dbContext = dbContext;
			_processors = processors.ToList ();
			_processors.Sort (new ProcessorComparer ());
			_nodeGatewayAgent = nodeGatewayAgent;
		}

		public void StartPipeline ()
		{
			_nodeGatewayAgent.Start ();
			running = true;
			_mainTask = new Task (() => {
				while (running) {
					ProcessNextIfAny();
					Thread.Sleep(10);
				}
			});
			_mainTask.Start ();
		}

		private readonly List<Task> processingTasks = new List<Task> ();
		private bool ProcessNextIfAny() {
			IncomingMessage incomingMessage;
			if (_nodeGatewayAgent.TryNextMessage (out incomingMessage)) {
				processingTasks.Add (Task.Factory.StartNew(() => Process (incomingMessage)));
				return true;
			}
			return false;
		}

		public void Process (IncomingMessage incomingMessage)
		{
			var message = incomingMessage as NodeStartedReport;
			if (message != null) {
				var node = _dbContext.Nodes.FirstOrDefault (_ => _.NodeId == message.Signature);
				if (node == null)
					_dbContext.Nodes.Add (node = new Node { NodeId = (int)message.Signature, Configuration = new NodeConfiguration { }, LatestValues = new NodeLatestValues {} });

				node.RfAddress = message.FromNodeId;
				node.MaxUpTime = Math.Max (node.UpTime, node.MaxUpTime);
				node.UpTime = 0;
				node.LastStartupTime = DateTime.Now;
				node.Version = String.Format ("{0}.{1}", message.Major, message.Minor);
				if(message.NeedNewRfAddress) {
					//Find an available nodeId
					var usedIds = _dbContext.Nodes.Select(_=> (byte)_.RfAddress).Distinct().ToList();
					var newRfAddress = _rfNodeIdGenerationStrategy.FindRfAddress (usedIds);
					_nodeGatewayAgent.Send(new NodeConfigCommand { NewRfAddress = newRfAddress });
				}
				_dbContext.SaveChanges ();
				return;
			}				

			var processors = _processors.Where (_ => _.CanProcess (incomingMessage)).ToList ();
			if (processors.Count == 0)
				Console.WriteLine ("No processor found for {0}", incomingMessage);
			else {
				//RfAdr conflict detection
				var nCount = _dbContext.Nodes.Count(_ => _.RfAddress == incomingMessage.FromNodeId);
				if (nCount > 1) {
					Console.WriteLine ("Multiple node found with rfAddress {0}. Processing stopped", incomingMessage.FromNodeId);
					return;
				}

				var responses = new List<OutgoingMessage> ();
				processors.ForEach (p => {
					var response = p.Process (incomingMessage);
					if (response != null)
						responses.AddRange(response);				
				});
				responses.ForEach (_nodeGatewayAgent.Send);
			}
		}

		public void StopPipeline ()
		{
			running = false;
			_nodeGatewayAgent.Stop();
			Task.WaitAll (_mainTask);
			Task.WaitAll (processingTasks.ToArray());
			while (ProcessNextIfAny ())
				Thread.Sleep (10);
		}
	}
}

