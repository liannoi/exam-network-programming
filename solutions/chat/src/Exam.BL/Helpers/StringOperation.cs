namespace Exam.BL.Helpers
{
    public static class StringOperation
    {
        public static bool IsCorrect(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
