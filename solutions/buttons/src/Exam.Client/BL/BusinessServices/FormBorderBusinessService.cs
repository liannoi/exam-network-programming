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
