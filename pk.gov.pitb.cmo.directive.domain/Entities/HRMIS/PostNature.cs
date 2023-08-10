using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;

[Table("PostNatures", Schema = "HRMIS")]
public class PostNature : BaseTableColumns
{
    public string? PostName { get; set; }

    public virtual List<EmployeesPostingsInfo> EmployeesPostingsInfos { get; } = new List<EmployeesPostingsInfo>();
}
