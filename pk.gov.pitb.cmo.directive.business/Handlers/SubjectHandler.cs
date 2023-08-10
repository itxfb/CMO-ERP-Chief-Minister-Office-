using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class SubjectHandler : BaseHandler
    {
        public SubjectHandler(IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal,IMapper mapper) : base(unitOfWork, claimsPrincipal, mapper)
        {

        }



        public async Task<List<Subject>> GetAllAsync(int? skip, int? take)
        {
            var subjects = default(IEnumerable<Subject>);
            if(skip!=-1)
            {
                subjects = await _unitOfWork.GetRepositoryAsync<Subject>().GetSkipTake(a=>!a.IsDeleted,skip.Value,take.Value);
            }
            else
            {
                subjects = await _unitOfWork.GetRepositoryAsync<Subject>().Get(a => !a.IsDeleted);

            }
            if (subjects.Count<Subject>()!=0)
                subjects.First().TotalCount = await _unitOfWork.GetRepositoryAsync<Subject>().GetCount(a=>!a.IsDeleted);

            return subjects.ToList();
        }

        public async Task<IdObject> AddAsync(SubjectVm vm)
        {

            Subject s = new Subject()
            {
               
                Code = vm.Code,
                Description= vm.Description,

            };
            var record = await _unitOfWork.GetRepositoryAsync<Subject>().GetOne(a => a.Code == vm.Code);
            if (record == null)
                s = await _unitOfWork.GetRepositoryAsync<Subject>().Insert(s);
            else
                throw new Exception("Record Already Exist");

            return new IdObject(s.Id);
        }


        public async Task UpdateAsync(SubjectVm vm)
        {
            Subject c = await _unitOfWork.GetRepositoryAsync<Subject>().GetOne(a => a.Id == vm.Id);
            if (c != null)
            {
                c.Code = vm.Code;
                c.Description= vm.Description;
                await _unitOfWork.GetRepositoryAsync<Subject>().Update(c.Id, c);
            }
        }


        public async Task DeleteAsync(SubjectVm vm)
        {
            Subject c = await _unitOfWork.GetRepositoryAsync<Subject>().GetOne(a => a.Id == vm.Id);
            if (c != null)
            {
                c.DeletedAt = DateTime.Now;
                c.DeletedBy = base.GetUserId();
                c.IsActive = false;

                await _unitOfWork.GetRepositoryAsync<Subject>().Update(c.Id, c,false,false);
            }
        }


    }
}
