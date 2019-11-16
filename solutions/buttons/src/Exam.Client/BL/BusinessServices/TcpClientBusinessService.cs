using Exam.Client.BL.Infrastructure;
using Step.Tcp.Infrastructure.Base;
using System.Threading.Tasks;

namespace Exam.Client.BL.BusinessServices
{
    public abstract class TcpClientBusinessService : ITcpClientBusinessService
    {
        protected ITcpClient client;

        protected TcpClientBusinessService(ITcpClient client)
        {
            this.client = client;
        }

        public abstract void Say<TObject>(TObject @object) where TObject : class;
        public abstract Task SayAsync<TObject>(TObject @object) where TObject : class;
    }
}
