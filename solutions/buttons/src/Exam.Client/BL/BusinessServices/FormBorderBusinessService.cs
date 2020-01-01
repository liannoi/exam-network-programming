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

using Exam.Client.BL.BusinessObjects;
using Exam.Client.BL.Infrastructure;
using Exam.Client.Helpers;
using Exam.Shared.BL.BusinessObjects;

namespace Exam.Client.BL.BusinessServices
{
    public class FormBorderBusinessService : IFormBorderBusinessService
    {
        private readonly FormBusinessObject formBusinessObject;
        private readonly ButtonBusinessObject buttonBusinessObject;
        private readonly int formBorderLimit;

        private int FormBorderLimit => formBorderLimit == 0 ? 40 : formBorderLimit;

        public FormBorderBusinessService(FormBusinessObject formBusinessObject, ButtonBusinessObject buttonBusinessObject)
        {
            this.formBusinessObject = formBusinessObject;
            this.buttonBusinessObject = buttonBusinessObject;
            formBorderLimit = FormBorderLimit;
        }

        public FormBorderBusinessService(FormBusinessObject formBusinessObject, ButtonBusinessObject buttonBusinessObject, int formBorderLimit)
        {
            this.formBusinessObject = formBusinessObject;
            this.buttonBusinessObject = buttonBusinessObject;
            this.formBorderLimit = formBorderLimit;
        }

        public ControlDirection AllowToBorder()
        {
            if (buttonBusinessObject.X <= FormBorderLimit && buttonBusinessObject.Y > FormBorderLimit / 2)
            {
                return ControlDirection.Top;
            }
            if ((buttonBusinessObject.Y + buttonBusinessObject.Height + FormBorderLimit * 3) >= formBusinessObject.Height)
            {
                return ControlDirection.Left;
            }
            else if ((buttonBusinessObject.X + buttonBusinessObject.Width + FormBorderLimit) >= formBusinessObject.Width)
            {
                return ControlDirection.Button;
            }
            else
            {
                return ControlDirection.Right;
            }
        }
    }
}
