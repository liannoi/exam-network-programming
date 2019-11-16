using System.Threading.Tasks;

namespace Step.Tcp.Infrastructure.Base
{
    public interface ITcpClient
    {
        IServerProperties Properties { get; set; }

        void Say<TSay>(TSay say);
        Task SayAsync<TSay>(TSay say);
        TListen SayAndListen<TListen, TSay>(TSay say) where TListen : class;
    }
}