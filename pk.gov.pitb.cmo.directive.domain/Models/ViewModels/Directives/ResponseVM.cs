using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives
{
    public class ResponseVM : IdObject
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int DirectiveId { get; set; }

        public string ResponseText { get; set; }

        public string DocumentDescription { get; set; }

        public string ResponseRemarks { get; set; }

        public string ResponseFileType { get; set; }

        [NotMapped]
        public virtual IFormFileCollection DocumentFiles { get; set; }

        [NotMapped]

        public virtual List<AttachmentVM> ResponseAttachments { get; set; }


        [NotMapped]

        public virtual List<AttachmentVM> CMOAttachments { get; set; }

        [NotMapped]

        public long? TotalCount { get; set; }
    }
}
