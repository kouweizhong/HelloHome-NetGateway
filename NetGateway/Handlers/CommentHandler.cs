using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Reports;
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

		protected override async Task HandleAsync(CommentReport request, IList<OutgoingMessage> outgoingMessages, CancellationToken cToken)
        {
            Logger.Info(request.Comment);
            await Task.Yield();
        }
    }
}