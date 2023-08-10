using pk.gov.pitb.cmo.directive.domain.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace pk.gov.pitb.cmo.directive.web.HRMIS;

[Table("Religions", Schema = "HRMIS")]
public class Religion : BaseTableColumns
{
    public string Name { get; set; } = null!;
}
