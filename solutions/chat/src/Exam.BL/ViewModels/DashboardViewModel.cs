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

using Exam.BL.BusinessObjects;
using Exam.BL.Helpers;
using Net.Messages.UdpClient.Infrastructure.Base;
using Net.Messages.UdpClient.Infrastructure.Client;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Exam.BL.ViewModels
{
    public sealed class DashboardViewModel : BaseViewModel
    {
        private readonly string chatMessageBuffer = string.Empty;
        private readonly string joinChatBuffer = string.Empty;
        private readonly ObservableCollection<ChatMemberBusinessObject> members = new ObservableCollection<ChatMemberBusinessObject>();
        private readonly ChatMemberBusinessObject currentMember = new ChatMemberBusinessObject();
        private readonly IUdpClient client = new UdpClient(new ClientProperties(), new UdpMessage());
        private readonly ObservableCollection<IUdpMessage> messages = new ObservableCollection<IUdpMessage>();

        public string ChatMessageBuffer
        {
            get => Get(chatMessageBuffer);
            set => Set(value);
        }
        public string JoinChatBuffer
        {
            get => Get(joinChatBuffer);
            set => Set(value);
        }
        public ObservableCollection<ChatMemberBusinessObject> Members
        {
            get => Get(members);
            set => Set(value);
        }
        public ChatMemberBusinessObject CurrentMember
        {
            get
            {
                ChatMemberBusinessObject result = Get(currentMember);
                if (result != null)
                {
                    client.ToLocalhost();
                    return result;
                }
                return new ChatMemberBusinessObject
                {
                    Name = "All"
                };
            }
            set => Set(value);
        }
        public ObservableCollection<IUdpMessage> Messages
        {
            get => Get(messages);
            set => Set(value);
        }
        public ICommand SendMessageCommand => MakeCommand(async a => await SendMessageAsync().ConfigureAwait(false), c => ChatMessageBuffer.IsCorrect());
        public ICommand JoinChatCommnad => MakeCommand(a => AddMember(), c => JoinChatBuffer.IsCorrect());
        public ICommand SelectAllMembersCommand => MakeCommand(a => SelectAll());

        public DashboardViewModel()
        {
            Task.Factory.StartNew(client.ListenAsync);
        }

        private void AddMember()
        {
            Members.Add(new ChatMemberBusinessObject
            {
                Name = JoinChatBuffer
            });
            JoinChatBuffer = string.Empty;
            CurrentMember = Members.LastOrDefault();
        }

        private async Task SendMessageAsync()
        {
            await client.SendAsync(new UdpMessage
            {
                Message = ChatMessageBuffer,
                Sender = CurrentMember.Name
            });
            ChatMessageBuffer = string.Empty;
            Messages.Add(client.LastMessage);
        }

        private void SelectAll()
        {
            client.ToBroadcast();
            CurrentMember = null;
        }
    }
}
