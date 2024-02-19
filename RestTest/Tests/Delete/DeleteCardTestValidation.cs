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

namespace RestTest.Tests.Delete
{
    public class DeleteCardTestValidation : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public async Task CheckDeleteCardWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithAuth(CardsEndPoints.DeleteCardUrl, Method.Delete)
                .AddOrUpdateParameters(validationArguments.PathParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentProvider))]
        public async Task CheckDeleteCardWithInvalidAuth(AuthValidationArgumentholder validationArguments)
        {
            var request = RequestWithoutAuth(CardsEndPoints.DeleteCardUrl, Method.Delete)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId)
                .AddOrUpdateParameters(validationArguments.AuthParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        public async Task CheckDeleteCardWithAnotherUserCredentials()
        {
            var request = RequestWithoutAuth(CardsEndPoints.DeleteCardUrl, Method.Delete)
                .AddOrUpdateParameters(UrlParamValues.InvalidAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingBoardId);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid key", response.Content);
        }
    }
}
