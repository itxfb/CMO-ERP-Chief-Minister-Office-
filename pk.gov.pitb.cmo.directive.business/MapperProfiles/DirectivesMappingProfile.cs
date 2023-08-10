using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;

namespace pk.gov.pitb.cmo.directive.business.MapperProfiles
{
    public class DirectivesMappingProfile : Profile
    {
        public DirectivesMappingProfile()
        {


            CreateMap<Directive, DirectiveInstruction>().ReverseMap();
            CreateMap<DirectiveEvent, DirectiveEventVm>().ReverseMap();
            
            CreateMap<Letter, LetterAddVm>().ReverseMap();


            CreateMap<Response, ResponseVM>().ReverseMap();




        }
    }   
}
