namespace Net.Messages.UdpClient.Infrastructure.Base
{
    public interface IUdpMessage
    {
        string Message { get; set; }
        string Sender { get; set; }

        string ToString();
    }
}