using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using System.Security.Claims;

namespace pk.gov.pitb.cmo.directive.web.Controllers.Directives
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        readonly DepartmentHandler _handler;
        public DepartmentController(IUnitOfWork unitOfWork, DepartmentHandler handler, IMapper mapper, ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
        {
            _handler = handler;
        }


        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(_handler.GetAll());
        }

        [HttpGet("departmentTypes")]
        public async Task<IActionResult> GetDepartmentTypeAsync(int? skip = -1, int? take = -1,string searchText="")
        {

            return Ok(await _handler.GetAllDepartmentTypesAsync(skip, take, searchText));
        }



        [HttpPost]
        public async Task<IActionResult> PostAsync(DepartmentVm model)
        {

            return Ok(await _handler.AddAsync(model));
        }


        [HttpPut]
        public async Task<IActionResult> PutAsync(DepartmentVm model)
        {
            await _handler.Update(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(DepartmentVm model)
        {
            await _handler.Delete(model);
            return Ok();
        }
    }
}
