using HelloHome.Common.Entities;
using HelloHome.NetGateway.Commands.RfNodeIdGenerationStrategy;
using HelloHome.NetGateway.Queries;

namespace HelloHome.NetGateway.Commands
{
    public interface ICreateNodeCommand : ICommand
    {
        Node Execute(long signature, byte rfId);
    }

    public class CreateNodeCommand : ICreateNodeCommand
    {
        private readonly IHelloHomeDbContext _ctx;
        private readonly IRfIdGenerationStrategy _rfIdGenerationStrategy;
        private readonly IListRfIdsQuery _listRfIdsQuery;
		readonly ITimeProvider _timeProvider;

		public CreateNodeCommand(
			IHelloHomeDbContext ctx, 
			IRfIdGenerationStrategy rfIdGenerationStrategy, 
			IListRfIdsQuery listRfIdsQuery,
			ITimeProvider timeProvider)
        {
			this._timeProvider = timeProvider;
			_ctx = ctx;
            _rfIdGenerationStrategy = rfIdGenerationStrategy;
            _listRfIdsQuery = listRfIdsQuery;
        }

        public Node Execute(long signature, byte rfId)
        {
			var allIds = _listRfIdsQuery.Execute ();
			if (rfId <=0 || allIds.Contains (rfId))
				rfId = _rfIdGenerationStrategy.FindRfAddress (allIds);
            var node = new Node
            {
                Signature = signature,
                RfId = rfId,
                Configuration = new NodeConfiguration
                {
                    Name = "Newly created",
                },
                LatestValues = new LatestValues
                {

                },
                NodeFacts = new NodeFacts
                {
					LastStartupTime = _timeProvider.UtcNow,
					MaxUpTime = 0,					                               
                }
            };
            _ctx.Nodes.Add(node);

            return node;
        }
    }
}