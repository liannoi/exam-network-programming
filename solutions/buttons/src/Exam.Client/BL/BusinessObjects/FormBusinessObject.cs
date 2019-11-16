using System.Windows.Forms;

namespace Exam.Client.BL.BusinessObjects
{
    public sealed class FormBusinessObject
    {
        public Form Form { get; set; }
        public int Height => Form.Height;
        public int Width => Form.Width;
    }
}
