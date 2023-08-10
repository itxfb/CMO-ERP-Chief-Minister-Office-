using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common
{

    [Table("UserRoles", Schema = "Auth")]
    public class UserRoles : BaseTableColumns
    {
        [ForeignKey("Role")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int RoleId { get; set; }

        [ForeignKey("User")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int UserId { get; set; }

        public virtual Role Role { get; set; }

        [JsonIgnore]
        public virtual AppUser User { get; set; }
    }
}
