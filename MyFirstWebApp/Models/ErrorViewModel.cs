namespace MyFirstWebApp.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorMessage { get; set; }
        public string ErrorStackTrace { get; set; }

        public ErrorViewModel(string msg, string trace)
        {
            ErrorMessage = msg;
            ErrorStackTrace = trace;
        }
    }
}