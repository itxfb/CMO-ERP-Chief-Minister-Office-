using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.web.Controllers;
using pk.gov.pitb.cmo.directive.web.Controllers.Account;
using pk.gov.pitb.cmo.directive.web.Core.Filters;

namespace pk.gov.pitb.cmo.directive.web
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ResponseFilter]
    [ExceptionFilter]

    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        internal readonly IUnitOfWork _unitOfWork;
        internal readonly AuthHandler _authHandler;
        
        public ControllerBase( IUnitOfWork unitOfWork, IMapper _mapper,ClaimsPrincipal claimsPrincipal)
        {

            _unitOfWork = unitOfWork;
            _authHandler = new AuthHandler(unitOfWork, _mapper,claimsPrincipal);


        }

    }
}
