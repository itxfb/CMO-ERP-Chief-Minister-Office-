using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;

namespace pk.gov.pitb.cmo.directive.domain.Models.Common
{
    public class AppUserModel : IdObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public string? PhoneOff { get; set; }
        public string? PhoneRes { get; set; }
        public string? MeetingDay { get; set; }
        public TimeSpan? MeetingTime { get; set; }

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
        public virtual List<UserDepartments> UserDepartments { get; set; }
        public virtual List<UserRoles> UserRoles { get; set; }
        public virtual List<UserFeaturePermission> UserFeaturePermissions { get; set; }
        public virtual List<RoleFeaturePermission> RoleFeaturePermissions { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual District District { get; set; }
        public virtual UserType UserType { get; set; }

    }

}
