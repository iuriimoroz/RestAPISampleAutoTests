using Newtonsoft.Json.Linq;
using RestSharp;

namespace RestAPISampleAutoTests.Utils
{
    public static class ApiClient
    {
        public static IRestResponse SendRequest(string url, IRestRequest request)
        {
            return new RestClient(url).Execute(request);
        }

        public static JObject ParseResponseContent(IRestResponse response)
        {
            return JObject.Parse(response.Content);
        }
    }
}