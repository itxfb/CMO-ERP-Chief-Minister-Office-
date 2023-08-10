using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration
{
    public class DepartmentVm
    {
        public DepartmentVm()
        {
            this.SubDepartments = new List<DepartmentVm>();
        }
        
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? Id { get; set; }
        
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? DepartmentTypeId { get; set; }
        
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumbers { get; set; }
        public string? EmailAddresses { get; set; }
        public string? Abbreviation { get; set; }




        public List<DepartmentVm> SubDepartments { get; set; }

        public FocalPersonOrRepresentative? DepartmentFocalPerson { get; set; }
        public FocalPersonOrRepresentative? DepartmentRepresentative { get; set; }
        

    }

    public class FocalPersonOrRepresentative
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? Id { get; set; }
        [JsonConverter(typeof(EncryptIdConverter))]

        public int? DepartmentId { get; set; }
        [JsonConverter(typeof(EncryptIdConverter))]

        public int? ProfileId { get; set; }

        public bool? IsActive { get; set; }
        public bool IsSignatory { get; set; }

    }
}
