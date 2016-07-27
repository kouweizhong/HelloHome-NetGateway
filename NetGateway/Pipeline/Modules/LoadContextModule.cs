using System;
using HelloHome.Common.Entities;
using System.Linq;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using System.Data.Entity;
using log4net;

namespace HelloHome.NetGateway.Pipeline
{
	public class LoadContextModule : IPipelineModule
	{
		static readonly ILog log = LogManager.GetLogger (typeof(LoadContextModule).Name);
		readonly HelloHomeDbContext _dbContext;

		public LoadContextModule (HelloHomeDbContext dbContext)
		{
			_dbContext = dbContext;
			log.Debug ($"Injected with DbContext with hash {dbContext.ContextId}");
		}

		#region IPipelineModule implementation

		public IPipelineModule Process (ProcessingContext context, IPipelineModule next)
		{
			var signedMessage = context.IncomingMessage as ISignedMessage;
			if (signedMessage != null) {
				if (signedMessage.Signature != 0) {
					context.Node = _dbContext.Nodes.SingleOrDefault (_ => _.Signature == signedMessage.Signature);
					if (context.Node == null)
						context.Node = _dbContext.Nodes.SingleOrDefault (_ => _.Signature == signedMessage.OldSignature);
					if (context.Node == null)
						_dbContext.Nodes.Add (context.Node = new Node { Signature = signedMessage.Signature });
				} else {
					context.Node = _dbContext.Nodes.SingleOrDefault (_ => _.Signature == signedMessage.OldSignature);
					if (context.Node == null)
						_dbContext.Nodes.Add (context.Node = new Node { Signature = signedMessage.OldSignature });
				}
				context.Node.RfId = context.IncomingMessage.FromNodeId;
				context.Node.LastStartupTime = DateTime.Now;
			} else {
				context.Node = _dbContext.Nodes.SingleOrDefault (_ => _.RfId == context.IncomingMessage.FromNodeId);
			}

			if (context.Node == null) {
				log.Warn ($"Node could not be identified by NodeId {context.IncomingMessage.FromNodeId}");
				return null;
			}
			return next;
		}

		#endregion
	}
}

