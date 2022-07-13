namespace MyFirstWebApp.Classes.API
{
    public class Result
    {
        public bool success { get; init; }
        public string error { get; init; }

        public Result()
        {
            success = true;           
        }

        public Result(string message)
        {
            success = false;
            error = message;
        }
    }
}
