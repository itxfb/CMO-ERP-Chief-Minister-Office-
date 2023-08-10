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
using pk.gov.pitb.cmo.directive.domain.Models;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;

namespace pk.gov.pitb.cmo.directive.domain.Entities.eDirectives
{
    [Table("Directives", Schema = "eDirectives")]
    public class Directive : BaseTableColumns
    {
        [ForeignKey("Letter")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? LetterId { get; set; }

        [ForeignKey("Department")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int DepartmentId { get; set; }
        public string? SubDepartmentName { get; set; }

        [ForeignKey("Subject")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int SubjectCodeId { get; set; }
        public string? SubjectText { get; set; }
        public DateTime TimeLine { get; set; }
        public string ApplicantName { get; set; }
        public double? EstimatedCost { get; set; }
        public double? AllocatedCost { get; set; }
        public string? SchemeMinutesOfMeeting { get; set; }

        //0 is pending with department //1 is pending with CMO //2 is implemented // 3 is re-sent to department for follow up //4 is disposed //5 is regretted

     
        [JsonConverter(typeof(EncryptIdConverter))]
        public int StatusId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("DirectiveTypes")]
        public int? DirectiveTypeId { get; set; }


        [ForeignKey("NatureOfScheme")]
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? NatureOfSchemeId { get; set; }
        public string? AdpNumber { get; set; }

        [JsonIgnore]
        public virtual Letter Letter { get; set; }
        public virtual NatureOfSchemes? NatureOfScheme { get; set; }
        public virtual Department Department { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual List<Response> Response { get; set; }

        //public virtual DirectiveStatus DirectiveStatus { get; set; }
        public virtual DirectiveType DirectiveTypes { get; set; }




       

    }
}
