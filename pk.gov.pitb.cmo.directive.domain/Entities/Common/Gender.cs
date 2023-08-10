using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common;

[Table("Genders", Schema = "Common")]
public class Gender : BaseTableColumns
{
    public string GenderType { get; set; } = null!;
}
