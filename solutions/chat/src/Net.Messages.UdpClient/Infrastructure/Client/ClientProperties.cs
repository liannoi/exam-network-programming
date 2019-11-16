using Net.Messages.UdpClient.Infrastructure.Base;
using System.Net;

namespace Net.Messages.UdpClient.Infrastructure.Client
{
    public sealed class ClientProperties : IClientProperties
    {
        public IPAddress Address { get; private set; }
        public int Port => 2222;

        public void ToBroadcast()
        {
            Address = IPAddress.Broadcast;
        }

        public void ToLocalhost()
        {
            Address = IPAddress.Parse("127.0.0.1");
        }
    }
}
