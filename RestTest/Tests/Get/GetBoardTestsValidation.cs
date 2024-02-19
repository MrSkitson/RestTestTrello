using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using Newtonsoft.Json.Schema;
using RestTest.Arguments.Holders;
using RestTest.Arguments.Providers;
using RestTest.Consts;

namespace RestTest.Tests.Get
{
    public class GetBoardValidationTrello : BaseTest
    {
        
        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentProvider))]
        public async Task CheckGetBoardWithInvalidAuth(AuthValidationArgumentholder validationArguments)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl, Method.Get)

            .AddOrUpdateParameters(validationArguments.AuthParams)
            .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.That(response.Content, Is.EqualTo(validationArguments.ErrorMessage));
          
        }

        [Test]
        public async Task CheckGetBoardWithAnotherUserCredentials()
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
             .AddOrUpdateParameters(UrlParamValues.AuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = await _client.ExecuteAsync(request);
            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

    }
}
