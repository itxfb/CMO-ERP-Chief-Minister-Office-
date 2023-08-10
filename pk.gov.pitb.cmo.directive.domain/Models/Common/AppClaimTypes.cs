using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pk.gov.pitb.cmo.directive.domain.Models.Common
{
    public enum AppClaimTypes : byte
    {
        Id= 1,
        DisplayName,
        Role,
        Level,
        ProvinceId,
        DistrictId,
        DepartmentId,

    }
}
