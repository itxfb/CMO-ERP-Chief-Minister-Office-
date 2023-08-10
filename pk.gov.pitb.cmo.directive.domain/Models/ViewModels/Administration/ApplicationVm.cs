using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration
{
    public class ApplicationVm :  BaseTableColumns
    {
        [JsonConverter(typeof(EncryptIdConverter))]
        public int ApplicationTypeId { get; set; }
        public string ApplicationName { get; set; }
    }
}
