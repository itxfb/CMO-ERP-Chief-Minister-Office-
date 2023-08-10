using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;

namespace pk.gov.pitb.cmo.directive.web.Controllers.Directives
{
    [Route("api/[controller]")]
    [ApiController]


    public class PriorityController : ControllerBase
    {
        readonly PriorityHandler _handler;
        public PriorityController(IUnitOfWork unitOfWork, PriorityHandler handler, IMapper mapper, ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
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
        public async Task<IActionResult> PostAsync(PriorityAddVm model)
        {

            return Ok(await _handler.AddAsync(model));
        }


        [HttpPut]
        public async Task<IActionResult> PutAsync(PriorityAddVm model)
        {
            await _handler.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(PriorityAddVm model)
        {
            await _handler.Delete(model);
            return Ok();
        }


    }
}
