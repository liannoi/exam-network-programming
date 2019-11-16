using Net.Messages.UdpClient.Infrastructure.Base;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Net.Messages.UdpClient.Infrastructure.Client
{
    public class UdpClient : IUdpClient
    {
        private readonly System.Net.Sockets.UdpClient udpclient;
        private readonly IPAddress multicastaddress;
        private readonly IPEndPoint remoteep;
        private readonly IClientProperties properties;

        public IUdpMessage LastMessage { get; private set; }

        public UdpClient(IClientProperties properties, IUdpMessage lastMessage)
        {
            this.properties = properties;
            LastMessage = lastMessage;
            properties.ToLocalhost();
            multicastaddress = properties.Address;
            udpclient = new System.Net.Sockets.UdpClient();
            remoteep = new IPEndPoint(multicastaddress, properties.Port);
        }

        public void ToLocalhost()
        {
            properties.ToLocalhost();
        }

        public void ToBroadcast()
        {
            properties.ToBroadcast();
        }

        public async Task SendAsync(IUdpMessage message)
        {
            LastMessage.Sender = message.Sender;
            byte[] buffer = Encoding.UTF8.GetBytes(message.Message);
            await udpclient.SendAsync(buffer, buffer.Length, remoteep);
        }

        public async Task ListenAsync()
        {
            using (System.Net.Sockets.UdpClient client = new System.Net.Sockets.UdpClient())
            {
                client.ExclusiveAddressUse = false;
                IPEndPoint local = new IPEndPoint(IPAddress.Any, properties.Port);
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                client.Client.Bind(local);
                while (true)
                {
                    byte[] data = (await client.ReceiveAsync()).Buffer;
                    LastMessage.Message = Encoding.UTF8.GetString(data);
                }
            }
        }
    }
}
