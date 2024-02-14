using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTest
{
    public class BaseClass
    {
        protected static RestClient _client;
        [OneTimeSetUp]
        public static void InitializeRestClient() =>
            _client = new RestClient("https://api.trello.com");
    
        protected RestRequest RequestWithAuth(string url) =>
           RequestWithoutAuth(url)
               .AddQueryParameter("key", "78cc9dcd0fe568a68b5e7d8cdfab098c")
               .AddQueryParameter("token", "ATTAa93f8c08f0535703af8d7bb92b0a2aa7b113474a74bad30fc61110df1b53b87dC13D048B");
        protected RestRequest RequestWithoutAuth(string url) =>
         new RestRequest(url);
      
    }
}
