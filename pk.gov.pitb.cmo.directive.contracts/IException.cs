using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Contracts
{
    public interface IException
    {
       
        public object Data { get;  }
        public void Log();

    }
}
