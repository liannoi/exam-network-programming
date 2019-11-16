using System.Net;

namespace Net.Messages.UdpClient.Infrastructure.Base
{
    public interface IClientProperties
    {
        IPAddress Address { get; }
        int Port { get; }

        void ToBroadcast();
        void ToLocalhost();
    }
}