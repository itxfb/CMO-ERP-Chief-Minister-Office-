using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;
using pk.gov.pitb.cmo.directive.domain.Models.Common;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Account;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;

namespace pk.gov.pitb.cmo.directive.business.MapperProfiles
{
    public class UserProfilesMappingProfile: Profile
    {
        public UserProfilesMappingProfile()
        {
            CreateMap<AppUser, AddUserProfileVm>().ReverseMap();
            CreateMap<AppUser, AppUserModel>().ReverseMap();
            CreateMap<AppUser, UserListVM>().ReverseMap();
           
        }
    }
}
