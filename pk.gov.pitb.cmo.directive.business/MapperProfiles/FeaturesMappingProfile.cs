using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Models.Common;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;

namespace pk.gov.pitb.cmo.directive.business.MapperProfiles
{
    public class FeaturesMappingProfile : Profile
    {
        public FeaturesMappingProfile()
        {


            CreateMap<Feature, FeatureVm>().ReverseMap();
            CreateMap<Feature, FeatureAddVm>().ReverseMap();
        }
    }
}
