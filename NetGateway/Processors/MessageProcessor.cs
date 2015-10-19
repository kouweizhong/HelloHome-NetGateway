using System;
using NetHhGateway.Agents.NodeGateway.Domain;
using NetHhGateway.Entities;
using System.Collections.Generic;

namespace NetHhGateway.Processors
{
	public abstract class MessageProcessor<TMessage> : IMessageProcessor where TMessage : IncomingMessage
	{
		#region IMessageProcessor implementation

		public bool CanProcess (IncomingMessage message)
		{
			return message is TMessage;
		}

		public IList<OutgoingMessage> Process (IncomingMessage message)
		{
			try {
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine ("{0} will process {1}", this.GetType ().Name, message);
				Console.ForegroundColor = ConsoleColor.White;

				var response = ProcessInternal (message as TMessage);

				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine ("{0} has finnish processing {1}", this.GetType ().Name, message);
				Console.ForegroundColor = ConsoleColor.White;

				return response;
			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
				return null;
			}
		}

		#endregion

		public abstract IList<OutgoingMessage> ProcessInternal (TMessage message);
	}
}

