using NUnit.Framework;
using System.Net;
using RestSharp;
using RestAPISampleAutoTests.Utils;
using RestAPISampleAutoTests.Configuration;
using NLog;
using Newtonsoft.Json.Schema;

namespace RestAPISampleAutoTests.Tests
{
    [TestFixture, Order(0)]
    public class ApiEndpointTests
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [SetUp]
        public void Setup()
        {
            Logger.Debug("*************************************** TEST STARTED");
            Logger.Debug("Test Case: {0}", TestContext.CurrentContext.Test.MethodName);
        }

        [TearDown]
        public void TearDown()
        {
            Logger.Debug("Test Status: {0}", TestContext.CurrentContext.Result.Outcome.Status);
            Logger.Debug("*************************************** TEST FINISHED" + System.Environment.NewLine);
        }

        [Test, Order(1)]
        public void ShouldReturnListOfUsersDefaultPage()
        {
            var apiResponse = ApiClient.SendRequest(RestAPISampleAutoTestsConfiguration.BaseUrl + "/api/users", new RestRequest(Method.GET));
            var users = ApiClient.ParseResponseContent(apiResponse);

            Assert.Multiple(() =>
            {
                Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code 200.");
                Assert.That(apiResponse.Content.Length, Is.GreaterThan(0), "Expected response content to be non-empty.");
                Assert.That(users.IsValid(JsonSchemas.UsersJsonSchema()), "Response does not match the expected schema.");
            });
        }

        [Test, Order(2)]
        public void ShouldReturnListOfUsersSecondPage()
        {
            var apiResponse = ApiClient.SendRequest(RestAPISampleAutoTestsConfiguration.BaseUrl + "/api/users?page=2", new RestRequest(Method.GET));
            var parsedResponseContent = ApiClient.ParseResponseContent(apiResponse);

            Assert.Multiple(() =>
            {
                Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code 200.");
                Assert.That(apiResponse.Content.Length, Is.GreaterThan(0), "Expected response content to be non-empty.");
                Assert.That((int)parsedResponseContent["page"], Is.EqualTo(2), "Expected current page to be 2.");
            });
        }

        [Test, Order(3)]
        public void ShouldReturnSingleUser()
        {
            var apiResponse = ApiClient.SendRequest(RestAPISampleAutoTestsConfiguration.BaseUrl + "/api/users/2", new RestRequest(Method.GET));
            var parsedResponseContent = ApiClient.ParseResponseContent(apiResponse);

            Assert.Multiple(() =>
            {
                Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code 200.");
                Assert.That(apiResponse.Content.Length, Is.GreaterThan(0), "Expected response content to be non-empty.");
                Assert.That((int)parsedResponseContent["data"]["id"], Is.EqualTo(2), "Expected user ID to be 2.");
                Assert.That((string)parsedResponseContent["data"]["email"], Is.EqualTo("janet.weaver@reqres.in"), "Expected email to be janet.weaver@reqres.in.");
                Assert.That((string)parsedResponseContent["data"]["first_name"], Is.EqualTo("Janet"), "Expected first name to be Janet.");
                Assert.That((string)parsedResponseContent["data"]["last_name"], Is.EqualTo("Weaver"), "Expected last name to be Weaver.");
                Assert.That((string)parsedResponseContent["data"]["avatar"], Is.EqualTo("https://reqres.in/img/faces/2-image.jpg"), "Expected avatar URL to be https://reqres.in/img/faces/2-image.jpg.");
            });
        }

        [Test, Order(4)]
        public void ShouldReturnUserNotFound()
        {
            var apiResponse = ApiClient.SendRequest(RestAPISampleAutoTestsConfiguration.BaseUrl + "/api/users/23", new RestRequest(Method.GET));

            Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Expected status code 404.");
        }

        [Test, Order(5)]
        public void ShouldReturnListOfResourcesDefaultPage()
        {
            var apiResponse = ApiClient.SendRequest(RestAPISampleAutoTestsConfiguration.BaseUrl + "/api/unknown", new RestRequest(Method.GET));
            var parsedResponseContent = ApiClient.ParseResponseContent(apiResponse);

            Assert.Multiple(() =>
            {
                Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code 200.");
                Assert.That(apiResponse.Content.Length, Is.GreaterThan(0), "Expected response content to be non-empty.");
                Assert.That(parsedResponseContent.IsValid(JsonSchemas.ResourcesJsonSchema()), "Response does not match the expected schema.");
            });
        }

