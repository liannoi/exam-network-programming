using Step.Tcp.Infrastructure.Events;
using System;
using System.Threading.Tasks;

namespace Step.Tcp.Infrastructure.Base
{
    public interface ITcpServer
    {
        event ClientConnectedEventHandler ClientConnected;

        Task<TListen> ListenAsync<TListen>() where TListen : class;
        TListen Listen<TListen>() where TListen : class;
        TListen ListenAndSay<TListen, TResult>(Func<TListen, TResult> say);
        void Start();
        void Stop();
    }
}