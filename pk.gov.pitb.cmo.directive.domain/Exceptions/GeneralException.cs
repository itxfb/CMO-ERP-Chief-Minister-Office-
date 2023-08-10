using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Exceptions
{
    public class GeneralException : ExceptionBase
    {
        public GeneralException() { }
        public GeneralException(string message) : base(message) { }

        public GeneralException(string message, object data = null) : base(message, data) { }
        public GeneralException(string message, Exception innerException, object data = null) : base(message, innerException, data) { }
    }
}
