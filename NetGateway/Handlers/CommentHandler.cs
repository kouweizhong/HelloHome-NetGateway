using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.MessageChannel.Domain.Base;
using HelloHome.NetGateway.MessageChannel.Domain.Reports;
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