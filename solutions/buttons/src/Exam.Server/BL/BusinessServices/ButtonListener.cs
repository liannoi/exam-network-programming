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

using Exam.Shared.BL.BusinessObjects;
using Step.Tcp.Infrastructure.Base;
using Step.Tcp.Infrastructure.Events;
using System;
using System.Threading.Tasks;

namespace Exam.Server.BL.BusinessServices
{
    public sealed class ButtonListener : TcpServerBusinessService, INotifyDataReceived<ButtonBusinessObject>
    {
        public event EventHandler<DataReceivedEventArgs<ButtonBusinessObject>> DataReceived;
        
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
