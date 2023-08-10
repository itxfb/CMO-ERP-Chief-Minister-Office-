using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.eDirectives
{
    [Table("Subjects", Schema = "eDirectives")]
    public class Subject : BaseTableColumns
    {
        public string Code { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public virtual List<Letter> Letters { get; set; }
    }
}
