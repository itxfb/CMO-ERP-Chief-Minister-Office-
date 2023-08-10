using Microsoft.AspNetCore.Http;
using pk.gov.pitb.cmo.contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives
{
    public class DirecivePostData
    {
        public DirectiveAddVm data { get; set; }

        public List<IFormFile>? files { get; set; }
    }

    public class DirectiveAddVm : IdObject
    {

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
        //  public List<DirectiveAttachementVm>? ListAttachments { get; set; }

        // public List<DirectiveInstruction>? DirectiveInstructions { get; set; }
    }

    //public class DirectiveAttachementVm
    //{
    //    [JsonConverter(typeof(EncryptIdConverter))]
    //    public int Id { get; set; }
    //    [JsonConverter(typeof(EncryptIdConverter))]
    //    public int LetterId { get; set; }
    //    public string KeyName { get; set; }
    //    public string DisplayName { get; set; }
    //    public string FileExtension { get; set; }
    //    public string ContentType { get; set; }
    //    public string Path { get; set; }
    //}


    //public class DirectiveInstruction
    //{
    //    public string? Id { get; set; }
    //    public string ApplicantName { get; set; }
    //    public string TimelineDays { get; set; }
    //    public string Timeline { get; set; }
    //    public string SubjectId { get; set; }
    //    public string SubjectTitle { get; set; }
    //    public string SubjectDescription { get; set; }
    //    public string DepartmentId { get; set; }
    //    public string? DepartmentName { get; set; }
    //    public string SubDepartmentName { get; set; }
    //    public string Description { get; set; }
    //    public double EstimatedCost { get; set; }
    //    // public string itemNo { get; set; }
    //    public string? SubjectCode { get; set; }
    //    public double AllocatedCost { get; set; }
    //    public string MinutesOfMeeting { get; set; }
    //    public string NatureOfScheme { get; set; }
    //    public string AdpNo { get; set; }
    //}


}
