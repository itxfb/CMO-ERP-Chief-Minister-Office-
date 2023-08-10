using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common
{
    [Table("Provinces", Schema = "Common")]
    public class Province : BaseTableColumns
    {
        public string Name { get; set; }
    }
}
