using System.Net;

namespace API_TestExercise.Models
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
