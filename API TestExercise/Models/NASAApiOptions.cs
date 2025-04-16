namespace API_TestExercise.Models
{
    public class NASAApiOptions
    {
        public const string ConfigSection = "NASA";
        public string ApiKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = "https://api.nasa.gov";
    }
} 