namespace MathAPI.Models
{
    public class Error
    {
        public string? ErrorMessage { get; set; }

        public Error(string? errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
