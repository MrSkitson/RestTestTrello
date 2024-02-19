using Newtonsoft.Json.Linq;
using RestSharp;
using RestTest.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestTest.Tests.Update
{
    public class UpdateCardTest : BaseTest
    {

        [Test]
        public async Task CheckUpdateCard()
        {
            var updatedName = "Updated Name" + DateTime.Now.ToLongTimeString();
            var request = RequestWithAuth(CardsEndPoints.UpdateCardUrl, Method.Put)
                .AddUrlSegment("id", UrlParamValues.CardIdToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", updatedName } });
            var response = await _client.ExecuteAsync(request);

            var responseContent = JToken.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(updatedName, responseContent.SelectToken("name").ToString());

            request = RequestWithAuth(CardsEndPoints.GetCardUrl, Method.Get)
                .AddUrlSegment("id", UrlParamValues.CardIdToUpdate);
            response = await _client.ExecuteAsync(request);
            responseContent = JToken.Parse(response.Content);
            Assert.AreEqual(updatedName, responseContent.SelectToken("name").ToString());
        }
    }
}
