using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration
{
    public class UpdateRolePermissionVm
    {
        
        [JsonConverter(typeof(EncryptIdConverter))]
        public int RoleId { get; set; }
        
        public List<RolePermissionVm> ListPermissions { get; set; }
    }

    public class RolePermissionVm
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int Id { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int ApplicationId { get; set; }
        public bool IsEnabled { get; set; }
    }   
}
