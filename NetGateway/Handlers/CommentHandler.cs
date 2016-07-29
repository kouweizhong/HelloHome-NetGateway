using System.Collections.Generic;
using System.Diagnostics;
using Castle.Components.DictionaryAdapter;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Logic;
using HelloHome.NetGateway.Queries;
using NLog;

namespace HelloHome.NetGateway.Handlers
{
    public class CommentHandler : MessageHandler<CommentReport>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
   
        protected override void Handle(CommentReport request, IList<OutgoingMessage> outgoingMessages)
        {
            Logger.Info(request.Comment);            
        }
    }
}