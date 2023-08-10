using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using System.Security.Claims;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;

namespace pk.gov.pitb.cmo.directive.web.Controllers.Directives
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly SubjectHandler _handler;
        public SubjectController(IUnitOfWork unitOfWork, SubjectHandler handler, IMapper mapper, ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
        {
            _handler = handler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int? skip = -1, int? take = -1)
        {

            return Ok(await _handler.GetAllAsync(skip, take));
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync(SubjectVm model)
        {

            return Ok(await _handler.AddAsync(model));
        }


        [HttpPut]
        public async Task<IActionResult> PutAsync(SubjectVm model)
        {
            await _handler.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(SubjectVm model)
        {
            await _handler.DeleteAsync(model);
            return Ok();
        }
    }
}
