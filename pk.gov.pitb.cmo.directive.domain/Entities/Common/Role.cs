using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common
{
    [Table("Roles", Schema = "Auth")]
    public class Role : BaseTableColumns
    {

        public string Name { get; set; }
        public bool IsSystem { get; set; }
        public virtual List<RoleFeaturePermission> RolePermissions { get; set; }

    }
}
