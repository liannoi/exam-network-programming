using System;

namespace Step.Tcp.Infrastructure.Events
{
    public interface INotifyDataReceived<TObject> where TObject : class
    {
        event EventHandler<DataReceivedEventArgs<TObject>> DataReceived;
    }
}
