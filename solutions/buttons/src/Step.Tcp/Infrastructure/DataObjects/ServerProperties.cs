using Step.Tcp.Infrastructure.Base;

namespace Step.Tcp.Infrastructure.DataObjects
{
    public class ServerProperties : IServerProperties
    {
        public int ServerPort { get; set; }
        public string ServerIP { get; set; }
    }
}
