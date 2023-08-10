using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common
{
    [Table("Offices", Schema = "Common")]
    public class Office : BaseTableColumns
    {

        public string Name { get; set; }

        public string? Abbreviation { get; set; }

        [JsonIgnore]
        public virtual List<Department> Departments { get; set; }

        [JsonIgnore]
        public virtual List<Letter> Letters { get; set; }
    }
}
