using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;

[Table("AccessColors", Schema = "HRMIS")]
public class AccessColor : BaseTableColumns
{
    public string ColorName { get; set; } = null!;

    [JsonIgnore]
    public virtual List<EmployeesColor> EmployeesColors { get; } = new List<EmployeesColor>();
}
