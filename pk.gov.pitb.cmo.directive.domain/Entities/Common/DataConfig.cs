using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common
{
    [Table("DataConfigurations", Schema = "Common")]
    public class DataConfig : BaseTableColumns
    {
        public string Name { get; set; }
        public string ConfigName { get; set; }
    }
}
