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
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class ApplicationHandler : BaseHandler
    {

       
        public ApplicationHandler(IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal, IMapper mapper) : base(unitOfWork, claimsPrincipal,mapper)
        {
            
        }


        public List<Application> GetAll()
        {
            return _unitOfWork.GetRepositoryAsync<Application>().GetAllQueryable().Where(x => !x.IsDeleted).ToList();
        }

        public async Task<Application> GetAsync(int id)
        {
            return await _unitOfWork.GetRepositoryAsync<Application>().GetOne(a => a.Id == id);
        }

        public async Task<IdObject> AddAsync(ApplicationVm vm)
        {

            Application app = _mapper.Map<Application>(vm);
            app.CreatedAt = DateTime.Now;
            app.CreatedBy = base.GetUserId();


            app = await _unitOfWork.GetRepositoryAsync<Application>().Insert(app);
           

            return new IdObject(app.Id);
        }


        public async Task Update(ApplicationVm vm)
        {
            Application app = await _unitOfWork.GetRepositoryAsync<Application>().GetOne(a=>a.Id == vm.Id);
            if (app != null)
            {
                app.ApplicationName = vm.ApplicationName;
                app.ApplicationTypeId = vm.ApplicationTypeId;
                app.UpdatedAt = DateTime.Now;
                app.UpdatedBy = base.GetUserId();
                await _unitOfWork.GetRepositoryAsync<Application>().Update(app.Id,app);
            }
        }


        public async Task Delete(ApplicationVm vm)
        {
            Application app = await _unitOfWork.GetRepositoryAsync<Application>().GetOne(a => a.Id == vm.Id);
            if (app != null)
            {

                app.DeletedAt = DateTime.Now;
                app.DeletedBy = base.GetUserId();
                app.IsDeleted = true;
                await _unitOfWork.GetRepositoryAsync<Application>().Update(app.Id, app, false, false);
            }
        }
    }
}
