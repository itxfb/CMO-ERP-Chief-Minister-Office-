using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;

namespace pk.gov.pitb.cmo.directive.domain.Entities.eDirectives
{
    [Table("Responses",Schema="eDirectives")]
    public class Response : BaseTableColumns
    {

        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("Directive")]
        public int DirectiveId { get; set; }

        public string ResponseText { get; set; }

        public string? DocumentDescription { get; set; }

        // not reviewed 0 // reviewed 1
        [JsonConverter(typeof(EncryptIdConverter))]
        public int? StatusId { get; set; } = 0;
        public string? ResponseRemarks { get; set; }

        [NotMapped]
        public virtual List<IFormFile> DocumentFiles { get; set; }

        [JsonIgnore]
        public virtual Directive Directive { get; set; }



    }
}
