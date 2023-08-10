using AutoMapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using System.Reflection.Metadata;

namespace pk.gov.pitb.cmo.directive.web.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        readonly ProfileHandler _handler;
        public ProfileController(IUnitOfWork unitOfWork,ProfileHandler handler, IMapper mapper, ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
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
        public async Task<IActionResult> PostAsync(AddUserProfileVm model)
        {
            return Ok(await _handler.AddAsync(model));
        }


        [HttpPut]
        public async Task<IActionResult> PutAsync(AddUserProfileVm model)
        {
            await _handler.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(AddUserProfileVm model)
        {
            await _handler.DeleteAsync(model);
            return Ok();
        }
    }
}
