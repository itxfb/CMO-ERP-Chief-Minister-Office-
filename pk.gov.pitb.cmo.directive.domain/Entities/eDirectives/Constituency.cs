using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;

namespace pk.gov.pitb.cmo.directive.domain.Entities.eDirectives
{
    [Table("Constituencies", Schema = "eDirectives")]
    public class Constituency : BaseTableColumns
    {
        public string? Name { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("ConstituencyType")]
        public int? ConstituencyTypeId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("District")]
        public int? DistrictId { get; set; }
        public string Code { get; set; }
        public virtual District? District { get; set; }
        public virtual List<AppUser> User { get; set; }
        public virtual ConstituencyType ConstituencyType { get; set; }

        [JsonIgnore]
        public virtual List<Letter> Letters { get; set; }

    }
}
