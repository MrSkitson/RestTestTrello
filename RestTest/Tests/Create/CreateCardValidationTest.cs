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
    public class CreateCardValidationTest : BaseTest
    {
         [Test]
        [TestCaseSource(typeof(CardBodyValidationArgumentsProvider))]
        public async Task CheckCreateCardWithInvalidName(CardBodyValidationArgumentsHolder validationArguments)
        {
            var request = RequestWithAuth(CardsEndPoints.CreateCardUrl)
                .AddJsonBody(validationArguments.BodyParams);
            var response = await _client.ExecutePostAsync(request);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        [Test]
        [TestCaseSource(typeof(AuthCardsValidationArgumentProvider))]
        public async Task CheckCreateCardWithInvalidAuth(AuthCardsValidationArgumentholder validationArguments)
        {
            var request = RequestWithoutAuth(CardsEndPoints.CreateCardUrl)
                .AddOrUpdateParameters(validationArguments.AuthParams)
                .AddJsonBody(new Dictionary<string, string>
                {
                    {"name", "New item"},
                    {"idList", UrlParamValues.ExistingListId}
                });
            var response = await _client.ExecutePostAsync(request);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(validationArguments.ErrorMessage, response.Content);
        }

        //[Test]
        //public void CheckCreateCardWithAnotherUserCredentials()
        //{
        //    var request = RequestWithoutAuth(CardsEndPoints.CreateCardUrl)
        //        .AddOrUpdateParameters(UrlParamValues.AnotherUserAuthQueryParams)
        //        .AddJsonBody(new Dictionary<string, string>
        //        {
        //            {"name", "New item"},
        //            {"idList", UrlParamValues.ExistingListId}
        //        });
        //    var response = _client.Post(request);
        //    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        //    Assert.AreEqual("invalid token", response.Content);
        //}
    }
}
