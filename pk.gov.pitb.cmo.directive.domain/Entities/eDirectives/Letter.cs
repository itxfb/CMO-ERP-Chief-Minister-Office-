using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Entities.eDirectives
{
    [Table("Letters", Schema = "eDirectives")]
    public class Letter : BaseTableColumns
    {
        public DateTime ComputerNumberImprintDate { get; set; }
        public string ComputerNumber { get; set; }
        public DateTime LetterDate { get; set; }

        public string? Dictation { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("Office")]
        public int? OfficeId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("Constituency")]
        public int? ConstituenceyId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("Priority")]
        public int? PriorityId { get; set; }
        public string? DiaryNumber { get; set; }
        public string? LetterNumber { get; set; }
        public bool? ActionByIC { get; set; }
        public bool? IsSentToMinister { get; set; }
        public string? LetterType { get; set; }
        public string? LetterStatus { get; set; }
        public string? DictationText { get; set; }
        public string? OfficeName { get; set; }
        public string? SubjectDescription { get; set; }

        public virtual Office Office { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Constituency Constituency { get; set; }
      
        

        public virtual List<Directive> Directives { get; set; }
        public virtual List<Attachment> Attachments { get; set; }


        [NotMapped]
        public DirectiveCountVM DirectiveCounts { get; set; }

    }
}
