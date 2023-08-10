using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Entities.Common
{
    [Table("AttachmentTypes", Schema = "Common")]
    public class AttachmentType : BaseTableColumns
    {
        public string Name { get; set; }

        public string Abbreviation { get; set; }

    }
}
