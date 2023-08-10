using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;

namespace pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;

[Table("Employees", Schema = "HRMIS")]
public class Employee : BaseTableColumns
{
    public string EmployeeName { get; set; } = null!;

    public string FatherName { get; set; } = null!;

    public DateTime DateBirth { get; set; }

    [JsonConverter(typeof(EncryptIdConverter))]
    public int? Gender { get; set; }
    
    [JsonConverter(typeof(EncryptIdConverter))]

    public int BloodGroup { get; set; }

    [JsonConverter(typeof(EncryptIdConverter))]

    public int Religion { get; set; } 

    public string Cnic { get; set; } = null!;

    public string PresentAddress { get; set; } = null!;

    public string PermanentAddress { get; set; } = null!;
    [JsonConverter(typeof(EncryptIdConverter))]
    public int Domicile { get; set; } 

    public string Mobile { get; set; } = null!;

    public string? Phone { get; set; }
    //public IFormFile? IFormFile { get; set; }

    public string? Image { get; set; }

    public string OfficialEmail { get; set; } = null!;

    public string PersonalEmail { get; set; } = null!;
    [JsonConverter(typeof(EncryptIdConverter))]

    public int AcademicQualification { get; set; } = 0;
    public string? ExtraQualification { get; set; }

    [JsonConverter(typeof(EncryptIdConverter))]

    public int Training { get; set; } = 0;
    [NotMapped]
    public IFormCollection? EmployeeAttachments { get; set; }


    //public string? LocalTraining { get; set; }

    //public string? ForeignTraining { get; set; }

    public virtual List<EmployeesColor> EmployeesColors { get; set; } = new List<EmployeesColor>();

    public virtual List<EmployeesJobsInfo> EmployeesJobsInfos { get; set; } = new List<EmployeesJobsInfo>();

    public virtual List<EmployeesPostingsInfo> EmployeesPostingsInfos { get; set; } = new List<EmployeesPostingsInfo>();
}
