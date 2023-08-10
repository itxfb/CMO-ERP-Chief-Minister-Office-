using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives
{
    public class DirectiveEventVm : IdObject
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int TypeId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int DirectiveId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int SenderId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int RecieverId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int StatusId { get; set; }

        public string? Sender { get; set; }

        public string? Reciever { get; set; }
        public string? DocumentNumber { get; set; }
        public string? Remarks { get; set; }

        public string? Type { get; set; }

        [NotMapped]
        public long? TotalCount { get; set; }
    }
}
