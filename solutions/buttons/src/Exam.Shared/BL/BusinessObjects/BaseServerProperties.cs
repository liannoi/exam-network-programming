using Step.Tcp.Infrastructure.DataObjects;

namespace Exam.Shared.BL.BusinessObjects
{
    public class BaseServerProperties
    {
        public ServerProperties ServerProperties { get; private set; }

        public BaseServerProperties()
        {
            ServerProperties = new ServerProperties
            {
                ServerIP = "127.0.0.1",
                ServerPort = 8888
            };
        }
    }
}
