using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Exceptions
{


    public class AuthenticationException  : ExceptionBase
    {
        public AuthenticationException() { }
        public AuthenticationException(string message) : base(message) { }

        public AuthenticationException(string message, object data = null) : base(message, data) { }
        public AuthenticationException(string message, Exception innerException, object data = null) : base(message, innerException, data) { }
    }
}
