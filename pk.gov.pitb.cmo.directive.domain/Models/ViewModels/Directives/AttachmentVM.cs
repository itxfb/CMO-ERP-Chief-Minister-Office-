using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives
{
    public class AttachmentVM : IdObject
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int AttachmentTypeId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int AttachmentId { get; set; }
        public string Path { get; set; }

        public string DisplayName { get; set; }
    }
}
