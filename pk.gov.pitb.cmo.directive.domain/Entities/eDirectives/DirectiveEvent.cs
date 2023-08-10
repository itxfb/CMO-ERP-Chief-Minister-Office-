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

namespace pk.gov.pitb.cmo.directive.domain.Entities.eDirectives
{
    [Table("DirectiveEvent", Schema = "eDirectives")]
    public class DirectiveEvent : BaseTableColumns
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int TypeId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        public int DirectiveId { get; set; }

        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("Sender")]
        public int SenderId { get; set; }
       
        [JsonConverter(typeof(EncryptIdConverter))]
        [ForeignKey("Reciever")]
        public int RecieverId { get; set; }
       
        [JsonConverter(typeof(EncryptIdConverter))]
        public int StatusId { get; set; }

        public string? DocumentNumber { get; set; }

        public string? Remarks { get; set; }

        public virtual AppUser? Sender { get; set; }
        public virtual AppUser? Reciever { get; set; }


    }
}
