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
    [Table("ConstituencyTypes", Schema = "eDirectives")]
    public class ConstituencyType : BaseTableColumns
    {

        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Constituency> Constituencies { get; set; }
    }
}
