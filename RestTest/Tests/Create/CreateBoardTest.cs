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
        public void CheckCreateBoard()
        {
            var boardName = "New Board" + DateTime.Now.ToLongTimeString();

            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl)
                .AddJsonBody(new Dictionary<string, string> /*{{ "name", boardName }});*/
            {
                { "name", boardName},
                    { "id", UrlParamValues.ExistingBoardId}
            });
            var response = _client.Post(request);

            Console.WriteLine($"Response Content after creating board: {response.Content}");
            var responseContent = JToken.Parse(response.Content);

            _createdBoardId = responseContent.SelectToken("id").ToString();
            Console.WriteLine($"Created board with ID: {_createdBoardId}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(boardName, responseContent.SelectToken("name").ToString());

            request = RequestWithAuth(BoardsEndpoints.GetAllBoardUrl)
                // .AddQueryParameter("fields", "id,name")
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId)
                .AddUrlSegment("member", UrlParamValues.UserName);
            response = _client.Get(request);
            responseContent = JToken.Parse(response.Content);
            Assert.True(responseContent.Children().Select(token => token.SelectToken("name").ToString()).Contains(boardName));

        }
        [TearDown]
        public void DeleteCreatedBoard()
        {
           
                Console.WriteLine($"Deleting board with ID: {_createdBoardId}");
                var requset = RequestWithAuth(BoardsEndpoints.DeleteBoardUrl)
                 .AddUrlSegment("id", _createdBoardId);
                var response = _client.Delete(requset);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


        }
    }
}
