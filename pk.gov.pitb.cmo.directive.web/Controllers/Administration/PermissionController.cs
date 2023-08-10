using System.Reflection.Metadata;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;

namespace pk.gov.pitb.cmo.directive.web.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private UserManageHandler _user;
        private RoleHandler _roleHandler;
        public PermissionController(IUnitOfWork unitOfWork, RoleHandler roleHandler,UserManageHandler userHandler, IMapper mapper,ClaimsPrincipal claims) : base( unitOfWork, mapper, claims)
        {
            this._roleHandler = roleHandler;
            this._user = userHandler;
        }


        [HttpPost("rolepermissions")]

        public IActionResult GetPermissionByRoleId(IdObject id)
        {

            return Ok(_user.GetAllFeaturesForUsermanagmentModule(id.Id));
        }

        [HttpPost("updaterolepermission")]

        public async Task<IActionResult> UpdateRolePermissionAsync(UpdateRolePermissionVm model)
        {
            await _user.UpdateRolePermission(model);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            return Ok(await _roleHandler.GetAllAsync());
        }



        [HttpPost("role")]
        public async Task<IActionResult> PostAsync(UserRoleAddEditVm model)
        {

            return Ok(await _roleHandler.AddAsync(model));
        }


    }
}
