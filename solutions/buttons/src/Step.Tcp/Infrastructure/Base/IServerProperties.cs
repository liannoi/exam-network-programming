namespace Step.Tcp.Infrastructure.Base
{
    public interface IServerProperties
    {
        string ServerIP { get; set; }
        int ServerPort { get; set; }
    }
}