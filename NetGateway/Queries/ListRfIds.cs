using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HelloHome.Common.Entities;

namespace HelloHome.NetGateway.Queries
{
    public interface IListRfIdsQuery : IQuery
    {
        IList<byte> Execute();
        Task<IList<byte>> ExecuteAsync();
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
            return _ctx.Nodes.Select(x => x.RfAddress).ToList();
        }

        public async Task<IList<byte>> ExecuteAsync()
        {
            return await _ctx.Nodes.Select(x => x.RfAddress).ToListAsync();
        }
    }
}