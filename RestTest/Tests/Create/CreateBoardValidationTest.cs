using Newtonsoft.Json.Linq;
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

namespace RestTest.Tests.Create
{
    public class CreateBoardValidationTest : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BoardNameValidationArgumentProvider))]
        public async Task CheckCreateBoardWithInvalidName(IDictionary<string, object> bodyParams)
        {
            var request = RequestWithAuth(BoardsEndpoints.CreateBoardUrl, Method.Post)
                .AddJsonBody(bodyParams);
            var response = await _client.ExecuteAsync(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.AreEqual("{\"message\":\"invalid value for name\",\"error\":\"ERROR\"}", response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentProvider))]
        public async Task CheckCreateBoardWithInvalidAuth(AuthValidationArgumentholder validationArguments)
        {
            var boardName = "New Board";
            var request = RequestWithoutAuth(BoardsEndpoints.CreateBoardUrl, Method.Post)
             .AddOrUpdateParameters(validationArguments.AuthParams)
             .AddJsonBody(new Dictionary<string, string> { { "name", boardName } });

            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
           

        }
    }
    }
