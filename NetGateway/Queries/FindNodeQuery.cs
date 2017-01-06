using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HelloHome.Common.Entities;
using HelloHome.Common.Entities.Includes;

namespace HelloHome.NetGateway.Queries
{
    public interface IFindNodeQuery : IQuery
    {
        Node BySignature(long signature, NodeInclude includes = NodeInclude.None);
        Node ByRfId(int rfId, NodeInclude includes = NodeInclude.None);
        Task<Node> BySignatureAsync(long signature, NodeInclude includes = NodeInclude.None);
        Task<Node> ByRfIdAsync(int rfId, NodeInclude includes = NodeInclude.None);
    }

    public class FindNodeQuery : IFindNodeQuery
    {
        private readonly IHelloHomeDbContext _ctx;

        public FindNodeQuery(IHelloHomeDbContext ctx)
        {
            _ctx = ctx;
        }

        public Node BySignature(long signature, NodeInclude includes = NodeInclude.None)
        {
            return _ctx.Nodes
                .Include(includes)
                .FirstOrDefault(x => x.Signature == signature);
        }

        public async Task<Node> BySignatureAsync(long signature, NodeInclude includes = NodeInclude.None)
        {
            return await _ctx.Nodes
                .Include(includes)
                .FirstOrDefaultAsync(x => x.Signature == signature);
        }

        public Node ByRfId(int rfId, NodeInclude includes = NodeInclude.None)
        {
            return _ctx.Nodes
                .Include(includes)
                .FirstOrDefault(x => x.RfAddress == rfId);
        }

        public async Task<Node> ByRfIdAsync(int rfId, NodeInclude includes = NodeInclude.None)
        {
            return await _ctx.Nodes
                .Include(includes)
                .FirstOrDefaultAsync(x => x.RfAddress == rfId);
        }
    }
}