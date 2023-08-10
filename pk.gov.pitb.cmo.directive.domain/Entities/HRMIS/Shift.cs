using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;

[Table("Shifts", Schema = "HRMIS")]
public class Shift : BaseTableColumns
{

    public string ShiftName { get; set; } = null!;

    public virtual List<EmployeesPostingsInfo> EmployeesPostingsInfos { get; } = new List<EmployeesPostingsInfo>();
}
