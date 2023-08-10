using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Models;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common
{
    [Table("Attachments", Schema = "Common")]
    public class Attachment : BaseTableColumns
    {

        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("AttachmentTypes")]
        public int AttachmentTypeId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int AttachmentId { get; set; }
        public string DisplayName { get; set; }
        public string FileExtension { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }

      
        public virtual AttachmentType AttachmentTypes { get; set; }


    }
}
