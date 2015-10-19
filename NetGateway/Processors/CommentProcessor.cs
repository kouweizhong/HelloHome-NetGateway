using System;
using NetHhGateway.Agents.NodeGateway.Domain;
using System.Collections.Generic;

namespace NetHhGateway.Processors
{
	public class CommentProcessor :MessageProcessor<CommentReport>
	{
		public override IList<OutgoingMessage> ProcessInternal (CommentReport message)
		{
			Console.WriteLine (message);
			return null;
		}
	}
}

