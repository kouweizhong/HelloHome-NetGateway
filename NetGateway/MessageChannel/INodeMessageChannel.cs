using System.Threading;
using System.Threading.Tasks;
using HelloHome.NetGateway.MessageChannel.Domain.Base;

namespace HelloHome.NetGateway.MessageChannel
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