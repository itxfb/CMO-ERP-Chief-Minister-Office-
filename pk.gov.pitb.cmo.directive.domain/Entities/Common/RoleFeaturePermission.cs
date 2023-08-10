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
    [Table("RolePermissions", Schema = "Auth")]
    public class RoleFeaturePermission : BaseTableColumns
    {
        [ForeignKey("Role")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int RoleId { get; set; }

        [ForeignKey("Application")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int ApplicationId { get; set; }

        [ForeignKey("Feature")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int FeatureId { get; set; }
        public virtual Feature Feature { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }

        public virtual Application Application { get; set; }
    }
}
