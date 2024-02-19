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
    public class AuthCardsValidationArgumentProvider : IEnumerable
    {
         public IEnumerator GetEnumerator()
        {
            yield return new object[]
            {
                new AuthCardsValidationArgumentholder
                {
                    
                    AuthParams = new[]
                    {
                        new MyParameter("key", UrlParamValues.InvalidKey, ParameterType.QueryString, false)
                        
                    },
                    ErrorMessage = "invalid key",
                }
            };
            yield return new object[]
            {
                new AuthCardsValidationArgumentholder
                {
                   
                    AuthParams = new[]
                    {
                        new MyParameter("token", UrlParamValues.InvalidToken, ParameterType.QueryString, false)
                    },
                     ErrorMessage = "invalid key",
                }
            };
            yield return new object[]
            {
                new AuthCardsValidationArgumentholder
                {
                   
                    AuthParams = ArraySegment<Parameter>.Empty,
                    ErrorMessage = "invalid key"
                },
                 
            };
        }
    }
}
