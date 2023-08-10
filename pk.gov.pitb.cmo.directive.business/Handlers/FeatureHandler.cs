using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class FeatureHandler : BaseHandler
    {
       

        public FeatureHandler(IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal,IMapper mapper) : base(unitOfWork, claimsPrincipal,mapper)
        {

            
        }


        public async Task<List<Feature>> GetAllAsync()
        {
            var result= await _unitOfWork.GetRepositoryAsync<Feature>().Get(x => !x.IsDeleted && x.IsActive);

            return result.ToList();
        }

        public async Task<Feature> GetAsync(int id)
        {
            return await _unitOfWork.GetRepositoryAsync<Feature>().GetOne(a => a.Id == id);
        }

        public async Task<IdObject> AddAsync(FeatureAddVm vm)
        {

            Feature c = _mapper.Map<Feature>(vm);

            if (c.ParentId == -1)
                c.ParentId = null;

            c.IsActive = true;
            c.CreatedAt = DateTime.Now;
            c.CreatedBy = base.GetUserId();
           
            var record = await _unitOfWork.GetRepositoryAsync<Feature>().GetOne(a => a.Name == vm.Name || a.DisplayName== vm.DisplayName);
            if (record == null)
                c = await _unitOfWork.GetRepositoryAsync<Feature>().Insert(c);
            else
                throw new Exception("Record Already Exist");

            return new IdObject(c.Id);
        }


        public async Task UpdateAsync(FeatureAddVm vm)
        {
            Feature f = await _unitOfWork.GetRepositoryAsync<Feature>().GetOne(a=>a.Id==vm.Id);
            if (f != null)
            {
                f.Name = vm.Name;
                f.DisplayName = vm.DisplayName;
                f.Path = vm.Path;
                

                if (vm.ParentId!=-1)
                f.ParentId = vm.ParentId;

               
               
                f.UpdatedAt = DateTime.Now;
                f.UpdatedBy = base.GetUserId();
                await _unitOfWork.GetRepositoryAsync<Feature>().Update(f.Id,f);
               
            }
        }


        public async Task DeleteAsync(FeatureAddVm vm)
        {
            Feature f = await _unitOfWork.GetRepositoryAsync<Feature>().GetOne(a=>a.Id==vm.Id);
            if (f != null)
            {

                f.DeletedAt = DateTime.Now;
                f.DeletedBy = base.GetUserId();
                f.IsDeleted = true;
                await _unitOfWork.GetRepositoryAsync<Feature>().Update(f.Id, f,false,false);
            }
        }
    }
}
