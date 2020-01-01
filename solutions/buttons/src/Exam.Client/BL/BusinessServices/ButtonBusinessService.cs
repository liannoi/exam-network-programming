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

using Exam.Client.BL.Infrastructure;
using Exam.Shared.BL.BusinessObjects;
using Exam.Shared.Helpers;
using Step.Tcp.Infrastructure.Base;
using Step.Tcp.Infrastructure.Client;
using Step.Tcp.Infrastructure.Helpers;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam.Client.BL.BusinessServices
{
    public sealed class ButtonBusinessService : TcpClientBusinessService, IButtonBusinessService
    {
        private readonly Button button;
        private readonly TcpClientBusinessService tcpClientBusinessService;

        private static int ButtonMoveStep => 20;
        public ButtonBusinessObject ButtonBusinessObject { get; private set; }

        public ButtonBusinessService(ITcpClient client, Button button) : base(client)
        {
            this.button = button;
            ButtonBusinessObject = new ButtonBusinessObject
            {
                ClientId = Guid.NewGuid().ToString(),
                ClientIP = IPDefiner.LocalIP
            };
            tcpClientBusinessService = this;
        }

        public override void Say<TObject>(TObject @object)
        {
            try
            {
                client.Say(@object);
            }
            catch (ServerNotAvailableException exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
            client = client.Reconnect();
        }

        public override async Task SayAsync<TObject>(TObject @object)
        {
            try
            {
                await client.SayAsync(@object);
            }
            catch (ServerNotAvailableException exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
            client = client.Reconnect();
        }

        public void NotifyServer()
        {
            ButtonBusinessObject.Update(button);
            tcpClientBusinessService.Say(ButtonBusinessObject);
        }

        public void ToLeft()
        {
            button.Location = new Point(button.Location.X - ButtonMoveStep, button.Location.Y);
        }

        public void ToUp()
        {
            button.Location = new Point(button.Location.X, button.Location.Y - ButtonMoveStep);
        }

        public void ToRight()
        {
            button.Location = new Point(button.Location.X + ButtonMoveStep, button.Location.Y);
        }

        public void ToButton()
        {
            button.Location = new Point(button.Location.X, button.Location.Y + ButtonMoveStep);
        }
    }
}
