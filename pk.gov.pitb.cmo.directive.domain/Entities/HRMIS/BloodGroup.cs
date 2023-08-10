using pk.gov.pitb.cmo.directive.domain.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;

[Table("BloodGroups", Schema = "HRMIS")]
public class BloodGroup : BaseTableColumns
{
    public string Name { get; set; } = null!;
}