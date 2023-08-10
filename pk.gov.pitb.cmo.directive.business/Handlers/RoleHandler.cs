using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pk.gov.pitb.cmo.directive.domain.Exceptions;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class RoleHandler : BaseHandler
    {

     
        public RoleHandler(IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal,IMapper mapper) : base(unitOfWork, claimsPrincipal,mapper)
        {
           

        }


        public async Task<List<Role>> GetAllAsync()
        {
            var result = await _unitOfWork.GetRepositoryAsync<Role>().Get(a=>!a.IsDeleted);
            return result.ToList();
        }

        public async Task<IdObject> AddAsync(UserRoleAddEditVm vm)
        {

            Role r = new Role()
            {
                //Id = vm.Id,
                Name = vm.Name,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                CreatedBy = base.GetUserId(),

            };
            var record = await _unitOfWork.GetRepositoryAsync<Role>().GetOne(a => a.Name == vm.Name);
            if (record == null)
                r = await _unitOfWork.GetRepositoryAsync<Role>().Insert(r);
            else
                throw new Exception("Record Already Exist");

            return new IdObject(r.Id);
        }


        public async Task UpdateAsync(UserRoleAddEditVm vm)
        {
            Role r = await _unitOfWork.GetRepositoryAsync<Role>().GetOne(a => a.Id == vm.Id);
            if (r != null)
            {
                r.Name = vm.Name;
                r.UpdatedAt = DateTime.Now;
                r.UpdatedBy = base.GetUserId();
                await _unitOfWork.GetRepositoryAsync<Role>().Update(r.Id, r);
            }
        }


        public async Task DeleteAsync(UserRoleAddEditVm vm)
        {
            Role r = await _unitOfWork.GetRepositoryAsync<Role>().GetOne(a => a.Id == vm.Id);
            if (r != null)
            {

                r.DeletedAt = DateTime.Now;
                r.DeletedBy = base.GetUserId();
                r.IsDeleted = true;
                await _unitOfWork.GetRepositoryAsync<Role>().Update(r.Id, r,false,false);
            }
        }
      
    }
}
