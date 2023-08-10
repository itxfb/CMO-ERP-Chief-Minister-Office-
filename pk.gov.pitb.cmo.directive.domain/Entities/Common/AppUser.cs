using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common
{
    [Table("AppUsers", Schema = "Auth")]

    public class AppUser : BaseTableColumns
    {

        public AppUser()
        {

        }
        public AppUser(string username, string password, bool isActive = true, bool isLocked = false, bool isTwoFactorEnabled = false)
        {

            Username = username;
            Password = password;
            IsActive = isActive;
            IsLocked = isLocked;
            IsTwoFactorEnabled = isTwoFactorEnabled;
            CreatedAt = DateTime.UtcNow;
        }
        public string? Username { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }
        public string? PhoneOff { get; set; }
        public string? PhoneRes { get; set; }
        public string? MeetingDay { get; set; }
        public TimeSpan? MeetingTime { get; set; }
        public bool IsLocked { get; set; }
        public bool IsAuthority { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public DateTime? LastLoggedInDateTime { get; set; }

        [ForeignKey("Constituency")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? ConstituencyId { get; set; }

        [ForeignKey("Designation")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? DesignationId { get; set; }

        [ForeignKey("Party")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? PartyId { get; set; }

        [ForeignKey("District")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? ResidentDistrictId { get; set; }

        [ForeignKey("UserType")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int UserTypeId { get; set; }


        // AppUser Relation Navigators 
        public virtual Constituency Constituency { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual Party Party { get; set; }
        public virtual District District { get; set; }
        public virtual UserType UserType { get; set; }

        // Foriegn Key Navigators
        public virtual List<UserDepartments> UserDepartments { get; set; }
        public virtual List<UserRoles> UserRoles { get; set; }
        public virtual List<UserFeaturePermission> UserFeaturePermissions { get; set; }

    }
}
