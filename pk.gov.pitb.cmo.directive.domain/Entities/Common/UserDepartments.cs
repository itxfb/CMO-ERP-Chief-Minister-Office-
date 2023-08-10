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
    [Table("UserDepartments", Schema = "Auth")]
    public class UserDepartments : BaseTableColumns
    {
        [ForeignKey("User")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int UserId { get; set; }

        [ForeignKey("Department")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        [JsonIgnore]
        public virtual AppUser User { get; set; }
    }
}
