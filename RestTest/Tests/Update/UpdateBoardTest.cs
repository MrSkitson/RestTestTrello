using Newtonsoft.Json.Linq;
using RestSharp;
using RestTest.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestTest.Tests.Update
{
    public class UpdateBoardTest : BaseTest
    {

        [Test]
        public async Task CheckUpdateBoard() 
        {
            var updatedName = "Updated Name" + DateTime.Now.ToLongTimeString();
            var request = RequestWithAuth(BoardsEndpoints.UpdateBoardUrl, Method.Put)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate)
                .AddJsonBody(new Dictionary<string, string> { { "name", updatedName } });
            var response =await _client.ExecuteAsync(request);

            var responseContent = JToken.Parse(response.Content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(updatedName, responseContent.SelectToken("name").ToString());

            request = RequestWithAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
                .AddUrlSegment("id", UrlParamValues.BoardIdToUpdate);
            response = await _client.ExecuteAsync(request);
            responseContent = JToken.Parse(response.Content);
            Assert.AreEqual(updatedName, responseContent.SelectToken("name").ToString());
        }

    }
}
