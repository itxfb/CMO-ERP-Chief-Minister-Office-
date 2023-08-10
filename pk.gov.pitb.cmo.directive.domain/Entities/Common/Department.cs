using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common
{
    [Table("Departments", Schema = "Common")]
    public class Department : BaseTableColumns
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string? Abbreviation { get; set; }

        [ForeignKey("DepartmentType")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int DepartmentTypeId { get; set; }
        public string? Address { get; set; }
        public bool IsInitiator { get; set; }
        public string? PhoneNumbers { get; set; }
        public string? EmailAddresses { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("Office")]
        public int? OfficeId { get; set; }

        public virtual DepartmentType DepartmentType { get; set; }

        public virtual Office Office { get; set; }

        [JsonIgnore]
        public virtual List<UserDepartments> UserDepartments { get; set; }

    }
}
