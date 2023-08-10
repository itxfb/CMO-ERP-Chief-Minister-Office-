using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives
{
    public class DirectiveCountVM
    {
        public int ImplementedCount { get; set; }
        
        public int PendingDepartmentCount { get; set; }

        public int PendingCMOCount { get; set; }

        public int PendingDisposedOffCount { get; set; }
    }
}
