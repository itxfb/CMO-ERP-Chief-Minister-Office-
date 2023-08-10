using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Account
{
    public class LoginVm
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public string? Email { get; set; }
     
    }
}
