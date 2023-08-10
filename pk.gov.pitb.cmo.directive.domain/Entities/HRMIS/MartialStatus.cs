using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;

[Table("MartialStatus", Schema = "HRMIS")]
public class MartialStatus : BaseTableColumns
{
    public string Status { get; set; } = null!;

    public virtual List<EmployeesPostingsInfo> EmployeesPostingsInfos { get; } = new List<EmployeesPostingsInfo>();
}
