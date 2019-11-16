using Exam.Shared.BL.BusinessObjects;
using System.Windows.Forms;

namespace Exam.Shared.Helpers
{
    public static class Updateable
    {
        public static void Update(this ButtonBusinessObject buttonBusinessObject, Button button)
        {
            buttonBusinessObject.Height = button.Height;
            buttonBusinessObject.Width = button.Width;
            buttonBusinessObject.X = button.Location.X;
            buttonBusinessObject.Y = button.Location.Y;
        }
    }
}
