using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestTest
{
    public class TrelloTest
    {
        private static IRestClient _client;

        [OneTimeSetUp]
        public static void InitializeRestClient() =>
            _client = new RestClient("https://api.trello.com");


        [Test]
        public void CheckTrelloAPI()
        {
            var request = new RestRequest();
         
            var response = ((RestClient)_client).Get(request);
            Console.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

      
    }
}
