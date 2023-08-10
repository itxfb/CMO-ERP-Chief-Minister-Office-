using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;

namespace pk.gov.pitb.cmo.directive.domain.Entities.eDirectives
{
    [Table("Parties", Schema = "eDirectives")]
    public class Party : BaseTableColumns
    {
        public string Name { get; set; }

        public virtual List<AppUser> User { get; set; }
    }
}
