using System.Net;
using API_TestExercise.Models;
using API_TestExercise.Services;
using API_TestExercise.Tests.Utilities;
using TechTalk.SpecFlow;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace API_TestExercise.Tests.StepDefinitions
{
    [Binding] // SpecFlow attribute to mark this as a step definition class
    internal class FLRApiSteps
    {
        private readonly NASAapiService _nasaApiService;

        public FLRApiSteps()
        {            
            // Initialize the NASA API service with the configuration from appsettings.json
            _nasaApiService = new NASAapiService(Options.Create(
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .GetSection("NASA")
                    .Get<NASAApiOptions>()
            ));
        }

        [Then(@"the response should return HTTP 200 with flare data")]
        public async Task ThenTheResponseShouldReturnHttp200WithFlareData()
        {
            var response = await _nasaApiService.GetFLRDataAsync("2025-04-07", "2025-04-12");
            // Validate the response
            ResponseValidator.ValidateFLRData(response);
        }

        [Then(@"the response should return HTTP 400 for missing startDate")]
        public async Task ThenTheResponseShouldReturnHttp400ForMissingStartDate()
        {
            // Invalid date range: startDate is missed
            var response = await _nasaApiService.GetFLRDataAsync("invalid", "2025-04-12");

            var convertedResponse = new ApiResponse<object>
            {
                StatusCode = response.StatusCode,
                Data = null,
                ErrorMessage = response.ErrorMessage
            };
            // Validate the response
            ResponseValidator.ValidateErrorResponse(convertedResponse, HttpStatusCode.BadRequest, "Invalid date range");
        }
    }
}
