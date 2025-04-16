using System.Globalization;
using System.Net;
using System.Text.Json;
using API_TestExercise.Models;
using RestSharp;
using Microsoft.Extensions.Options;

namespace API_TestExercise.Services
{
    public interface INASAapiService
    {
        Task<ApiResponse<List<CMEModel>>> GetCMEDataAsync(string startDate, string endDate);
        Task<ApiResponse<List<FLRModel>>> GetFLRDataAsync(string startDate, string endDate);
    }

    public class NASAapiService : INASAapiService
    {
        private readonly RestClient _restClient; 
        private readonly string _apiKey;
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public NASAapiService(IOptions<NASAApiOptions> options)
        {
            var nasaOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _apiKey = nasaOptions.ApiKey ?? throw new ArgumentException("NASA API key not configured");
            _restClient = new RestClient(nasaOptions.BaseUrl);
        }

        public async Task<ApiResponse<List<CMEModel>>> GetCMEDataAsync(string startDate, string endDate)
            => await GetDonkiDataAsync<List<CMEModel>>("CME", startDate, endDate);

        public async Task<ApiResponse<List<FLRModel>>> GetFLRDataAsync(string startDate, string endDate)
            => await GetDonkiDataAsync<List<FLRModel>>("FLR", startDate, endDate);

        private async Task<ApiResponse<T>> GetDonkiDataAsync<T>(string endpoint, string startDate, string endDate)
        {
            Console.WriteLine($"Fetching {endpoint} data from {startDate} to {endDate}");
            return await MakeGetRequestAsync<T>($"/DONKI/{endpoint}", new Dictionary<string, string>
            {
                { "startDate", startDate },
                { "endDate", endDate },
                { "api_key", _apiKey }
            });
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
