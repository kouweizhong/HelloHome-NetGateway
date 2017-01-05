using System;
using System.Threading.Tasks;
using HelloHome.Common;
using HelloHome.Common.Entities;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;
using HelloHome.NetGateway.Queries;
using NLog;

namespace HelloHome.NetGateway.Commands
{
    public interface ICreateNodeCommand : ICommand
    {
        Task<Node> ExecuteAsync(long signature, byte rfId);
    }

    public class CreateNodeCommand : ICreateNodeCommand
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();

        private readonly IHelloHomeDbContext _ctx;
		readonly ITimeProvider _timeProvider;

		public CreateNodeCommand(
			IHelloHomeDbContext ctx, 
			ITimeProvider timeProvider)
        {
			_timeProvider = timeProvider;
			_ctx = ctx;
        }

        public  Task<Node> ExecuteAsync(long signature, byte rfId)
        {
            var node = new Node
            {
                Signature = signature,
                RfAddress = rfId,
                Configuration = new NodeConfiguration
                {
                    Name = "Newly created",
                },
                LatestValues = new LatestValues
                {
                    StartupTime = _timeProvider.UtcNow,
                    MaxUpTime = TimeSpan.Zero,
                }
            };
            _ctx.Nodes.Add(node);
            node.AddLog("CRTD");

            return Task.FromResult(node);
        }
    }
}