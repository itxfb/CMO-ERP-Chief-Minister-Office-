using pk.gov.pitb.cmo.contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.HRMIS
{
    public class EmployeeSearch
    {


        public string? EmployeeName { get; set; }
        public string? FatherName { get; set; }
        public string JoiningDate { get; set; }
        public string? CNIC { get; set; }
        public string? Mobile { get; set; }
        public string? OfficialEmail { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? DepartmentId { get; set; } = 0;

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? OfficeNameId { get; set; } = 0;

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? DesignationId { get; set; } = 0;
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? Bps { get; set; } = 0;
    }
    public class ID
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? id { get; set; }
    }
}
