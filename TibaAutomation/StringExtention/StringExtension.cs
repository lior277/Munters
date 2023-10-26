namespace AutomationInfra.StringExtention
{
    public static class StringExtension
    {
        public static string RemoveNewLine(this string text)
        {
            return text.Replace(Environment.NewLine, " ");
        }

    }
}
