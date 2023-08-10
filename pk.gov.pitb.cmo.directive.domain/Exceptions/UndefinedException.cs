using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Exceptions
{
    public class UndefinedException : ExceptionBase
    {
        public UndefinedException() { }
        public UndefinedException(Exception parentException) : base(message: "An undefined error has occurred. Please contact technical team.", innerException: parentException) { }
        public UndefinedException(string message) : base(message) { }

        public UndefinedException(string message, object data = null) : base(message, data) { }
        public UndefinedException(string message, Exception innerException, object data = null) : base(message, innerException, data) { }


    }
}
