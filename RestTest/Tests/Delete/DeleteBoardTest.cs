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
    public class DeleteBoardTest : BaseTest
    {
        private string _createdBoardId;

        [SetUp]
        public async Task CreateBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl, Method.Post)
                .AddJsonBody(new Dictionary<string, string> { { "name", "New Board" } });
            var response = await _client.ExecuteAsync(request);
            _createdBoardId = JToken.Parse(response.Content).SelectToken("id").ToString();
        }

        [Test]
        public async Task CheckDeleteBoard()
        {
            var request = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl, Method.Delete)
                .AddUrlSegment("id", _createdBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(string.Empty, JToken.Parse(response.Content).SelectToken("_value").ToString());

            request = RequestWithAuth(BoardsEndpoints.GetAllBoardUrl, Method.Get)
                .AddUrlSegment("member", UrlParamValues.UserName);
            response = await _client.ExecuteAsync(request);
            var responseContent = JToken.Parse(response.Content);
            Assert.False(responseContent.Children().Select(token => token.SelectToken("id").ToString()).Contains(_createdBoardId));
        }
    }
}
