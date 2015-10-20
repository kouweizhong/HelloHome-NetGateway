using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using System.Collections.Generic;

namespace HelloHome.NetGateway.Processors
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

