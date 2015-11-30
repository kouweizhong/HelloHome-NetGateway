using System;

namespace HelloHome.NetGateway.Pipeline
{
	public class UpdateStatisticsModule : IPipelineModule
	{
		#region IPipelineModule implementation
		public IPipelineModule Process (ProcessingContext context, IPipelineModule next)
		{
			if (context.Node != null) {
				context.Node.UpTime = (float)(DateTime.Now - (context.Node.LastStartupTime ?? DateTime.Now)).TotalSeconds;
				context.Node.MaxUpTime = Math.Max (context.Node.UpTime, context.Node.MaxUpTime);
				context.Node.LastMessageReceivedTime = DateTime.Now;
				context.Node.LastRssi = context.IncomingMessage.Rssi;
			}
			return next;
		}
		#endregion
	}
}

