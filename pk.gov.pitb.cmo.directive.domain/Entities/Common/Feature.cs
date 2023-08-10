using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common
{
    [Table("Features", Schema = "Common")]
    public class Feature : BaseTableColumns
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("Application")]
        public int? ApplicationId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? ParentId { get; set; } = null;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Path { get; set; }

        //Navigators
        [JsonIgnore]
        public virtual List<UserFeaturePermission> UserFeatures { get; set; }

      
        public virtual List<RoleFeaturePermission> RoleFeatures { get; set; }


        public virtual Application Application { get; set; }

    }
}
