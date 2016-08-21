using System;
using System.Collections.Generic;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace HelloHome.NetGateway.Handlers
{
    public abstract class MessageHandler<T> : IMessageHandler where T : IncomingMessage
    {
		readonly IHelloHomeDbContext _dbCtx;

		public MessageHandler (IHelloHomeDbContext dbCtx)
		{
			this._dbCtx = dbCtx;
		}

		public IList<OutgoingMessage> Handle(IncomingMessage request)
        {
            if(request.GetType() != typeof(T))
                throw new ArgumentException($"request of type {request.GetType().Name} cannot be processed by {this.GetType().Name}");
            var outgoigMessages = new List<OutgoingMessage>();
            Handle((T) request, outgoigMessages);
			_dbCtx.Commit ();
            return outgoigMessages;
        }

        protected abstract void Handle(T request, IList<OutgoingMessage> outgoingMessages);
    }
}