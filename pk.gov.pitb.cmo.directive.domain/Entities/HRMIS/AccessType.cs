using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;

[Table("AccessTypes", Schema = "HRMIS")]
public class AccessType : BaseTableColumns
{
    public string TypeName { get; set; } = null!;

    public virtual List<EmployeesPostingsInfo> EmployeesPostingsInfos { get; } = new List<EmployeesPostingsInfo>();
}
