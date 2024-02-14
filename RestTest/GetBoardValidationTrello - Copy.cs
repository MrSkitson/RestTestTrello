using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using Newtonsoft.Json.Schema;


namespace RestTest
{
    public class GetBoardValidationTrello : BaseClass
    {
        //private static IRestClient _client;

      
        [Test]
        public void GetBoards()
        {
            var request = RequestWithAuth("/1/members/{member}/boards")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("member", "mr_skit");//path parametr

            var response = ((RestClient)_client).Get(request);

            Console.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseContent = JToken.Parse(response.Content);
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getBoards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public void CheckGetBoard()
        {
            var request = RequestWithAuth("/1/board/{id}")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("id", "5db7e6f535458d1f8a77c0e9");

            var response = ((RestClient)_client).Get(request);
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
            var request = RequestWithAuth("/1/lists/{idList}/cards")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("idList", "5db7e6f535458d1f8a77c0eb");

            var response = ((RestClient)_client).Get(request);
            var responseContent = JToken.Parse(response.Content);
            Console.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getCards.json"));
            Assert.True(responseContent.IsValid(jsonSchema));

        }
        [Test]
        public void CheckGetCard()
        {
            var request = RequestWithAuth("/1/cards/{id}")
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("id", "5df0e93e13a8de6d77bd5498");

            var response = ((RestClient)_client).Get(request);
            var responseContent = JToken.Parse(response.Content);
            Console.WriteLine(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent.SelectToken("name").ToString(), Is.EqualTo("Itech"));
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getCard.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }

        [Test]
        public async Task CheckGetBoardWithInvalidId()
        {
            var request = RequestWithAuth("/1/boards/{id}")
                .AddUrlSegment("id", "invalid");
            var response = await _client.ExecutePostAsync(request);
            Console.WriteLine(response.Content);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
           // Assert.AreEqual("invalid id", response.Content);
        }
        [Test]
        public async Task CheckGetBoardWithInvalidAuth()
        {
            var request = RequestWithoutAuth("/1/boards/{id}")
                .AddUrlSegment("id", "61fe437419cdd87656ce9fa6");
            var response = await _client.ExecutePostAsync(request);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.AreEqual("Cannot POST /1/boards/61fe437419cdd87656ce9fa6", response.Content);
        }

        [Test]
        public async Task CheckGetBoardWithAnotherUserCredentials()
        {
            var request =RequestWithoutAuth("/1/boards/{id}")
                .AddQueryParameter("key", "8b32218e6887516d17c84253faf967b6")
                .AddQueryParameter("token", "492343b8106e7df3ebb7f01e219cbf32827c852a5f9e2b8f9ca296b1cc604955")
                .AddUrlSegment("id", "61fe437419cdd87656ce9fa6");
            var response = await _client.ExecutePostAsync(request);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            //Assert.AreEqual("Cannot POST /1/boards/61fe437419cdd87656ce9fa6?", response.Content);
        }

    }
}
