using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Exceptions;
using pk.gov.pitb.cmo.directive.domain.Models.Common;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using AutoMapper;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class UserManageHandler : BaseHandler
    {
        public UserManageHandler(IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal, IMapper mapper) : base(unitOfWork, claimsPrincipal, mapper)
        {

        }

        public string GetAllFeaturesForUsermanagmentModule(int id)
        {
            var features = _unitOfWork.GetRepositoryAsync<Feature>().GetAllQueryable().Where(x => !x.IsDeleted && x.IsActive).Include(a => a.RoleFeatures.Where(a => a.RoleId == id && a.IsActive == true))
               .ToList();


            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return System.Text.Json.JsonSerializer.Serialize(features, options);
        }


        public async Task UpdateRolePermission(UpdateRolePermissionVm model)
        {
            var role = await _unitOfWork.GetRepositoryAsync<RoleFeaturePermission>().Get(a => a.RoleId == model.RoleId && !a.IsDeleted);
            if (role == null) throw new GeneralException("Provided role doesn't exist in system");

            foreach (var roleP in model.ListPermissions)
            {
                if (role.Any())
                {
                    RoleFeaturePermission p = role.FirstOrDefault(x => x.FeatureId == roleP.Id);
                    if (p == null)
                    {
                        //If parent of that feature does not exist within the permissions before
                        var parent = await _unitOfWork.GetRepositoryAsync<Feature>().GetOne(a => a.Id == roleP.Id);
                        var checkParent = await _unitOfWork.GetRepositoryAsync<RoleFeaturePermission>().GetOne(a => a.RoleId == model.RoleId && !a.IsDeleted && a.FeatureId == parent.ParentId.Value);
                        if (checkParent== null)
                        {
                            await _unitOfWork.GetRepositoryAsync<RoleFeaturePermission>().Insert(new RoleFeaturePermission() { ApplicationId = parent.ApplicationId.Value, FeatureId = parent.ParentId.Value, RoleId = model.RoleId, IsActive = roleP.IsEnabled, CreatedBy = GetUserId() });
                        }
                        await _unitOfWork.GetRepositoryAsync<RoleFeaturePermission>().Insert(new RoleFeaturePermission() { ApplicationId = roleP.ApplicationId, FeatureId = roleP.Id, RoleId = model.RoleId, IsActive = roleP.IsEnabled, CreatedBy = GetUserId() });
                    }

                    else
                    {
                        p.IsActive = roleP.IsEnabled;
                        await _unitOfWork.GetRepositoryAsync<RoleFeaturePermission>().Update(p.Id, p);
                    }
                }
                else
                {
                    //If parent of that feature does not exist within the permissions before
                    var parent = await _unitOfWork.GetRepositoryAsync<Feature>().GetOne(a => a.Id == roleP.Id);
                    var checkParent = await _unitOfWork.GetRepositoryAsync<RoleFeaturePermission>().GetOne(a => a.RoleId == model.RoleId && !a.IsDeleted && a.FeatureId == parent.ParentId.Value);
                    if (checkParent == null)
                    {
                        await _unitOfWork.GetRepositoryAsync<RoleFeaturePermission>().Insert(new RoleFeaturePermission() { ApplicationId = parent.ApplicationId.Value, FeatureId = parent.ParentId.Value, RoleId = model.RoleId, IsActive = roleP.IsEnabled, CreatedBy = GetUserId() });
                    }
                    await _unitOfWork.GetRepositoryAsync<RoleFeaturePermission>().Insert(new RoleFeaturePermission() { ApplicationId = roleP.ApplicationId, FeatureId = roleP.Id, RoleId = model.RoleId, IsActive = roleP.IsEnabled, CreatedBy = GetUserId() });
                }


            }



        }
    }
}