        [Test, Order(6)]
        public void ShouldReturnSingleResource()
        {
            var apiResponse = ApiClient.SendRequest(RestAPISampleAutoTestsConfiguration.BaseUrl + "/api/unknown/2", new RestRequest(Method.GET));
            var parsedResponseContent = ApiClient.ParseResponseContent(apiResponse);

            Assert.Multiple(() =>
            {
                Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code 200.");
                Assert.That(apiResponse.Content.Length, Is.GreaterThan(0), "Expected response content to be non-empty.");
                Assert.That((int)parsedResponseContent["data"]["id"], Is.EqualTo(2), "Expected resource ID to be 2.");
                Assert.That((string)parsedResponseContent["data"]["name"], Is.EqualTo("fuchsia rose"), "Expected resource name to be fuchsia rose.");
                Assert.That((string)parsedResponseContent["data"]["year"], Is.EqualTo("2001"), "Expected resource year to be 2001.");
                Assert.That((string)parsedResponseContent["data"]["color"], Is.EqualTo("#C74375"), "Expected resource color to be #C74375.");
                Assert.That((string)parsedResponseContent["data"]["pantone_value"], Is.EqualTo("17-2031"), "Expected Pantone value to be 17-2031.");
            });
        }

        [Test, Order(7)]
        public void ShouldReturnResourceNotFound()
        {
            var apiResponse = ApiClient.SendRequest(RestAPISampleAutoTestsConfiguration.BaseUrl + "/api/unknown/23", new RestRequest(Method.GET));

            Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Expected status code 404.");
        }

        [Test, Order(8)]
        public void ShouldCreateUser()
        {
            var jsonBody = "{ \"name\": \"morpheus\", \"job\": \"leader\" }";
            var apiResponse = ApiClient.SendRequest(RestAPISampleAutoTestsConfiguration.BaseUrl + "/api/users", new RestRequest(Method.POST)
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(jsonBody));
            var parsedResponseContent = ApiClient.ParseResponseContent(apiResponse);

            Assert.Multiple(() =>
            {
                Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Expected status code 201.");
                Assert.That((string)parsedResponseContent["name"], Is.EqualTo("morpheus"), "Expected name to be morpheus.");
                Assert.That((string)parsedResponseContent["job"], Is.EqualTo("leader"), "Expected job to be leader.");
            });
        }

        [Test, Order(9)]
        public void ShouldUpdateUser()
        {
            var jsonBody = "{ \"name\": \"morpheus\", \"job\": \"zion resident\" }";
            var apiResponse = ApiClient.SendRequest(RestAPISampleAutoTestsConfiguration.BaseUrl + "/api/users/2", new RestRequest(Method.PUT)
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(jsonBody));
            var parsedResponseContent = ApiClient.ParseResponseContent(apiResponse);

            Assert.Multiple(() =>
            {
                Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code 200.");
                Assert.That((string)parsedResponseContent["name"], Is.EqualTo("morpheus"), "Expected name to be morpheus.");
                Assert.That((string)parsedResponseContent["job"], Is.EqualTo("zion resident"), "Expected job to be zion resident.");
            });
        }

        [Test, Order(10)]
        public void ShouldUpdateUserAgain()
        {
            var jsonBody = "{ \"name\": \"morpheus\", \"job\": \"zion resident\" }";
            var apiResponse = ApiClient.SendRequest(RestAPISampleAutoTestsConfiguration.BaseUrl + "/api/users/2", new RestRequest(Method.PATCH)
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(jsonBody));
            var parsedResponseContent = ApiClient.ParseResponseContent(apiResponse);

            Assert.Multiple(() =>
            {
                Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected status code 200.");
                Assert.That((string)parsedResponseContent["name"], Is.EqualTo("morpheus"), "Expected name to be morpheus.");
                Assert.That((string)parsedResponseContent["job"], Is.EqualTo("zion resident"), "Expected job to be zion resident.");
            });
        }

        [Test, Order(11)]
        public void ShouldDeleteUser()
        {
            var apiResponse = ApiClient.SendRequest(RestAPISampleAutoTestsConfiguration.BaseUrl + "/api/users/2", new RestRequest(Method.DELETE));

            Assert.Multiple(() =>
            {
                Assert.That(apiResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent), "Expected status code 204.");
                Assert.That(apiResponse.Content.Length, Is.EqualTo(0), "Expected response content to be empty.");
            });
        }
    }
}
