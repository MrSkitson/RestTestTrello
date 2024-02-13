using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestTestGoogle
{
    public class GoogleTest
    {
        public static IRestClient _client;

        [OneTimeSetUp]
        public static void InitializerestClient() =>
            _client = new RestClient("https://google.com");

        [Test]
        public static void CheckGoogleAPI()
        {
            var request = new RestRequest();
           // var client = new RestClient("https://google.com");
            //Console.WriteLine($"{client.BaseUrl} {request.Method}");

            var response = ((RestClient)_client).Get(request);

            Console.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
