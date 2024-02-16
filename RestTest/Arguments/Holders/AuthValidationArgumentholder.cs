using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTest.Arguments.Holders
{
    public class AuthValidationArgumentholder
    {
        public IEnumerable<Parameter> AuthParams { get; set; }
        public string ErrorMessage { get; set; }
    }
}
