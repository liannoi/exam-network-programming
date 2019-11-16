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
