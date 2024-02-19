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

namespace RestTest.Tests.Update
{
    public class UpdateBoardTestValidation : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(BoardIdValidationArgumentProvider))]
        public async Task CheckUpdateBoardWithInvalidId(BoardIdValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithAuth(BoardsEndpoints.GetBoardUrl, Method.Put)
                .AddOrUpdateParameters(validationArguments.PathParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentProvider))]
        public async Task CheckUpdateBoardWithInvalidAuth(AuthValidationArgumentholder validationArguments)
        {
            var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl, Method.Put)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId)
                .AddOrUpdateParameters(validationArguments.AuthParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        public async Task CheckUpdateBoardWithAnotherUserCredentialsAsync()
        {
            var request = RequestWithoutAuth(BoardsEndpoints.GetBoardUrl, Method.Get)
                .AddOrUpdateParameters(UrlParamValues.InvalidAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid key", response.Content);
        }
    }
}
