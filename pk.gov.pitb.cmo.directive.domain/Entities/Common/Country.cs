using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Hrmis
{
    [Table("Country", Schema = "Common")]
    public partial class Country : BaseTableColumns
    {
        public string CountryName { get; set; } = null!;
       
    }
}
