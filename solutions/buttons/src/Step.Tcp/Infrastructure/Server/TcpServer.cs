// Copyright 2019 Maksym Liannoi
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Step.Tcp.Infrastructure.Base;
using Step.Tcp.Infrastructure.Events;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Step.Tcp.Infrastructure.Server
{
    public class TcpServer : ITcpServer
    {
        protected readonly TcpListener server;

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        public TcpServer(IServerProperties properties)
        {
            server = new TcpListener(
                IPAddress.Parse(properties.ServerIP),
                properties.ServerPort);
            ClientConnected += TcpServer_ClientConnected;
        }

        public void Start()
        {
            try
            {
                server.Start();
            }
            catch (SocketException)
            {
                throw new SingletonTcpServerException("The server can only be run in one instance.");
            }
        }

        public async Task<TListen> ListenAsync<TListen>() where TListen : class
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            DefaultOnClientConnected(client);
            NetworkStream stream = client.GetStream();
            BinaryFormatter formatter = new BinaryFormatter();
            TListen result = (TListen)formatter.Deserialize(stream);
            client.Close();
            return result;
        }

        public TListen Listen<TListen>() where TListen : class
        {
            Listen(out TcpClient client, out TListen result);
            client.Close();
            return result;
        }

        public TListen ListenAndSay<TListen, TResult>(Func<TListen, TResult> say)
        {
            Listen(
                out TcpClient client,
                out NetworkStream stream,
                out BinaryFormatter formatter,
                out TListen listenResult);
            TResult sayObject = say.Invoke(listenResult);
            formatter.Serialize(stream, sayObject);
            client.Close();
            return listenResult;
        }

        public void Stop()
        {
            server?.Stop();
        }

        private void Listen<TListen>(
            out TcpClient client,
            out TListen result) where TListen : class
        {
            client = server.AcceptTcpClient();
            DefaultOnClientConnected(client);
            NetworkStream stream = client.GetStream();
            BinaryFormatter formatter = new BinaryFormatter();
            result = (TListen)formatter.Deserialize(stream);
        }

        private void Listen<TListen>(
            out TcpClient client,
            out NetworkStream stream,
            out BinaryFormatter formatter,
            out TListen listenResult)
        {
            client = server.AcceptTcpClient();
            DefaultOnClientConnected(client);
            stream = client.GetStream();
            formatter = new BinaryFormatter();
            listenResult = (TListen)formatter.Deserialize(stream);
        }

        private void OnClientConnected(ClientConnectedEventArgs args)
        {
            ClientConnected?.Invoke(this, args);
        }

        private void DefaultOnClientConnected(TcpClient client)
        {
            OnClientConnected(new ClientConnectedEventArgs
            {
                ClientIP = client.Client.RemoteEndPoint.ToString()
            });
        }

        private void TcpServer_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Console.WriteLine($"Подключен клиент {e.ClientIP}. Выполнение запроса...");
        }
    }

    [Serializable]
    public class SingletonTcpServerException : Exception
    {
        public SingletonTcpServerException() { }
        public SingletonTcpServerException(string message) : base(message) { }
        public SingletonTcpServerException(string message, Exception inner) : base(message, inner) { }
        protected SingletonTcpServerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
