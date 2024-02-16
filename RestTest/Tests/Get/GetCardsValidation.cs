using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using RestTest.Arguments.Holders;
using RestTest.Arguments.Providers;
using RestTest.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestTest.Tests.Get
{
    public class GetCardsValidation : BaseTest
    {

        [Test]
        public void GetCards()
        {
            var request = RequestWithAuth(CardsEndPoints.GetAllCardsUrl)
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);

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
            var request = RequestWithAuth(CardsEndPoints.GetCardUrl)
                .AddQueryParameter("field", "id, name")
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);

            var response = _client.Get(request);
            var responseContent = JToken.Parse(response.Content);
            Console.WriteLine(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent.SelectToken("name").ToString(), Is.EqualTo("Itech"));
            var jsonSchema = JSchema.Parse(File.ReadAllText($"{Directory.GetCurrentDirectory()}/Resources/Schemas/getCard.json"));
            Assert.True(responseContent.IsValid(jsonSchema));
        }
        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public async Task CheckGetCardsWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
        {
            //var request = RequestWithAuth(CardsEndPoints.GetAllCardsUrl)
            //    .AddUrlSegment("id", "invalid");
            ////var response = ((RestClient)_client).Get(request);

            //var response = await _client.ExecuteGetAsync(request);
            //Console.WriteLine(response.Content);
            //Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            // Assert.AreEqual("invalid id", response.Content);

            var request = RequestWithAuth(CardsEndPoints.GetAllCardsUrl)
               .AddOrUpdateParameters(validationArguments.PathParams);
            var response = await _client.ExecuteGetAsync(request);
            Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthCardsValidationArgumentProvider))]
        public async Task CheckGetCardWithInvalidAuth(AuthCardsValidationArgumentholder validationArguments)
        {
            var request = RequestWithInvalidAuth(CardsEndPoints.GetCardUrl)
                .AddUrlSegment("id", UrlParamValues.ExistingListId)
              .AddOrUpdateParameters(validationArguments.AuthParams);
            var response = await _client.ExecuteGetAsync(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            Assert.AreEqual("invalid key", response.Content);
        }
    }
}
