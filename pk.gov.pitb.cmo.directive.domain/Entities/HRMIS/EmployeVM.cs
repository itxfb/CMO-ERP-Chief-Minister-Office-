using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Hrmis
{
    public class EmployeVM
    {
        public string? EmployeeName { get; set; }
        public string? FatherName { get; set; }
        public DateTime? DateBirth { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? Gender { get; set; }
        public string? GenderName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? BloodGroup { get; set; }
        public string? BloodGroupName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? Religion { get; set; }
        public string? ReligionName { get; set; }

        public string? Cnic { get; set; }
        [JsonConverter(typeof(EncryptIdConverter))]

        public int? Domicile { get; set; }
        public string? DomicileName { get; set; }

        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? OfficialEmail { get; set; }
        public string? PersonalEmail { get; set; }
        [JsonConverter(typeof(EncryptIdConverter))]

        public int? AcademicQualification { get; set; }
        public string? AcademicQualificationName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? Training { get; set; }
        public string? TrainingName { get; set; }

        public string? PermanentAddress { get; set; }
        public string? PresentAddress { get; set; }
        public string? FileNo { get; set; }
        [JsonConverter(typeof(EncryptIdConverter))]

        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? JobTypeId { get; set; }
        public string? JobTypeName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? DesignationId { get; set; }
        public string? DesignationName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? Bps { get; set; }
        public string? BpsName { get; set; }

        public DateTime? JoiningDate { get; set; }
        public string? Ntn { get; set; }
        public string? VendorNo { get; set; }
        public string? Ddoname { get; set; }
        public string? Ddocode { get; set; }
        public string? Pifra { get; set; }
        public decimal? BasicPay { get; set; }
        [JsonConverter(typeof(EncryptIdConverter))]

        public int? AppointmentModeId { get; set; }
        public string? AppointmentModeName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? ServiceGroupId { get; set; }
        public string? ServiceGroupName { get; set; }

        public string? Ctpnumber { get; set; }
        [JsonConverter(typeof(EncryptIdConverter))]

        public int? OfficeNameId { get; set; }
        public string? OfficeName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? PostPresentlyId { get; set; }
        public string? PostPresentlyName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? PostNatureId { get; set; }
        public string? PostNatureName { get; set; }

        public DateTime? JoiningDateCm { get; set; }
        [JsonConverter(typeof(EncryptIdConverter))]

        public int? ShiftId { get; set; }
        public string? ShiftName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? PoliceStation { get; set; }
        public string? PoliceStationName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? EmployeeStatusId { get; set; }
        public string? EmployeeStatusName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? EmployeeSubStatusId { get; set; }
        public string? EmployeeSubStatusName { get; set; }

        public DateTime? RepatriationDate { get; set; }
        [JsonConverter(typeof(EncryptIdConverter))]

        public int? CardTypeId { get; set; }
        public string? CardTypeName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? MartialStatusId { get; set; }
        public string? MartialStatusName { get; set; }

        public string? Remarks { get; set; }
        public int? CardIssued { get; set; }
        public string? ImagePath { get; set; }
        public string? Imagebase64 { get; set; }
        public List<ColorAccess> colors { get; set; } = new List<ColorAccess>();

    }


    public class ColorAccess
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int Id { get; set; }
    }

}
