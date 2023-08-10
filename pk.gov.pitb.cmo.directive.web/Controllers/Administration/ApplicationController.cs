using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using System.Security.Claims;

namespace pk.gov.pitb.cmo.directive.web.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        readonly ApplicationHandler _handler;
        public ApplicationController(IUnitOfWork unitOfWork, ApplicationHandler handler, IMapper mapper, ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(_handler.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(IdObject id)
        {
            return Ok(await _handler.GetAsync(id.Id));
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync(ApplicationVm model)
        {

            var result = Ok(await _handler.AddAsync(model));
            return result;
        }


        [HttpPut]
        public async Task<IActionResult> PutAsync(ApplicationVm model)
        {
            await _handler.Update(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(ApplicationVm model)
        {
            await _handler.Delete(model);
            return Ok();
        }
    }
}
