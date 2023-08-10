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
    public class ConstituencyController : ControllerBase
    {
        readonly ConstituencyHandler _handler;
        public ConstituencyController(IUnitOfWork unitOfWork, ConstituencyHandler handler, IMapper mapper, ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
        {
            _handler = handler;
        }


        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(_handler.GetAll());
        }

        [HttpGet("constituencyTypes")]
        public async Task<IActionResult> GetAllConstituencyTypesAsync(int? skip = -1, int? limit = -1)
        {

            return Ok(await _handler.GetAllConstituencyTypes(skip, limit));
        }


        [HttpGet("getAllConstituences")]
        public async Task<IActionResult> GetAllConstituencesAsync(int? skip = -1, int take = -1, string searchText="")
        {

            return Ok(await _handler.GetAllConstituencies(skip, take, searchText));
        }



        [HttpPost]
        public async Task<IActionResult> PostAsync(ConstituencyVm model)
        {

            return Ok(await _handler.AddAsync(model));
        }


        [HttpPut]
        public async Task<IActionResult> PutAsync(ConstituencyVm model)
        {
            await _handler.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(IdObject model)
        {
            await _handler.DeleteAsync(model);
            return Ok();
        }
    }
}
