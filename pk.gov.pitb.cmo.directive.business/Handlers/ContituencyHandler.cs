using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class ConstituencyHandler : BaseHandler
    {
        public ConstituencyHandler(IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal,IMapper mapper) : base(unitOfWork, claimsPrincipal,mapper)
        {
        }

     


        public List<Constituency> GetAll()
        {
            return _unitOfWork.GetRepositoryAsync<Constituency>().GetAllQueryable() 
                .Include(x => x.District)
                .Where(x => x.IsDeleted == false).ToList();
        }

        public async Task<IdObject> AddAsync(ConstituencyVm vm)
        {

            Constituency c = new Constituency()
            {
                Name = vm.Name,
                ConstituencyTypeId = vm.ConstituencyTypeId,
                Code = vm.Code,
                DistrictId = vm.DistrictId,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                CreatedBy = base.GetUserId(),


            };


            c = await _unitOfWork.GetRepositoryAsync<Constituency>().Insert(c);
           

            return new IdObject(c.Id);
        }


        public async Task UpdateAsync(ConstituencyVm vm)
        {

            Constituency c = await _unitOfWork.GetRepositoryAsync<Constituency>().GetOne(x => x.Id == vm.Id && x.IsDeleted == false);
            if (c != null)
            {
                c.Name = vm.Name;
                c.ConstituencyTypeId = vm.ConstituencyTypeId;
                c.Code = vm.Code;
                c.DistrictId = vm.DistrictId;
                c.UpdatedAt = DateTime.Now;
                c.UpdatedBy = base.GetUserId();
                await _unitOfWork.GetRepositoryAsync<Constituency>().Update(c.Id,c);
            }
        }


        public async Task DeleteAsync(IdObject vm)
        {
            Constituency c = await _unitOfWork.GetRepositoryAsync<Constituency>().GetOne(x => x.Id == vm.Id && x.IsDeleted == false);
            if (c != null)
            {
               
                c.IsDeleted = true;
                c.DeletedAt = DateTime.Now;
                c.DeletedBy = base.GetUserId();
                await _unitOfWork.GetRepositoryAsync<Constituency>().Update(c.Id, c, false, false);
            }
        }
    }
}
