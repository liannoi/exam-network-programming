using Exam.Shared.BL.BusinessObjects;
using System.Threading.Tasks;

namespace Exam.Client.BL.Infrastructure
{
    public interface IButtonBusinessService
    {
        ButtonBusinessObject ButtonBusinessObject { get; }

        void NotifyServer();
        void Say<TObject>(TObject @object) where TObject : class;
        Task SayAsync<TObject>(TObject @object) where TObject : class;
        void ToButton();
        void ToLeft();
        void ToRight();
        void ToUp();
    }
}