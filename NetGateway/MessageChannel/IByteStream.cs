using System.Threading;
using System.Threading.Tasks;

namespace HelloHome.NetGateway.Agents.NodeGateway
{
    public interface IByteStream
    {
        int ReadTimeout { get; set; }
        Task<int> ReadAsync(byte[] buffer, int offset, int cout, CancellationToken cToken);
        Task WriteAsync(byte[] buffer, int offset, int cout, CancellationToken cToken);
    }
}