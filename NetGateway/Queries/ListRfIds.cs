using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelloHome.Common.Entities;

namespace HelloHome.NetGateway.Queries
{
    public interface IListRfIdsQuery : IQuery
    {
        Task<IList<byte>> ExecuteAsync();
        Task<IList<byte>> ExecuteAsync(byte network, CancellationToken cToken);
    }

    public class ListRfIdsQuery : IListRfIdsQuery
    {
        private readonly IHelloHomeDbContext _ctx;

        public ListRfIdsQuery(IHelloHomeDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IList<byte>> ExecuteAsync()
        {
            return await _ctx.Nodes.Select(x => x.RfAddress).ToListAsync();
        }

        public async Task<IList<byte>> ExecuteAsync(byte network, CancellationToken cToken)
        {
            return await _ctx.Nodes
                .Where(x => x.RfNetwork == network)
                .Select(x => x.RfAddress)
                .ToListAsync(cToken);
        }
    }
}