using System.Collections.Generic;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.Common.Entities;
using System;

namespace HelloHome.NetGateway.Pipeline
{
	public class ProcessingContext
	{
		[ThreadStatic]
		private static ProcessingContext _current;

		public ProcessingContext (IncomingMessage incomingMessage)
		{
			IncomingMessage = incomingMessage;
			Responses = new List<OutgoingMessage> ();
			ContextId = Guid.NewGuid ();
		}

		public static ProcessingContext Current { get { return _current; } }

		public Guid ContextId { get; private set; }

		public Node Node { get; set; }

		public IncomingMessage IncomingMessage { get; set; }

		public List<OutgoingMessage> Responses { get; set; }
	}
}

