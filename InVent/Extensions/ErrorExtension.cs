namespace InVent.Extensions
{
    public static class ErrorExtension
    {
        public static string HandleErrorMessage(string message)
        {
            return message switch
            {
                string a when a.Contains("Violation of UNIQUE KEY constraint") => "آیتم تکراری",
                string b when b.Contains("The DELETE statement conflicted with the REFERENCE constraint") => "این آیتم درجای دیگری استفاده شده است.",
                _ => message,
            };
        }
    }
}
