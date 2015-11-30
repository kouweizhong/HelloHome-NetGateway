using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.Common.Entities;
using System.Collections.Generic;
using log4net;
using HelloHome.NetGateway.Pipeline;
using System.Linq;

namespace HelloHome.NetGateway.Processors
{
	public abstract class MessageProcessor<TMessage> : IMessageProcessor where TMessage : IncomingMessage
	{
		protected readonly ILog Log = LogManager.GetLogger (typeof(MessageProcessor<TMessage>).Name);

		protected Node Node { get; set; }

		#region IMessageProcessor implementation

		public void Process (ProcessingContext ctx)
		{
			try {
				Log.DebugFormat ("{0} will process {1}", this.GetType ().Name, ctx.IncomingMessage);

				Node = ctx.Node;
				var msgOut = ProcessInternal (ctx.IncomingMessage as TMessage);
				if(msgOut != null)
					ctx.Responses.AddRange(msgOut);

				Log.DebugFormat ("{0} has finnish processing {1}", this.GetType ().Name, ctx.IncomingMessage);

			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}
		}

		#endregion

		public abstract IList<OutgoingMessage> ProcessInternal (TMessage message);
	}
}

