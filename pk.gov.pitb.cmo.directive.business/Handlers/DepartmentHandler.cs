using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using AutoMapper;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class DepartmentHandler : BaseHandler
    {
        public DepartmentHandler(IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal,IMapper mapper) : base(unitOfWork, claimsPrincipal,mapper)
        {

        }

        public List<Department> GetAll()
        {
            return _unitOfWork.GetRepositoryAsync<Department>().GetAllQueryable().Where(x => x.IsDeleted==false && x.ParentId == null)
                .Include(a=>a.DepartmentType)
                .ToList();
        }

        public async Task<IdObject> AddAsync(DepartmentVm vm)
        {

            Department c = new Department()
            {
                Name = vm.Name,
                DepartmentTypeId = vm.DepartmentTypeId.Value,
                Address = vm.Address,
                PhoneNumbers = vm.PhoneNumbers,
                EmailAddresses = vm.EmailAddresses,
                Abbreviation = vm.Abbreviation,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                CreatedBy = base.GetUserId(),


            };


            var record = await _unitOfWork.GetRepositoryAsync<Department>().GetOne(a => a.Name == vm.Name);
            if (record == null)
                c = await _unitOfWork.GetRepositoryAsync<Department>().Insert(c);
            else
                throw new Exception("Record Already Exist");

            return new IdObject(c.Id);
        }


        public async Task Update(DepartmentVm vm)
        {
            Department c = await _unitOfWork.GetRepositoryAsync<Department>().GetOne(x=> x.Id == vm.Id.Value && x.IsDeleted == false);
            if (c != null)
            {
                c.Name = vm.Name;
                c.DepartmentTypeId = vm.DepartmentTypeId.Value;
                c.Address = vm.Address;
                c.PhoneNumbers = vm.PhoneNumbers;
                c.EmailAddresses = vm.EmailAddresses;
                c.Abbreviation = vm.Abbreviation;
                c.UpdatedAt = DateTime.Now;
                c.UpdatedBy = base.GetUserId();

                await _unitOfWork.GetRepositoryAsync<Department>().Update(c.Id,c);
            }
        }


        public async Task Delete(DepartmentVm vm)
        {
            Department c = await _unitOfWork.GetRepositoryAsync<Department>().GetOne(x => x.Id == vm.Id.Value && x.IsDeleted == false);
            if (c != null)
            {
                DateTime now = DateTime.Now;
                c.IsDeleted = true;
                c.DeletedAt = now;
                c.DeletedBy = base.GetUserId();
                await _unitOfWork.GetRepositoryAsync<Department>().Update(c.Id, c, false, false);
            }
        }
    }
}
