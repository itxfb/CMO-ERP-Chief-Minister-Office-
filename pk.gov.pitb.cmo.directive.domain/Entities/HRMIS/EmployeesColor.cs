using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;


[Table("EmployeesColors", Schema = "HRMIS")]
public class EmployeesColor : BaseTableColumns
{
    public int EmployeeId { get; set; }
    [JsonConverter(typeof(EncryptIdConverter))]

    public int AccessColorId { get; set; }
    [ForeignKey("AccessColorId")]

    public virtual AccessColor AccessColor { get; set; } = null!;
    [JsonIgnore]

    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; set; } = null!;
}
