using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;
[Table("Qualification", Schema = "HRMIS")]

public class Qualification : BaseTableColumns
{
    public string DegreeName { get; set; } = null!;
}
