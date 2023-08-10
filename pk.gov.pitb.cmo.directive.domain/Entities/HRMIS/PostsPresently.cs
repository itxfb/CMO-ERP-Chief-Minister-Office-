using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;

[Table("PostsPresently", Schema = "HRMIS")]
public class PostsPresently : BaseTableColumns
{
    public string? PostPresentlyName { get; set; }

    public virtual List<EmployeesPostingsInfo> EmployeesPostingsInfos { get; } = new List<EmployeesPostingsInfo>();
}
