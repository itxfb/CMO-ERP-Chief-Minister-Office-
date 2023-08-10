using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using System.Security.Claims;
using pk.gov.pitb.cmo.directive.domain.Data.Abstractions;

namespace pk.gov.pitb.cmo.directive.web.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        readonly FeatureHandler _handler;
        public FeatureController(IUnitOfWork unitOfWork, FeatureHandler handler, IMapper mapper, ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
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
        public async Task<IActionResult> PostAsync(FeatureAddVm model)
        {
            return Ok(await _handler.AddAsync(model));
        }


        [HttpPut]
        public async Task<IActionResult> PutAsync(FeatureAddVm model)
        {
            await _handler.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(FeatureAddVm model)
        {
            await _handler.DeleteAsync(model);
            return Ok();
        }
    }
}
