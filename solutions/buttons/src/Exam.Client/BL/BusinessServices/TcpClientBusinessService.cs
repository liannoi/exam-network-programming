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
using Step.Tcp.Infrastructure.Base;
using System.Threading.Tasks;

namespace Exam.Client.BL.BusinessServices
{
    public abstract class TcpClientBusinessService : ITcpClientBusinessService
    {
        protected ITcpClient client;

        protected TcpClientBusinessService(ITcpClient client)
        {
            this.client = client;
        }

        public abstract void Say<TObject>(TObject @object) where TObject : class;
        public abstract Task SayAsync<TObject>(TObject @object) where TObject : class;
    }
}
