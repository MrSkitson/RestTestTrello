using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestTest
{
    public class GetCardsValidation : BaseClass
    {

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
        public async Task CheckGetCardsWithInvalidId()
        {
            var request = RequestWithAuth("/1/lists/{idList}/cards")
                .AddUrlSegment("id", "invalid");
            //var response = ((RestClient)_client).Get(request);
         
            var response = await _client.ExecutePostAsync(request);
            Console.WriteLine(response.Content);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
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
    }
}
