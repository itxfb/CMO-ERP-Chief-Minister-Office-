using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using pk.gov.pitb.cmo.contracts;
using System.Text.Json.Serialization;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;

namespace pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;

[Table("EmployeesJobsInfo", Schema = "HRMIS")]
public class EmployeesJobsInfo : BaseTableColumns
{
    [JsonConverter(typeof(EncryptIdConverter))]

    public int EmployeeId { get; set; }

    public DateTime JoiningDate { get; set; } = DateTime.MinValue;

    public string? FileNo { get; set; }
    [JsonConverter(typeof(EncryptIdConverter))]

    public int DepartmentId { get; set; } = 0;
    [JsonConverter(typeof(EncryptIdConverter))]


    public int DesignationId { get; set; } = 0;

    [JsonConverter(typeof(EncryptIdConverter))]


    public int JobTypeId { get; set; } = 0;
    [JsonConverter(typeof(EncryptIdConverter))]

    public int AppointmentModeId { get; set; } = 0;
    [JsonConverter(typeof(EncryptIdConverter))]

    [ForeignKey("Bpsscale")]

    public int Bps { get; set; } = 0;

    public decimal BasicPay { get; set; } = 0;

    public string? Ntn { get; set; }

    public string? VendorNo { get; set; }

    public string? Pifra { get; set; }

    public string? Ddoname { get; set; }
    public string? Ddocode { get; set; }
    [JsonConverter(typeof(EncryptIdConverter))]

    public int ServiceGroupId { get; set; } = 0;

    public string? Ctpnumber { get; set; }

    [JsonIgnore]
    public virtual AppointmentMode AppointmentMode { get; set; } = null!;
    [JsonIgnore]

    public virtual Employee Employee { get; set; } = null!;
    [JsonIgnore]

    public virtual JobType JobType { get; set; } = null!;
    [JsonIgnore]

    public virtual ServiceGroup ServiceGroup { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;

    public virtual Designation Designation { get; set; } = null!;

    public virtual Bpsscale Bpsscale { get; set; } = null!;

}
