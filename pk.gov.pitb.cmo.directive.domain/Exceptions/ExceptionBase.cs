using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Contracts;


namespace pk.gov.pitb.cmo.directive.domain.Exceptions
{
    public abstract class ExceptionBase : Exception, IException
    {

        
        protected ExceptionBase(object data = null)
        {
            this.Data = data;
        }
        protected ExceptionBase(string message) : base(message) {  }
        protected ExceptionBase(string message, object data = null) : base(message) { this.Data = data; }
        protected ExceptionBase(string message, Exception innerException) : base(message, innerException) {  }
        protected ExceptionBase(string message, Exception innerException, object data = null) : base(message, innerException) { this.Data = data; }

        public object Data { get; private set; }


        public virtual void Log()
        {

        }
    }
}
