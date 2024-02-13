using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using Newtonsoft.Json.Schema;


namespace RestTest
{
    public class GetBoardTrello
    {
        private static RestClient _client;

        [OneTimeSetUp]
        public static void InitializeTestFixture()
        {
            _client = new RestClient("https://api.trello.com");
            Authenticate();
        }

        private static void Authenticate()
        {
            // Authenticate and set up RestClient
            _client.AddDefaultQueryParameter("key", "78cc9dcd0fe568a68b5e7d8cdfab098c");
            _client.AddDefaultQueryParameter("token", "ATTAa93f8c08f0535703af8d7bb92b0a2aa7b113474a74bad30fc61110df1b53b87dC13D048B");
        }

        private RestRequest CreateAuthenticatedRequest(string resource, Method method = Method.Get)
        {
            var request = new RestRequest(resource, method);
            return request;
        }

        [Test]
        public void GetBoards()
        {
            var request = CreateAuthenticatedRequest("/1/members/{member}/boards")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("member", "mr_skit");//path parametr

            var response = _client.Get(request);

            Console.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getBoards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetBoard()
        {
            var request = CreateAuthenticatedRequest("/1/board/{id}")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("id", "5db7e6f535458d1f8a77c0e9");

            var response = _client.Get(request);
            var responseContent = JToken.Parse(response.Content);

            Console.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent.SelectToken("name").ToString(), Is.EqualTo("Homework"));
           
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getBoard.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }
        [Test]
        public void GetCards()
        {
            var request = CreateAuthenticatedRequest("/1/lists/{idList}/cards")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("idList", "5db7e6f535458d1f8a77c0eb");

            var response = _client.Get(request);
            var responseContent = JToken.Parse(response.Content);
            Console.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getCards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));

        }
        [Test]
        public void CheckGetCard()
        {
            var request = CreateAuthenticatedRequest("/1/cards/{id}")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("id", "5df0e93e13a8de6d77bd5498");

            var response = _client.Get(request);
            var responseContent = JToken.Parse(response.Content);
            Console.WriteLine(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent.SelectToken("name").ToString(), Is.EqualTo("Itech"));
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getCard.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }


    }
}
