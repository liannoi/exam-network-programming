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

using Exam.Client.BL.BusinessServices;
using Exam.Shared.BL.BusinessObjects;
using Step.Tcp.Infrastructure.Client;
using System;
using System.Windows.Forms;

namespace Exam.Client
{
    public partial class Dashboard : Form
    {
#if Debug_Borders

        private readonly FormBorderBusinessService formBorderBusinessService;
        
#endif
        private readonly BaseServerProperties serverProperties;
        private readonly ButtonBusinessService buttonBusinessService;

        private bool IsStartPress => !startButton.Enabled;

        public Dashboard()
        {
            InitializeComponent();
            serverProperties = new BaseServerProperties();
            try
            {
                buttonBusinessService = new ButtonBusinessService(
                    new TcpClient(serverProperties.ServerProperties),
                    carButton);
            }
            catch (ServerNotAvailableException e)
            {
                MessageBox.Show(e.Message);
                throw;
            }

#if Debug_Borders

            formBorderBusinessService = new FormBorderBusinessService(new FormBusinessObject
            {
                Form = this
            }, buttonBusinessService.ButtonBusinessObject);
        
#endif
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            NotifyServer();
            startButton.Enabled = false;
        }

        private async void CarButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (!IsStartPress)
            {
                MessageBox.Show("Click Start");
                return;
            }

            if (e.KeyCode == Keys.Left)
            {
                buttonBusinessService.ToLeft();
            }

            else if (e.KeyCode == Keys.Up)
            {
                buttonBusinessService.ToUp();
            }

            else if (e.KeyCode == Keys.Right)
            {
                buttonBusinessService.ToRight();
            }

            else if (e.KeyCode == Keys.Down)
            {
                buttonBusinessService.ToButton();
            }

            NotifyServer();
        }

        private void NotifyServer()
        {
            buttonBusinessService.NotifyServer();
        }
    }
}
