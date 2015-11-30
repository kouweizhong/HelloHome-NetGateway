using System.Collections.Generic;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.Common.Entities;
using System;

namespace HelloHome.NetGateway.Pipeline
{
	public class ProcessingContext
	{
		public ProcessingContext (IncomingMessage incomingMessage)
		{
			IncomingMessage = incomingMessage;
			Responses = new List<OutgoingMessage> ();
			_contextId = Guid.NewGuid ();
		}

		[ThreadStatic]
		static Guid _contextId;

		public static Guid ContextId { get { return _contextId; } }

		public Node Node { get; set; }

		public IncomingMessage IncomingMessage { get; set; }

		public List<OutgoingMessage> Responses { get; set; }
	}
}

