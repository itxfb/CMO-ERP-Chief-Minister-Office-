using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives
{
    public class PriorityAddVm : IdObject
    {

        public string? Name { get; set; }
        public string? ColorHexCode { get; set; }
    }
}
