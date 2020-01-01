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

using Exam.Server.BL.BusinessServices;
using Exam.Shared.BL.BusinessObjects;
using Step.Tcp.Infrastructure.Events;
using Step.Tcp.Infrastructure.Server;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam.Server
{
    public partial class Dashboard : Form
    {
        private readonly ButtonListener buttonListener;
        private readonly BaseServerProperties serverProperties;

        public Dashboard()
        {
            InitializeComponent();
            serverProperties = new BaseServerProperties();
            buttonListener = new ButtonListener(new TcpServer(serverProperties.ServerProperties));
            buttonListener.DataReceived += ButtonListener_DataReceived;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            try
            {
                buttonListener.Start();
            }
            catch (SingletonTcpServerException exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
            Task.Factory.StartNew(buttonListener.ListenAsync);
        }

        private void ButtonListener_DataReceived(object sender, DataReceivedEventArgs<ButtonBusinessObject> e)
        {
            Invoke(new Action(() =>
            {
                Control[] find = Controls.Find(e.Data.ClientId, true);
                if (find.Length == 0)
                {
                    Controls.Add(new Button
                    {
                        Width = e.Data.Width,
                        Height = e.Data.Height,
                        Location = new Point(e.Data.X, e.Data.Y),
                        Name = e.Data.ClientId,
                        UseVisualStyleBackColor = true,
                        Enabled = true,
                        Text = e.Data.ClientIP
                    });
                }
                else
                {
                    find.FirstOrDefault().Location = new Point(e.Data.X, e.Data.Y);
                }
            }));
        }
    }
}
