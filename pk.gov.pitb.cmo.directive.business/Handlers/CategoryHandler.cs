using pk.gov.pitb.cmo.directive.domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class CategoryHandler : BaseHandler
    {
        

        public CategoryHandler(IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal,IMapper mapper) : base (unitOfWork, claimsPrincipal, mapper)
        {
            

        }


        public async Task<List<Category>> GetAllAsync()
        {
            var result= await _unitOfWork.GetRepositoryAsync<Category>().Get(x => !x.IsDeleted);
            return result.ToList();
        }

        public async Task<Category> GetAsync(int id)
        {
            return await _unitOfWork.GetRepositoryAsync<Category>().GetOne(x=>x.Id == id && !x.IsDeleted);
            
        }

        public async Task<IdObject> AddAsync(CategoryAddVm vm)
        {
            
            Category c = new Category()
            {
                
                Name = vm.Name,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                CreatedBy = base.GetUserId(),

            };

            var res = await _unitOfWork.GetRepositoryAsync<Category>().GetOne(a => a.Name == vm.Name);
            if (res == null)
                c = await _unitOfWork.GetRepositoryAsync<Category>().Insert(c);
            else
                throw new Exception("Category Already Exist");

            return new IdObject(c.Id);
        }


        public async Task Update(CategoryAddVm vm)
        {
            Category c = await _unitOfWork.GetRepositoryAsync<Category>().GetOne(a=> a.Id==vm.Id);
            if (c != null)
            {
                c.Name = vm.Name;
                c.UpdatedAt= DateTime.Now;
                c.UpdatedBy = base.GetUserId();
                await _unitOfWork.GetRepositoryAsync<Category>().Update(c.Id,c);
               
            }
        }


        public async Task Delete(CategoryAddVm vm)
        {
            Category c = await _unitOfWork.GetRepositoryAsync<Category>().GetOne(a=>a.Id==vm.Id);
            if (c != null)
            {
                
                c.DeletedAt = DateTime.Now;
                c.DeletedBy = base.GetUserId();
                c.IsDeleted = true;
                await _unitOfWork.GetRepositoryAsync<Category>().Update(c.Id, c,false,false);
            }
        }
    }
}
