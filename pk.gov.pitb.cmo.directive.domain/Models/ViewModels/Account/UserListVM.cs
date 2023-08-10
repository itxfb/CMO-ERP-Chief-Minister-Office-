using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Account
{
    public class UserListVM : IdObject
    {
        public string FullName { get; set; }

        public int TotalCount { get; set; }
    }
}
