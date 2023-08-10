using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives
{
    public class SubjectVm
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
