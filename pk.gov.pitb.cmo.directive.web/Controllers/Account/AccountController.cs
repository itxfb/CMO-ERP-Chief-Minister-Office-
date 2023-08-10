using System.IO;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Models.Common;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Account;

namespace pk.gov.pitb.cmo.directive.web.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(IUnitOfWork unitOfWork, IMapper mapper,ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
        {
            
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVm request)
        {
            AppUserModel user = await _authHandler.Login(request);
            var x = AuthHandler.GenerateToken(user);
                       
            return Ok(x);
        }
    }
}
