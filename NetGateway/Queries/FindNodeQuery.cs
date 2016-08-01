using System.Linq;
using HelloHome.Common.Entities;

namespace HelloHome.NetGateway.Queries
{
    public interface IFindNodeQuery : IQuery
    {
        Node BySignature(long signature);
        Node ByRfId(int rfId);
    }

    public class FindNodeQuery : IFindNodeQuery
    {
        private readonly IHelloHomeDbContext _ctx;

        public FindNodeQuery(IHelloHomeDbContext ctx)
        {
            _ctx = ctx;
        }

        public Node BySignature(long signature)
        {
            return _ctx.Nodes.FirstOrDefault(x => x.Signature == signature);
        }

        public Node ByRfId(int rfId)
        {
            return _ctx.Nodes.FirstOrDefault(x => x.RfId == rfId);
        }
    }
}