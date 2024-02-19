using Newtonsoft.Json.Linq;
using RestSharp;
using RestTest.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestTest.Tests.Delete
{
    public class DeleteCardtest : BaseTest
    {
        private string _createdCardId; 

        [SetUp]
        public async Task CreateCard()
        {
            var request = RequestWithAuth(CardsEndPoints.CreateCardUrl, Method.Post)
               .AddJsonBody(new Dictionary<string, string>
                {
                    {"name", "New Card"},
                    {"idList", UrlParamValues.ExistingListId}
               });
            var response = await _client.ExecuteAsync(request);
            _createdCardId = JToken.Parse(response.Content).SelectToken("id").ToString();
        }

        [Test]
        public async Task CheckDeleteCard()
        {
            var request = RequestWithAuth(CardsEndPoints.DeleteCardUrl, Method.Delete)
                .AddUrlSegment("id", _createdCardId);
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(JToken.Parse(response.Content).SelectToken("_value"), Is.EqualTo(null));

            request = RequestWithAuth(CardsEndPoints.GetAllCardsUrl, Method.Get)
                .AddUrlSegment("list_id", UrlParamValues.ExistingListId);
            response = await _client.ExecuteAsync(request);
            var responseContent = JToken.Parse(response.Content);
            Assert.False(responseContent.Children().Select(token => token.SelectToken("id").ToString()).Contains(_createdCardId));
        }
    }
}
