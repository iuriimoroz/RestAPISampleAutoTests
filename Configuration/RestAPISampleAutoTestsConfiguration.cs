using System;

namespace RestAPISampleAutoTests.Configuration
{
    public static class RestAPISampleAutoTestsConfiguration
    {
        private static readonly string defaultBaseUrl = "https://reqres.in";

        public static string BaseUrl { get; } = GetBaseUrl();

        private static string GetBaseUrl()
        {
            // Fetch the base URL from environment variables
            var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL");

            // If the environment variable is not set, use the default URL
            return string.IsNullOrEmpty(baseUrl) ? defaultBaseUrl : baseUrl;
        }
    }
}
