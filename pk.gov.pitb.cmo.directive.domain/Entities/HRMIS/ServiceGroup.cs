using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;

[Table("ServiceGroups", Schema = "HRMIS")]
public class ServiceGroup : BaseTableColumns
{

    public string? ServiceName { get; set; }

    public virtual List<EmployeesJobsInfo> EmployeesJobsInfos { get; } = new List<EmployeesJobsInfo>();
}
