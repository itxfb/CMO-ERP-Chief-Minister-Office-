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
    [Table("UserPermissions", Schema = "Auth")]
    public class UserFeaturePermission : BaseTableColumns
    {
        [ForeignKey("User")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int UserId { get; set; }

        [ForeignKey("Application")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int ApplicationId { get; set; }

        [ForeignKey("Feature")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int FeatureId { get; set; }

        [JsonIgnore]
        public virtual AppUser User { get; set; }


        public virtual Application Application { get; set; }


        public virtual Feature Feature { get; set; }



    }
}
