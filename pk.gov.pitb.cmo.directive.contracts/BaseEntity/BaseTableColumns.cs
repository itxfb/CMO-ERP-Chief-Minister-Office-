using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;

namespace pk.gov.pitb.cmo.directive.domain.Data
{
    public class BaseTableColumns
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int Id { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set;}
        public bool IsDeleted { get; set; } = false;
        public int? DeletedBy { get; set; }

        [NotMapped]
        public long? TotalCount { get; set; }
    }
}
