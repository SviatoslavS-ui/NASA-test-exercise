using API_TestExercise.Models;
using API_TestExercise.Services;
using TechTalk.SpecFlow;
using API_TestExercise.Tests.Utilities;
using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace API_TestExercise.Tests.StepDefinitions
{
    [Binding] // SpecFlow attribute to mark this as a step definition class
    internal class CMEApiSteps
    {
        private readonly NASAapiService _nasaApiService;       

        public CMEApiSteps()
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

        [Then(@"the response should return HTTP 200 with a non-empty list")]
        public async Task ThenTheResponseShouldReturnHttp200WithANonEmptyList()
        {
            var response = await _nasaApiService.GetCMEDataAsync("2025-04-07", "2025-04-12");
            // Validate the response
            ResponseValidator.ValidateCMEData(response);
        }

        [Then(@"the response should return HTTP 400 with error message")]
        public async Task ThenTheResponseShouldReturnHttp400WithErrorMessage()
        {
            // Invalid date range: startDate is after endDate
            var response = await _nasaApiService.GetCMEDataAsync("invalid", "invalid");

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
