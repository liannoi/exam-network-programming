using Exam.Shared.BL.BusinessObjects;
using Step.Tcp.Infrastructure.Base;
using Step.Tcp.Infrastructure.Events;
using System.Threading.Tasks;

namespace Exam.Server.BL.BusinessServices
{
    public sealed class ButtonListener : TcpServerBusinessService
    {
        public event DataReceivedEventHandler<ButtonBusinessObject> DataReceived;

        public ButtonListener(ITcpServer server) : base(server)
        {
        }

        public override void Listen()
        {
            while (true)
            {
                ButtonBusinessObject businessObject = server.Listen<ButtonBusinessObject>();
                OnDataReceived(new DataReceivedEventArgs<ButtonBusinessObject>
                {
                    Data = businessObject
                });
            }
        }

        public override async Task ListenAsync()
        {
            while (true)
            {
                ButtonBusinessObject businessObject = await server.ListenAsync<ButtonBusinessObject>();
                OnDataReceived(new DataReceivedEventArgs<ButtonBusinessObject>
                {
                    Data = businessObject
                });
            }
        }

        private void OnDataReceived(DataReceivedEventArgs<ButtonBusinessObject> args)
        {
            DataReceived?.Invoke(this, args);
        }
    }
}
