using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration
{
    public class UserRoleAddEditVm
    {
        [JsonConverter(typeof(EncryptIdConverter))]

        public int? Id { get; set; }

        public string Name { get; set; }


    }
}
