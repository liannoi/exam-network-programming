using System;

namespace Exam.Shared.BL.BusinessObjects
{
    [Serializable]
    public sealed class ButtonBusinessObject
    {
        public string ClientId { get; set; }
        public string ClientIP { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
