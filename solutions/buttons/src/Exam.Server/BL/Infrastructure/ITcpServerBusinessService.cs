using System.Threading.Tasks;

namespace Exam.Server.BL.Infrastructure
{
    public interface ITcpServerBusinessService
    {
        void Start();
        void Listen();
        Task ListenAsync();
    }
}