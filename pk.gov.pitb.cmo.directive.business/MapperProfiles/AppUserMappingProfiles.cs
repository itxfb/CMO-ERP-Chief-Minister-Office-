using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Models.Common;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;

namespace pk.gov.pitb.cmo.directive.business.MapperProfiles
{
    public class AppUserMappingProfiles : Profile
    {
        public AppUserMappingProfiles()
        {
         

            CreateMap<AppUser, AppUserModel>().ReverseMap();
            CreateMap<AppUser, ProfileListVm>().ReverseMap();
            CreateMap<UserType, ProfileTypesVm>().ReverseMap();
        }
    }
}
