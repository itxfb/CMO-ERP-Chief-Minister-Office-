using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration
{
    public class FeatureVm : BaseTableColumns
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? ApplicationId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Path { get; set; }
        public virtual List<UserFeaturePermission> UserFeatures { get; set; }
        public virtual List<RoleFeaturePermission> RoleFeatures { get; set; }
    }
}
