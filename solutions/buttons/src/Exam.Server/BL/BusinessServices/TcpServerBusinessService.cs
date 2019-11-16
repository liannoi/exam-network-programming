using Exam.Server.BL.Infrastructure;
using Step.Tcp.Infrastructure.Base;
using System.Threading.Tasks;

namespace Exam.Server.BL.BusinessServices
{
    public abstract class TcpServerBusinessService : ITcpServerBusinessService
    {
        protected readonly ITcpServer server;

        protected TcpServerBusinessService(ITcpServer server)
        {
            this.server = server;
        }

        public void Start()
        {
            server.Start();
        }

        public abstract void Listen();

        public abstract Task ListenAsync();
    }
}
