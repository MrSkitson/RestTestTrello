using RestSharp;
using RestTest.Arguments.Holders;
using RestTest.Consts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Parameter = RestSharp.Parameter;

namespace RestTest.Arguments.Providers
{
    public class AuthValidationArgumentProvider : IEnumerable
    {
         public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new AuthValidationArgumentholder
                {

                    AuthParams = new[]
                    {
                        new MyParameter("key", UrlParamValues.InvalidKey, ParameterType.QueryString, true)

                    },
                     ErrorMessage = "invalid key",
                    //ErrorMessage ="{\"message\":\"Board must be in a team — specify an idOrganization\"}",
                }
            };
            yield return new object[]
            {
                new AuthValidationArgumentholder
                {
                     
                    AuthParams = new[]
                    {
                        new MyParameter("token", UrlParamValues.InvalidToken, ParameterType.QueryString)
                    },
                     ErrorMessage = "unauthorized permission requested",
                    //ErrorMessage ="{\"message\":\"Board must be in a team — specify an idOrganization\"}",
                }
            };
            yield return new object[]
            {

                new AuthValidationArgumentholder
                { 
                    
                    AuthParams = ArraySegment<MyParameter>.Empty,
                     ErrorMessage = "unauthorized permission requested",
                    //ErrorMessage ="{\"message\":\"Board must be in a team — specify an idOrganization\"}"
                }

            };
        }
    }
}
