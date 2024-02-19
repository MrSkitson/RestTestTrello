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
    public  class CreateBoardTest : BaseTest
    {
        private string _createdBoardId;
        [Test]
        public async Task CheckCreateBoard()
        {
            var boardName = "New Board" + DateTime.Now.ToLongTimeString();

            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl, Method.Post)
                .AddJsonBody(new Dictionary<string, string> /*{{ "name", boardName }});*/
            {
                { "name", boardName},
                    { "id", UrlParamValues.ExistingBoardId}
            });
            var response =  await _client.ExecuteAsync(request);

            Console.WriteLine($"Response Content after creating board: {response.Content}");
            var responseContent = JToken.Parse(response.Content);

            _createdBoardId = responseContent.SelectToken("id").ToString();
            Console.WriteLine($"Created board with ID: {_createdBoardId}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(boardName, responseContent.SelectToken("name").ToString());

            request = RequestWithAuth(BoardsEndpoints.GetAllBoardUrl, Method.Get)
                // .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId)
                .AddUrlSegment("member", UrlParamValues.UserName);
            response = await _client.ExecuteAsync(request);
            responseContent = JToken.Parse(response.Content);
            Assert.True(responseContent.Children().Select(token => token.SelectToken("name").ToString()).Contains(boardName));

        }
        [TearDown]
        public async Task DeleteCreatedBoard()
        {
           
                Console.WriteLine($"Deleting board with ID: {_createdBoardId}");
                var requset = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl, Method.Delete)
                 .AddUrlSegment("id", _createdBoardId);
                var response = await _client.ExecuteAsync(requset);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


        }
    }
}
