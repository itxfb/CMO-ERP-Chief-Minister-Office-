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
    [Table("Applications", Schema = "ERP")]
    public class Application : BaseTableColumns
    {

        [ForeignKey("ApplicationType")]
        public int ApplicationTypeId { get; set; }
        public string ApplicationName { get; set; }
        public string AppURL { get; set; }
        public string? IconPath { get; set; }
        public string? Description { get; set; }
        public virtual ApplicationType ApplicationType { get; set; }

        [JsonIgnore]
        public List<Feature> Features { get; set; }

        [JsonIgnore]
        public virtual List<UserFeaturePermission> UserFeatures { get; set; }

        [JsonIgnore]
        public virtual List<RoleFeaturePermission> RoleFeatures { get; set; }
    }
}
