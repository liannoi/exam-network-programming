using System;

namespace Step.Tcp.Infrastructure.Events
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public string ClientIP { get; set; }
    }

    public delegate void ClientConnectedEventHandler(object sender, ClientConnectedEventArgs e);
}
