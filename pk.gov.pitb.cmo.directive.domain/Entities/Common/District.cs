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
    [Table("Districts", Schema = "Common")]

    public class District : BaseTableColumns
    {
        public string Name { get; set; }

        [ForeignKey("Divsion")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int DivisionId { get; set; }

        [JsonIgnore]
        public virtual List<AppUser> User { get; set; }

        [JsonIgnore]
        public virtual Division Divison { get; set; }


    }
}
