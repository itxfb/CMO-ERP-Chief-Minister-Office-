using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;

[Table("EmployeesSubStatus", Schema = "HRMIS")]
public class EmployeesSubStatus : BaseTableColumns
{
    public string EmployeeSubName { get; set; } = null!;

    public virtual List<EmployeesPostingsInfo> EmployeesPostingsInfos { get; } = new List<EmployeesPostingsInfo>();
}
