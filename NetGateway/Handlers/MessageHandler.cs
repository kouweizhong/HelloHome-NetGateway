using System;
using System.Collections.Generic;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace HelloHome.NetGateway.Handlers
{
    public abstract class MessageHandler<T> : IMessageHandler where T : IncomingMessage
    {
        public IList<OutgoingMessage> Handle(IncomingMessage request)
        {
            if(request.GetType() != typeof(T))
                throw new ArgumentException($"request of type {request.GetType().Name} cannot be processed by {this.GetType().Name}");
            var outgoigMessages = new List<OutgoingMessage>();
            Handle((T) request, outgoigMessages);
            return outgoigMessages;
        }

        protected abstract void Handle(T request, IList<OutgoingMessage> outgoingMessages);
    }
}