using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Models.Common;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration
{
    public class AddUserProfileVm
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public string? PhoneOff { get; set; }
        public string? PhoneRes { get; set; }
        public bool GenerateUser { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? ConstituencyId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? DesignationId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? PartyId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? ResidentDistrictId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int UserTypeId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? DepartmentId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? RoleId { get; set; }

        [NotMapped]
        public int? TotalCount { get; set; }

    }



}
