using System;
using System.Linq.Expressions;
using System.Security.Claims;
using AutoMapper;
using HashidsNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace pk.gov.pitb.cmo.directive.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedController : ControllerBase
    {
        private BaseHandler _handler;
       
        public SharedController(IUnitOfWork unitOfWork, BaseHandler handler, IMapper mapper, ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> GetGenericData( string tableName = "",  int? skip = 0, int? take = 10, object? predicates=null, string? columns = "", string? dtoName="")
        {
            return Ok(await _handler.GetData(tableName, skip, take, predicates, columns, dtoName));
        }

        [HttpGet("getAllDivisionsWithDistrict")]
        public async Task<IActionResult> GetAllDivisionsAsync(int? skip = -1, int? take = -1,string searchText="")
        {
            return Ok(await _handler.GetAllDivisionsByUser(skip, take, searchText));

        }


        [HttpGet("getAllDistricts")]
        public async Task<IActionResult> GetAllDistrictsAsync(int? skip = -1, int? take = -1, string searchText = "")
        {
            return Ok(await _handler.GetAllDistricts( skip,  take,  searchText));

        }

        [HttpGet("getAllDepartmentTypes")]
        public async Task<IActionResult> GetAllDepartmentTypesAsync(int? skip = -1, int? take = -1, string searchText = "")
        {
            return Ok(await _handler.GetAllDepartmentTypesAsync(skip, take,searchText));

        }

        [HttpGet("getAllDepartments")]
        public async Task<IActionResult> GetAllDepartmentsAsync(int? skip = -1, int? take = -1, string searchText = "")
        {
            return Ok(await _handler.GetAllDepartments(skip, take,searchText));

        }

        [HttpGet("getAllConstituencyTypes")]
        public async Task<IActionResult> GetAllConstituencyTypesAsync(int? skip = -1, int? take = -1, string searchText = "")
        {
            return Ok(await _handler.GetAllConstituencyTypes(skip,take,searchText));

        }



        [HttpGet("getAllConstituencies")]
        public async Task<IActionResult> GetAllConstituenciesAsync(int? skip = -1, int? take = -1,string searchText="")
        {
            return Ok(await _handler.GetAllConstituencies(skip,take, searchText));
        }


        [HttpGet("getAllOffices")]
        public async Task<IActionResult> GetAllOfficesAsync(int? skip = -1, int? take = -1, string searchText="")
        {
            return Ok(await _handler.GetAllOffices(skip,take, searchText));
        }

        [HttpGet("getAllSubjects")]
        public async Task<IActionResult> GetAllSubjectsAsync(int? skip=-1, int? take=-1, string searchText="")
        {
            return Ok(await _handler.GetAllSubjectsAsync(skip, take, searchText));
        }

        [HttpGet("getAllPriorities")]
        public async Task<IActionResult> GetAllPrioritiesAsync(int? skip = -1, int? take = -1, string searchText="")
        {
            return Ok(await _handler.GetAllPrioritiesAsync(skip, take, searchText));
        }

        [HttpGet("getAllSignatories")]
        public IActionResult GetAllSignatories()
        {
            return Ok(_handler.GetAllSignatories());
        }


        [HttpGet("getAllParties")]
        public async Task<IActionResult> GetAllPartiesAsync(int? skip = -1, int? take = -1,string searchText="")
        {
            return Ok(await _handler.GetAllPartiesAsync(skip, take, searchText));
        }


        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsersListAsync(int? skip = -1, int? take = -1, string searchText = "")
        {
            return Ok(await _handler.GetAllUsersListAsync(skip, take, searchText));
        }

        [HttpGet("getAllDesignations")]
        public async Task<IActionResult> GetAllDesignationsAsync(int? skip = -1, int? take = -1, string searchText = "")
        {
            return Ok(await _handler.GetAllDesignationsAsync(skip, take, searchText));
        }


        [HttpGet("getAllDirectiveTypes")]
        public async Task<IActionResult> GetAllDirectiveTypesAsync(int? skip = -1, int? take = -1, string searchText = "")
        {
            return Ok(await _handler.GetAllDirectiveTypes(skip, take, searchText));
        }

        [HttpPost("getAllApplicantsByConstituency")]
        public async Task<IActionResult> GetApplicantsByConstituencyIdAsync(IdObject idObject)
        {
            return Ok(await _handler.GetApplicantsByConstituencyIdAsync(idObject.Id));
        }

        [HttpGet("getAllRoles")]
        public async Task<IActionResult> GetAllRolesAsync(int? skip = -1, int? take = -1, string searchText = "")
        {
            return Ok(await _handler.GetAllRolesAsync(skip, take, searchText));
        }

        [HttpGet("getAllNOS")]

        public async Task<IActionResult> GetAllNatureOfSchemesAsync(int? skip = -1, int? take = -1, string searchText = "")
        {
            return Ok(await _handler.GetAllNatureOfSchemesAsync(skip, take,searchText));
        }

        [HttpGet("getAllProfileTypes")]

        public async Task<IActionResult> GetAllProfileTypesAsync(int? skip = -1, int? take = -1, string searchText = "")
        {
            return Ok(await _handler.GetAllProfileTypesAsync(skip, take, searchText));
        }


        [HttpGet("getAllProfiles")]

        public async Task<IActionResult> GetAllProfilesAsync(int? skip = -1, int? take = -1, string searchText = "")
        {
            return Ok(await _handler.GetAllProfilesAsync(skip, take, searchText));
        }

        [HttpPost("EncryptId")]
        [AllowAnonymous]
        public string Encrypt(int id)
        {
            return Utility.Encrypt(id);
        }

        [HttpPost("deleteAttachment")]
        public async Task<IActionResult> DeleteAttachment(IdObject id)
        {
            var delete = await _unitOfWork.GetRepositoryAsync<Attachment>().Delete(id.Id);

            if (delete != 0)
            {
                return Ok(delete);
            }
            return BadRequest();
        }


        [HttpPost("downloadFile")]
        public async Task<IActionResult> FileDownload(IdObject id)
        {

            var file = await _unitOfWork.GetRepositoryAsync<Attachment>().GetOne(a => a.Id == id.Id && !a.IsDeleted);

            if (System.IO.File.Exists(file.Path))
            {
                var fileBytes = System.IO.File.ReadAllBytes(file.Path);
                var fileName = Path.GetFileName(file.Path);
                return File(fileBytes, "application/pdf", fileName);
            }
            return BadRequest();
        }

    }
}
