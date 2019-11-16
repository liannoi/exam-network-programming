using System;

namespace Step.Tcp.Infrastructure.Events
{
    public class DataReceivedEventArgs<TObject> : EventArgs where TObject : class
    {
        public TObject Data { get; set; }
    }

    public delegate void DataReceivedEventHandler<TObject>(object sender, DataReceivedEventArgs<TObject> e) where TObject : class;
}
