using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RestTest.Arguments.Holders
{
    public record MyParameter : Parameter
    {
        public MyParameter(string name, object? value, ParameterType type, bool encode = true)
            : base(name, value, type, encode) { }
        public MyParameter(string name, string value, ParameterType type)
                : base(name, value, type, true) { }
    }
}
