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

using Net.Messages.UdpClient.Infrastructure.Base;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Net.Messages.UdpClient.Infrastructure.Client
{
    public class UdpClient : IUdpClient
    {
        private readonly System.Net.Sockets.UdpClient udpclient;
        private readonly IPEndPoint remoteep;
        private readonly IClientProperties properties;

        public IUdpMessage LastMessage { get; private set; }

        public UdpClient(IClientProperties properties, IUdpMessage lastMessage)
        {
            this.properties = properties;
            LastMessage = lastMessage;
            properties.ToLocalhost();
            udpclient = new System.Net.Sockets.UdpClient();
            remoteep = new IPEndPoint(properties.Address, properties.Port);
        }

        public void ToLocalhost()
        {
            properties?.ToLocalhost();
        }

        public void ToBroadcast()
        {
            properties?.ToBroadcast();
        }

        public async Task SendAsync(IUdpMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            LastMessage.Sender = message.Sender;
            byte[] buffer = Encoding.UTF8.GetBytes(message.Message);
            await udpclient.SendAsync(buffer, buffer.Length, remoteep);
        }

        public async Task ListenAsync()
        {
            using (System.Net.Sockets.UdpClient client = new System.Net.Sockets.UdpClient())
            {
                client.ExclusiveAddressUse = false;
                IPEndPoint local = new IPEndPoint(IPAddress.Any, properties.Port);
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                client.Client.Bind(local);
                while (true)
                {
                    byte[] data = (await client.ReceiveAsync()).Buffer;
                    LastMessage.Message = Encoding.UTF8.GetString(data);
                }
            }
        }
    }
}
