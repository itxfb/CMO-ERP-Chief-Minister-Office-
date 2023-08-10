using System.Security.Claims;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class PriorityHandler : BaseHandler
    {


        
        public PriorityHandler( IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal,IMapper mapper) : base(unitOfWork, claimsPrincipal,mapper)
        {


        }


        public async Task<List<Priority>> GetAllAsync()
        {

            var result = await _unitOfWork.GetRepositoryAsync<Priority>().Get(x => !x.IsDeleted);
            return result.ToList();
        }

        public async Task<Priority> GetAsync(int id)
        {
            return await _unitOfWork.GetRepositoryAsync<Priority>().GetOne(a => a.Id == id);
        }
        public async Task<IdObject> AddAsync(PriorityAddVm vm)
        {
            Priority c = new Priority()
            {
                Id = vm.Id,
                Name = vm.Name,
                ColorHexCode = vm.ColorHexCode,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                CreatedBy = base.GetUserId(),

            };
            var record = await _unitOfWork.GetRepositoryAsync<Priority>().GetOne(a => a.Name == vm.Name || a.ColorHexCode == vm.ColorHexCode);
            if (record == null)
                c = await _unitOfWork.GetRepositoryAsync<Priority>().Insert(c);
            else
                throw new Exception("Record Already Exist");


            return new IdObject(c.Id);
        }


        public async Task UpdateAsync(PriorityAddVm vm)
        {
            Priority c = await _unitOfWork.GetRepositoryAsync<Priority>().GetOne(a => a.Id == vm.Id);
            if (c != null)
            {
                c.Name = vm.Name;
                c.ColorHexCode = vm.ColorHexCode;
                c.UpdatedAt = DateTime.Now;
                c.UpdatedBy = base.GetUserId();
                await _unitOfWork.GetRepositoryAsync<Priority>().Update(c.Id,c);
               
            }
        }


        public async Task Delete(PriorityAddVm vm)
        {
            Priority c = await _unitOfWork.GetRepositoryAsync<Priority>().GetOne(a => a.Id == vm.Id);
            if (c != null)
            {
                c.DeletedAt = DateTime.Now;
                c.DeletedBy = base.GetUserId();
                c.IsDeleted = true;
                await _unitOfWork.GetRepositoryAsync<Priority>().Update(c.Id, c,false,false);
            }
        }
    }
}
