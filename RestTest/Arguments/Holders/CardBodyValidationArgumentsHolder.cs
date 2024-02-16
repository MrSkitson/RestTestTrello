using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTest.Arguments.Holders
{
    public class CardBodyValidationArgumentsHolder
    {
        public IDictionary<string, object> BodyParams { get; set; }

        public string ErrorMessage { get; set; }
    }
}
