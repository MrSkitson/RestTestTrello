
using RestSharp;
using RestTest.Arguments.Holders;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace RestTest.Arguments.Providers
{
    public class BoardIdValidationArgumentProvider : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new BoardIdValidationArgumentsHolder
                {
                    ErrorMessage = "invalid id",
                    StatusCode = HttpStatusCode.BadRequest,
                    PathParams = new[] {new MyParameter("id", "invalid", ParameterType.UrlSegment, false)}
                }
            };
            yield return new object[]
            {
                new BoardIdValidationArgumentsHolder
                {
                    ErrorMessage = "The requested resource was not found.",
                    StatusCode = HttpStatusCode.BadRequest,
                    PathParams = new[] {new MyParameter("id", "60d847d9aad2437cb984f8e1", ParameterType.UrlSegment, false)}
                }
            };
        }
    }
    
}
