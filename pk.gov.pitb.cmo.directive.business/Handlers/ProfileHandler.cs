using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using System.Security.Claims;
using pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;
using pk.gov.pitb.cmo.directive.domain.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class ProfileHandler : BaseHandler
    {

        public ProfileHandler(IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal, IMapper mapper) : base(unitOfWork, claimsPrincipal, mapper)
        {


        }
        public async Task<List<AppUserModel>> GetAllAsync()
        {
            var result =  _unitOfWork.GetRepositoryAsync<AppUser>().GetAllQueryable().Where(x => !x.IsDeleted && x.IsActive); //for profiles only excluding login users//  && x.UserTypeId!=1

            var profiles = await result.Include(a => a.District).Include(a => a.Designation).Include(a=>a.UserType).Include(a=>a.UserRoles).Include(a => a.UserDepartments).ThenInclude(a => a.Department).ToListAsync();
            return _mapper.Map<List<AppUserModel>>(profiles);
        }

        public async Task<AppUserModel> GetAsync(int id)
        {
            var result = await _unitOfWork.GetRepositoryAsync<AppUser>().GetOne(a => a.Id == id);

            return _mapper.Map<AppUserModel>(result);
             
        }

        public async Task<IdObject> AddAsync(AddUserProfileVm vm)
        {

            AppUser c = _mapper.Map<AppUser>(vm);

            c.Id = 0;
           
            c.IsActive = true;
            c.CreatedAt = DateTime.Now;
            c.CreatedBy = base.GetUserId();

            if (c.ConstituencyId == -1)
                c.ConstituencyId = null;

            if (c.PartyId == -1)
                c.PartyId = null;

            var record = await _unitOfWork.GetRepositoryAsync<AppUser>().GetOne(a => a.Email== vm.Email);
            if (record == null)
                c = await _unitOfWork.GetRepositoryAsync<AppUser>().Insert(c);
            else
                throw new Exception("Record Already Exist");

            if(c.Id!=0 )
            {
                await _unitOfWork.GetRepositoryAsync<UserDepartments>().Insert(new UserDepartments() { UserId = c.Id, DepartmentId = vm.DepartmentId.Value });

                if(vm.RoleId != -1)
                    await _unitOfWork.GetRepositoryAsync<UserRoles>().Insert(new UserRoles { UserId = c.Id, RoleId = vm.RoleId.Value });
            }

           

            return new IdObject(c.Id);
        }

        public async Task UpdateAsync(AddUserProfileVm vm)
        {
            AppUser f = await _unitOfWork.GetRepositoryAsync<AppUser>().GetOne(a => a.Id == vm.Id);
            if (f != null)
            {

                f = _mapper.Map<AppUser>(vm);

                if (f.ConstituencyId == -1)
                    f.ConstituencyId = null;

                if (f.PartyId == -1)
                    f.PartyId = null;

                f.UpdatedAt = DateTime.Now;
                f.UpdatedBy = base.GetUserId();
                await _unitOfWork.GetRepositoryAsync<AppUser>().Update(f.Id, f);

            }
        }

        public async Task DeleteAsync(AddUserProfileVm vm)
        {
            AppUser f = await _unitOfWork.GetRepositoryAsync<AppUser>().GetOne(a => a.Id == vm.Id);

            if(f.Id == GetUserId())
            {
                throw new InvalidOperationException("Logged In User Cannot be Deleted");
            }
            if (f != null)
            {

                f.DeletedAt = DateTime.Now;
                f.DeletedBy = base.GetUserId();
                f.IsDeleted = true;
                await _unitOfWork.GetRepositoryAsync<AppUser>().Update(f.Id, f, false, false);
            }
        }
    }
}
