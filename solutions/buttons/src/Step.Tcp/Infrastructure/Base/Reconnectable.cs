using Step.Tcp.Infrastructure.Client;
using System;

namespace Step.Tcp.Infrastructure.Base
{
    public static class Reconnectable
    {
        public static ITcpClient Reconnect(this ITcpClient client)
        {
            GC.SuppressFinalize(client);
            return new TcpClient(client.Properties);
        }
    }
}
