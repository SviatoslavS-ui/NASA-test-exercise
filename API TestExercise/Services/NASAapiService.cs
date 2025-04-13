using System.Globalization;
using System.Net;
using System.Text.Json;
using API_TestExercise.Models;
using RestSharp;

namespace API_TestExercise.Services
{
    public class NASAapiService
    {
        private readonly RestClient _restClient;
        private const string BaseUrl = "https://api.nasa.gov";
        private const string ApiKey = "DEMO_KEY";
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public NASAapiService()
        {
            _restClient = new RestClient(BaseUrl);
        }

        // Fetch CME data
        public async Task<ApiResponse<List<CMEModel>>> GetCMEDataAsync(string startDate, string endDate)
        {
            Console.WriteLine($"Fetching CME data from {startDate} to {endDate}");
            string endpoint = "/DONKI/CME";
            var queryParams = new Dictionary<string, string>
            {
                { "startDate", startDate },
                { "endDate", endDate },
                { "api_key", ApiKey }
            };

            return await MakeGetRequestAsync<List<CMEModel>>(endpoint, queryParams);
        }

        // Fetch FLR data
        public async Task<ApiResponse<List<FLRModel>>> GetFLRDataAsync(string startDate, string endDate)
        {
            Console.WriteLine($"Fetching FLR data from {startDate} to {endDate}");
            string endpoint = "/DONKI/FLR";
            var queryParams = new Dictionary<string, string>
                {
                    { "startDate", startDate },
                    { "endDate", endDate },
                    { "api_key", ApiKey }
                };

            return await MakeGetRequestAsync<List<FLRModel>>(endpoint, queryParams);
        }

        // Reusable method for making GET requests
        private async Task<ApiResponse<T>> MakeGetRequestAsync<T>(string endpoint, Dictionary<string, string> queryParams)
        {
            // Validate the date range because the API falls back to default values if invalid
            if (!(DateTime.TryParseExact(queryParams["startDate"], "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime start) &&
                DateTime.TryParseExact(queryParams["endDate"], "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime end)) || start > end)
            {
                return new ApiResponse<T>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = "Invalid date range"
                };
            }

            Console.WriteLine($"Making GET request to {endpoint} with parameters: {queryParams}");
            var request = new RestRequest(endpoint, Method.Get);

            foreach (var param in queryParams)
            {
                request.AddQueryParameter(param.Key, param.Value);
            }

            var response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
            {
                Console.WriteLine($"Request to {endpoint} failed with status code {response.StatusCode}: {response.ErrorMessage}");
                return new ApiResponse<T>
                {
                    StatusCode = response.StatusCode,
                    ErrorMessage = response.ErrorMessage
                };
            }

            Console.WriteLine($"Request to {endpoint} succeeded with status code {response.StatusCode}");
            return new ApiResponse<T>
            {
                StatusCode = response.StatusCode,
                Data = JsonSerializer.Deserialize<T>(response.Content, JsonOptions)
            };
        }
    }
}
