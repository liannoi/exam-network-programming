using System.Threading.Tasks;

namespace Exam.Client.BL.Infrastructure
{
    public interface ITcpClientBusinessService
    {
        void Say<TObject>(TObject @object) where TObject : class;
        Task SayAsync<TObject>(TObject @object) where TObject : class;
    }
}