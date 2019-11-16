using Net.Messages.UdpClient.Infrastructure.Base;

namespace Net.Messages.UdpClient.Infrastructure.Client
{
    public class UdpMessage : IUdpMessage
    {
        public string Sender { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return $"Sender: {Sender}, Message: {Message}";
        }
    }
}
