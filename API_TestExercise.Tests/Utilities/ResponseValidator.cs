using System.Net;
using API_TestExercise.Models;
using NUnit.Framework;

namespace API_TestExercise.Tests.Utilities
{
    public static class ResponseValidator
    {
        public static void ValidateCMEData(ApiResponse<List<CMEModel>> response)
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected HTTP 200 OK.");
            Assert.That(response.Data, Is.Not.Null, "CME data should not be null.");
            Assert.That(response.Data, Is.Not.Empty, "CME data list should not be empty.");
        }

        public static void ValidateErrorResponse(ApiResponse<object> response, HttpStatusCode expectedStatusCode, string expectedErrorMessage)
        {
            Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode), $"Expected status code {expectedStatusCode}, but got {response.StatusCode}.");
            Assert.That(response.ErrorMessage, Is.Not.Null.And.Contains(expectedErrorMessage), "Error message does not match.");
        }

        public static void ValidateFLRData(ApiResponse<List<FLRModel>> response)
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected HTTP 200 OK.");
            Assert.That(response.Data, Is.Not.Null, "FLR data should not be null.");
            Assert.That(response.Data, Is.Not.Empty, "FLR data list should not be empty.");
        }
    }
}
