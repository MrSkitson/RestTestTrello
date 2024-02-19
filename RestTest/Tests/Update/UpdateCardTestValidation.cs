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
    public class UpdateCardTestValidation : BaseTest
    {
        [Test]
        [TestCaseSource(typeof(CardIdValidationArgumentsProvider))]
        public async Task CheckUpdateCardWithInvalidId(CardIdValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithAuth(CardsEndPoints.GetCardUrl, Method.Put)
                .AddOrUpdateParameters(validationArguments.PathParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(validationArguments.StatusCode, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthValidationArgumentProvider))]
        public async Task CheckUpdateCardWithInvalidAuth(AuthValidationArgumentholder validationArguments)
        {
            var request = RequestWithoutAuth(CardsEndPoints.GetCardUrl, Method.Put)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId)
                .AddOrUpdateParameters(validationArguments.AuthParams);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        public async Task CheckUpdateCardWithAnotherUserCredentialsAsync()
        {
            var request = RequestWithoutAuth(CardsEndPoints.GetAllCardsUrl, Method.Get)
                .AddOrUpdateParameters(UrlParamValues.InvalidAuthQueryParams)
                .AddUrlSegment("id", UrlParamValues.ExistingCardId);
            var response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual("invalid key", response.Content);
        }
    }
}
