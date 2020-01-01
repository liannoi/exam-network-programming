// Copyright 2020 Maksym Liannoi
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
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Step.Tcp.Infrastructure.Client
{
    public class TcpClient : ITcpClient
    {
        private readonly System.Net.Sockets.TcpClient client;

        public IServerProperties Properties { get; set; }

        public TcpClient(IServerProperties properties)
        {
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
            client = new System.Net.Sockets.TcpClient();
            try
            {
                client.Connect(properties.ServerIP, properties.ServerPort);
            }
            catch (SocketException)
            {
                throw new ServerNotAvailableException("Server is not available.");
            }
        }

        public void Say<TSay>(TSay @object)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, @object);
            }
            catch (IOException)
            {
                throw new ServerNotAvailableException("Server is not available.");
            }
        }

        public async Task SayAsync<TSay>(TSay say)
        {
            await Task.Factory.StartNew(() =>
            {
                Say(say);
            }).ConfigureAwait(false);
        }

        public TListen SayAndListen<TListen, TSay>(TSay say) where TListen : class
        {
            Say(say);
            Listen(out NetworkStream stream, out BinaryFormatter formatter);
            return (TListen)formatter.Deserialize(stream);
        }

        private void Listen(out NetworkStream stream, out BinaryFormatter formatter)
        {
            stream = client.GetStream();
            formatter = new BinaryFormatter();
        }
    }

    [Serializable]
    public class ServerNotAvailableException : Exception
    {
        public ServerNotAvailableException() { }
        public ServerNotAvailableException(string message) : base(message) { }
        public ServerNotAvailableException(string message, Exception inner) : base(message, inner) { }
        protected ServerNotAvailableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
