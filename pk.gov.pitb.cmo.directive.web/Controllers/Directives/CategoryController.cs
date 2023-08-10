using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;
using pk.gov.pitb.cmo.directive.web.Controllers.Account;

namespace pk.gov.pitb.cmo.directive.web.Controllers.Directives
{
    [Route("api/[controller]")]
    [ApiController]



    public class CategoryController : ControllerBase
    {
        readonly CategoryHandler _handler;
        public CategoryController(IUnitOfWork unitOfWork, CategoryHandler handler, IMapper mapper, ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
        {
            _handler = handler;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            return Ok(await _handler.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(IdObject id)
        {
            return Ok(await _handler.GetAsync(id.Id));
        }


        [HttpPost]
        public IActionResult Post(CategoryAddVm model)
        {

            var sss = Ok(_handler.AddAsync(model));
            return sss;
        }


        [HttpPut]
        public async Task<IActionResult> PutAsync(CategoryAddVm model)
        {
            await _handler.Update(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(CategoryAddVm model)
        {
            await _handler.Delete(model);
            return Ok();
        }


    }
}
