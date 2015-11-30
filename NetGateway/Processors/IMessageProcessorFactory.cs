using System;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;

namespace HelloHome.NetGateway.Processors
{
	public interface IMessageProcessorFactory
	{
		IMessageProcessor Create(IncomingMessage forMessage);
		void Release (IMessageProcessor processor);
	}
}

