using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common
{
    [Table("Divisions", Schema = "Common")]
    public class Division : BaseTableColumns
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }

        public virtual List<District> Districts { get; set; }
    }
}
