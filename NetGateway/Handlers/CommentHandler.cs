using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Commands;
using HelloHome.NetGateway.Queries;
using NLog;

namespace HelloHome.NetGateway.Handlers
{
    public class CommentHandler : MessageHandler<CommentReport>
    {		
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
   
		public CommentHandler (IHelloHomeDbContext dbCtx) : base(dbCtx)
		{

		}

		protected override void Handle(CommentReport request, IList<OutgoingMessage> outgoingMessages)
        {
            Logger.Info(request.Comment);
        }
    }
}