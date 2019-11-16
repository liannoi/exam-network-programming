namespace Exam.BL.BusinessObjects
{
    public class ChatMemberBusinessObject
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
