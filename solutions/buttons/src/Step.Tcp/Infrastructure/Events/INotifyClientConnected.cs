using System;

namespace Step.Tcp.Infrastructure.Events
{
    public interface INotifyClientConnected
    {
        event EventHandler<ClientConnectedEventArgs> ClientConnected;
    }
}
