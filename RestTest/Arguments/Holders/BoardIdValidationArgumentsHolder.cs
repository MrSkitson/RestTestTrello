﻿using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace RestTest.Arguments.Holders
{
 
    public class BoardIdValidationArgumentsHolder
    {
        public IEnumerable<Parameter> PathParams { get; set; }

        public string ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
