using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.contracts;
using System.Text.Json.Serialization;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;

namespace pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;


[Table("EmployeesPostingsInfo", Schema = "HRMIS")]
public class EmployeesPostingsInfo : BaseTableColumns
{
    [JsonConverter(typeof(EncryptIdConverter))]
    public int EmployeeId { get; set; }
    [JsonConverter(typeof(EncryptIdConverter))]

    public int OfficeNameId { get; set; } = 0;
    [JsonConverter(typeof(EncryptIdConverter))]

    public int PostNatureId { get; set; } = 0;
    [JsonConverter(typeof(EncryptIdConverter))]

    public int ShiftId { get; set; } = 0;
    [JsonConverter(typeof(EncryptIdConverter))]

    public int? PostPresentlyId { get; set; }

    public DateTime JoiningDateCm { get; set; } = DateTime.MinValue;

    public DateTime? RepatriationDate { get; set; }
    [JsonConverter(typeof(EncryptIdConverter))]
    public int PoliceStation { get; set; } = 0;
    [JsonConverter(typeof(EncryptIdConverter))]

    public int EmployeeStatusId { get; set; } = 0;
    [JsonConverter(typeof(EncryptIdConverter))]

    public int? EmployeeSubStatusId { get; set; }
    [JsonConverter(typeof(EncryptIdConverter))]

    public int MartialStatusId { get; set; } = 0;
    [JsonConverter(typeof(EncryptIdConverter))]

    public int CardTypeId { get; set; } = 0;

    public int? CardIssued { get; set; }

    public string? Remarks { get; set; }
    [JsonIgnore]
    public virtual CardType CardType { get; set; } = null!;
    [JsonIgnore]

    public virtual Employee Employee { get; set; } = null!;
    [JsonIgnore]

    public virtual EmployeesStatus EmployeeStatus { get; set; } = null!;
    [JsonIgnore]

    public virtual EmployeesSubStatus? EmployeeSubStatus { get; set; }
    [JsonIgnore]

    public virtual MartialStatus MartialStatus { get; set; } = null!;
    [JsonIgnore]

    public virtual PostNature PostNature { get; set; } = null!;
    [JsonIgnore]

    public virtual PostsPresently? PostPresently { get; set; }
    [JsonIgnore]

    public virtual Shift Shift { get; set; } = null!;
    [ForeignKey("OfficeNameId")]
    public virtual Office Office { get; set; } = null!;
    [ForeignKey("PoliceStation")]
    [JsonIgnore]

    public virtual PoliceStation Police { get; set; } = null!;
}
