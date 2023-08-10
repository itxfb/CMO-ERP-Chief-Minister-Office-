using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;

namespace pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;

[Table("AppointmentModes", Schema = "HRMIS")]
public class AppointmentMode : BaseTableColumns
{
    public string AppointmentName { get; set; } = null!;

    public virtual List<EmployeesJobsInfo> EmployeesJobsInfos { get; } = new List<EmployeesJobsInfo>();
}
