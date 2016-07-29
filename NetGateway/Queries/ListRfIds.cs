using System.Collections.Generic;
using System.Linq;
using HelloHome.Common.Entities;

namespace HelloHome.NetGateway.Queries
{
    public interface IListRfIdsQuery
    {
        IList<byte> Execute();
    }

    public class ListRfIdsQuery : IListRfIdsQuery
    {
        private readonly IHelloHomeDbContext _ctx;

        public ListRfIdsQuery(IHelloHomeDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<byte> Execute()
        {
            return _ctx.Nodes.Select(x => x.RfId).ToList();
        }
    }
}