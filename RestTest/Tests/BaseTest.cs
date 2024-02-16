using RestSharp;
using RestTest.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTest.Tests
{
    public class BaseTest
    {
        protected static RestClient _client;
        [SetUp]
        public static void InitializeRestClient() =>
            _client = new RestClient("https://api.trello.com");

        protected RestRequest RequestWithAuth(string url) =>
           RequestWithoutAuth(url)
               .AddOrUpdateParameters(UrlParamValues.AuthQueryParams);
        protected RestRequest RequestWithoutAuth(string url) =>
         new RestRequest(url);
        protected RestRequest RequestWithInvalidAuth(string url) =>
            RequestWithoutAuth(url)
                .AddOrUpdateParameters(UrlParamValues.InvalidAuthQueryParams);
    }
}
