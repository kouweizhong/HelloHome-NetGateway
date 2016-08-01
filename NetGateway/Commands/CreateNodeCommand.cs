using HelloHome.Common.Entities;
using HelloHome.NetGateway.Logic.RfNodeIdGenerationStrategy;
using HelloHome.NetGateway.Queries;

namespace HelloHome.NetGateway.Commands
{
    public interface ICreateNodeCommand : ICommand
    {
        Node Execute(long signature);
    }

    public class CreateNodeCommand : ICreateNodeCommand
    {
        private readonly IHelloHomeDbContext _ctx;
        private readonly IRfIdGenerationStrategy _rfIdGenerationStrategy;
        private readonly IListRfIdsQuery _listRfIdsQuery;

        public CreateNodeCommand(IHelloHomeDbContext ctx, IRfIdGenerationStrategy rfIdGenerationStrategy, IListRfIdsQuery listRfIdsQuery)
        {
            _ctx = ctx;
            _rfIdGenerationStrategy = rfIdGenerationStrategy;
            _listRfIdsQuery = listRfIdsQuery;
        }

        public Node Execute(long signature)
        {
            var node = new Node
            {
                Signature = signature,
                RfId = _rfIdGenerationStrategy.FindRfAddress(_listRfIdsQuery.Execute()),
                Configuration = new NodeConfiguration
                {
                    Name = "Newly created",
                },
                LatestValues = new LatestValues
                {

                },
                NodeFacts = new NodeFacts
                {

                }
            };
            _ctx.Nodes.Add(node);

            return node;
        }
    }
}