using System.Threading.Tasks;

namespace Net.Messages.UdpClient.Infrastructure.Base
{
    public interface IUdpClient
    {
        IUdpMessage LastMessage { get; }

        Task ListenAsync();
        Task SendAsync(IUdpMessage message);
        void ToBroadcast();
        void ToLocalhost();
    }
}