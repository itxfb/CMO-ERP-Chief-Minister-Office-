using pk.gov.pitb.cmo.directive.domain.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;
[Table("Domiciles", Schema = "HRMIS")]

public class Domicile : BaseTableColumns
{
    public string ProvinceName { get; set; } = null!;
}
