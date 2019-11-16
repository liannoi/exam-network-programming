﻿// Copyright 2019 Maksym Liannoi
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

using Exam.Server.BL.Infrastructure;
using Step.Tcp.Infrastructure.Base;
using System.Threading.Tasks;

namespace Exam.Server.BL.BusinessServices
{
    public abstract class TcpServerBusinessService : ITcpServerBusinessService
    {
        protected readonly ITcpServer server;

        protected TcpServerBusinessService(ITcpServer server)
        {
            this.server = server;
        }

        public void Start()
        {
            server.Start();
        }

        public abstract void Listen();

        public abstract Task ListenAsync();
    }
}
