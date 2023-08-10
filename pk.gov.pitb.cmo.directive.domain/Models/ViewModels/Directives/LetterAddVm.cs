using Microsoft.AspNetCore.Http;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives
{
    public class LetterAddVm : CountClass
    {

        [JsonConverter(typeof(EncryptIdConverter))]

        public int? Id { get; set; }
        public DateTime ComputerNumberImprintDate { get; set; }
        public string ComputerNumber { get; set; }
        public DateTime LetterDate { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int OfficeId { get; set; }
        public string? Dictation { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? ConstituenceyId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? PriorityId { get; set; }
        public string? DiaryNumber { get; set; }
        public string? LetterNumber { get; set; }
        public bool? ActionByIC { get; set; }
        public bool? IsSentToMinister { get; set; }
        public string? LetterType { get; set; }
        public string? LetterStatus { get; set; }
        public string? DictationText { get; set; }
        public string? OfficeName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? SubjectId { get; set; }
        public string? SubjectDescription { get; set; }

        [JsonIgnore]
        public IFormFileCollection? LetterAttachments { get; set; }

        public List<DirectiveInstruction>? DirectiveInstructions { get; set; }
    }

    public class LetterAttachementVm : CountClass
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string DisplayName { get; set; }
        public string FileExtension { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
    }


    public class DirectiveInstruction : CountClass
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? Id { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? LetterId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int DepartmentId { get; set; }

        public string? SubDepartmentName { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int SubjectCodeId { get; set; }
        public string? SubjectText { get; set; }
        public DateTime TimeLine { get; set; }
        public string ApplicantName { get; set; }
        public double? EstimatedCost { get; set; }
        public double? AllocatedCost { get; set; }
        
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? StatusId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int DirectiveTypeId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int? NatureOfSchemeId { get; set; }
        public string? SchemeMinutesOfMeeting { get; set; }
        public string? AdpNumber { get; set; }
        public virtual NatureOfSchemes? NatureOfScheme { get; set; }
        public virtual Department? Department { get; set; }
        public virtual Subject? Subject { get; set; }


    }


    public class CountClass : IdObject
    {
        public long? TotalCount { get; set; }
    }
    public class GenericLetterVM: CountClass
    {
        public DateTime ComputerNumberImprintDate { get; set; }
        public string ComputerNumber { get; set; }

       
    }
  
    public class LetterVM : CountClass
    {

        
        public string ComputerNumberImprintDate { get; set; }
        public string ComputerNumber { get; set; }
        public string LetterNumber { get; set; }
        public string LetterDate { get; set; }
        public string DiaryNumber { get; set; }
        public string ActionByIC { get; set; }
        public string IsSentToM { get; set; }
        public string Type { get; set; } //type
        public string Status { get; set; }
        public string Office { get; set; }
        public string SubjectCode { get; set; }
        public string Prority { get; set; }
        public string Constituency { get; set; }
        public virtual IEnumerable<Attachment?> Documents { get; set; }
        public DirectiveCountVM DirectiveCounts { get; set; }


    }

    public class DirectiveInstructionDTO : DirectiveInstruction
    {
        public string DepartmentName { get; set; }

        public string SubjectCodeName { get; set; }

        public string NatureOfSchemeName { get; set; }

        public string TimelineDays { get; set; }
        public string Status { get; set; }

        public string TypeName { get; set; }

        
      

        


    }




}
