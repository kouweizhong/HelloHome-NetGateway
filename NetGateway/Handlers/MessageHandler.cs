using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace HelloHome.NetGateway.Handlers
{
    public interface IMessageHandler
    {
        Task<IList<OutgoingMessage>> HandleAsync(IncomingMessage request);
    }

    public abstract class MessageHandler<T> : IMessageHandler where T : IncomingMessage
    {
		readonly IHelloHomeDbContext _dbCtx;

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
            await _dbCtx.CommitAsync();
            return outgoigMessages;
        }

        protected abstract Task HandleAsync(T request, IList<OutgoingMessage> outgoingMessages);
    }
}