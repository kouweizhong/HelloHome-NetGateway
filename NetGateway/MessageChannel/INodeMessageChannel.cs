using System.Threading;
using System.Threading.Tasks;
using HelloHome.NetGateway.Agents.NodeGateway.Domain;
using HelloHome.NetGateway.Agents.NodeGateway.Domain.Base;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
    public interface INodeMessageChannel
    {
        Task SendAsync(OutgoingMessage message, CancellationToken cancellationToken);

        /// <summary>
        /// Return a message or null if timout passes without a complete message ending with Eof can be found
        /// </summary>
        /// <param name="cancelationToken"></param>
        /// <returns></returns>
        Task<IncomingMessage> ReadAsync(CancellationToken cancelationToken);
    }
}