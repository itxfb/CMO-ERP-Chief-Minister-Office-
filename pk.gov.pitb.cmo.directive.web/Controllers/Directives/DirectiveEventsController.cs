using AutoMapper;
using System.Reflection.Metadata;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.HRMIS;

namespace pk.gov.pitb.cmo.directive.web.Controllers.Directives
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectiveEventsController : ControllerBase
    {
        readonly EventsHandler _handler;
        public DirectiveEventsController(IUnitOfWork unitOfWork, IMapper mapper,EventsHandler handler, ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
        {
            _handler = handler;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _handler.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(IdObject id)
        {
            return Ok(await _handler.GetAsync(id.Id));
        }

        [HttpPost("getAllEvents")]
        public async Task<IActionResult> GetDirectiveEventsByDirectiveId(string id, int? skip = null, int? take = null, object? Params = null)
        {
            return Ok(await _handler.GetDirectiveEventsByDirectiveId(id, skip, take, Params));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(DirectiveEventVm model)
        {

            return Ok(await _handler.AddAsync(model));
        }


        [HttpPut]
        public async Task<IActionResult> PutAsync(DirectiveEventVm model)
        {
            await _handler.Update(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(DirectiveEventVm model)
        {
            await _handler.Delete(model);
            return Ok();
        }

    }
}
