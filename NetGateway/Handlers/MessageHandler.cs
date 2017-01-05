using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using NLog;
using NLog.Fluent;

namespace HelloHome.NetGateway.Handlers
{
    public interface IMessageHandler
    {
        Task<IList<OutgoingMessage>> HandleAsync(IncomingMessage request);
    }

    public abstract class MessageHandler<T> : IMessageHandler where T : IncomingMessage
    {
		readonly IHelloHomeDbContext _dbCtx;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        protected MessageHandler (IHelloHomeDbContext dbCtx)
		{
			_dbCtx = dbCtx;
		}

        public async Task<IList<OutgoingMessage>> HandleAsync(IncomingMessage request)
        {
            if(request.GetType() != typeof(T))
                throw new ArgumentException($"request of type {request.GetType().Name} cannot be processed by {this.GetType().Name}");
            var outgoigMessages = new List<OutgoingMessage>();
            await HandleAsync((T) request, outgoigMessages);
            Logger.Debug("Will commit on context " + _dbCtx.GetHashCode());
            await _dbCtx.CommitAsync();
            Logger.Debug("Has commit on context " + _dbCtx.GetHashCode());
            return outgoigMessages;
        }

        protected abstract Task HandleAsync(T request, IList<OutgoingMessage> outgoingMessages);
    }
}