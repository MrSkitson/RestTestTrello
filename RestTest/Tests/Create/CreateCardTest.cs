using Newtonsoft.Json.Linq;
using RestSharp;
using RestTest.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestTest.Tests.Create
{
    public class CreateCardTest: BaseTest
    {
        private string _createdCardId;

        [Test]
        public async Task CheckCreateCard()
        {
            var cardName = "New Card" + DateTime.Now.ToLongTimeString();

            var request = RequestWithAuth(CardsEndPoints.CreateCardUrl, Method.Post)
                .AddJsonBody(new Dictionary<string, string>
                {
                    {"name", cardName},
                    {"idList", UrlParamValues.ExistingListId}
                });
            var response = await _client.ExecuteAsync(request);

            var responseContent = JToken.Parse(response.Content);

            _createdCardId = responseContent.SelectToken("id").ToString();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(cardName, responseContent.SelectToken("name").ToString());

            request = RequestWithAuth(CardsEndPoints.GetAllCardsUrl, Method.Get)
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            response =await _client.ExecuteAsync(request);
            responseContent = JToken.Parse(response.Content);
            Assert.True(responseContent.Children().Select(token => token.SelectToken("name").ToString()).Contains(cardName));
        }

        [TearDown]
        public async Task DeleteCreatedCard()
        {
           
                var request = RequestWithAuth(CardsEndPoints.DeleteCardUrl, Method.Delete)
                    .AddUrlSegment("id", _createdCardId);
                var response = await _client.ExecuteAsync(request);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            
        }

    }
}
