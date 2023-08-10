using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Entities;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;
using pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.HRMIS;
using System.Security.Claims;
using System.Text.Json;

namespace pk.gov.pitb.cmo.directive.web.Controllers.HRMIS
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        readonly EmployeeHandler _handler;
        private readonly IWebHostEnvironment _webhosting;
        public EmployeeController(ILogger<EmployeeController> logger, IUnitOfWork unitOfWork, EmployeeHandler handler, IMapper mapper, ClaimsPrincipal claims, IWebHostEnvironment hostingEnvironment) : base(unitOfWork, mapper, claims)
        {
            _webhosting = hostingEnvironment;
            _handler = handler;
        }


        #region Dropdowns

        [HttpGet("getGender")]
        public async Task<IActionResult> GetGenders()
        {
            var data = await _handler.GetAllGenderAsync();

            return Ok(data);
        }

        [HttpGet("getMartialStatus")]
        public async Task<IActionResult> GetMaritalStatus()
        {
            var data = await _handler.GetAllMartialStatusAsync();
            return Ok(data);
        }


        [HttpGet("getAccessColour")]
        public async Task<IActionResult> getAccessColour()
        {
            var data = await _handler.getAccessColourAsync();
            return Ok(data);
        }

        [HttpGet("getCardType")]
        public async Task<IActionResult> getCardType()
        {
            var data = await _handler.getCardTypeAsync();
            return Ok(data);
        }

        [HttpGet("getEmployeeColour")]
        public async Task<IActionResult> getEmployeeColour()
        {
            var data = await _handler.getEmployeeColourAsync();
            return Ok(data);
        }

        [HttpGet("getEmployeeStatus")]
        public async Task<IActionResult> getEmployeeStatus()
        {
            var data = await _handler.getEmployeeStatusAsync();
            return Ok(data);
        }

        [HttpGet("getEmployeeSubStatus")]
        public async Task<IActionResult> getEmployeeSubStatus()
        {
            var data = await _handler.getEmployeeSubStatusAsync();
            return Ok(data);
        }

        [HttpGet("getPostNature")]
        public async Task<IActionResult> getPostNature()
        {
            var data = await _handler.getPostNatureAsync();
            return Ok(data);
        }

        [HttpGet("getPostPresently")]
        public async Task<IActionResult> getPostPresently()
        {
            var data = await _handler.getPostPresentlyAsync();
            return Ok(data);
        }

        [HttpGet("getServiceGroup")]
        public async Task<IActionResult> getServiceGroup()
        {
            var data = await _handler.getServiceGroupAsync();
            return Ok(data);
        }

        [HttpGet("getShifts")]
        public async Task<IActionResult> getShifts()
        {
            var data = await _handler.getShiftsAsync();
            return Ok(data);
        }

        [HttpGet("getJobType")]
        public async Task<IActionResult> getJobType()
        {
            var data = await _handler.getJobTypeAsync();
            return Ok(data);
        }

        [HttpGet("getDesignation")]
        public async Task<IActionResult> getDesignation()
        {
            var data = await _handler.getDesignationAsync();
            return Ok(data);
        }


        [HttpGet("getOffices")]
        public async Task<IActionResult> getOffices()
        {
            var data = await _handler.getOfficesAsync();
            return Ok(data);
        }

        [HttpGet("getDepartments")]
        public async Task<IActionResult> getDepartments()
        {
            var data = await _handler.getDepartmentsAsync();
            return Ok(data);
        }

        [HttpGet("getScales")]
        public async Task<IActionResult> getScales()
        {
            var data = await _handler.getScalesAsync();
            return Ok(data);
        }


        [HttpGet("getQualification")]
        public async Task<IActionResult> getQualification()
        {
            var data = await _handler.getQualificationAsync();
            return Ok(data);
        }

        [HttpGet("getDomiciles")]
        public async Task<IActionResult> getDomiciles()
        {
            var data = await _handler.getDomicilesAsync();
            return Ok(data);
        }


        [HttpGet("getReligions")]
        public async Task<IActionResult> getReligions()
        {
            var data = await _handler.getReligionsAsync();
            return Ok(data);
        }


        [HttpGet("getBloodGroups")]
        public async Task<IActionResult> getBloodGroups()
        {
            var data = await _handler.getBloodGroupsAsync();
            return Ok(data);
        }


        [HttpGet("getAppointmentMode")]
        public async Task<IActionResult> getAppointmentMode()
        {
            var data = await _handler.getAppointmentModeAsync();
            return Ok(data);
        }

        [HttpGet("getAccessColorsList")]
        public async Task<IActionResult> getAccessColorsList()
        {
            var data = await _handler.getAccessColorsAsync();
            return Ok(data);
        }

        [HttpGet("getCountriesList")]
        public async Task<IActionResult> getCountriesList()
        {
            var data = await _handler.getCountriesAsync();
            return Ok(data);
        }

        [HttpGet("getPoliceStation")]
        public async Task<IActionResult> getgetPoliceStationList()
        {
            var data = await _handler.getgetPoliceStationAsync();
            return Ok(data);
        }
        #endregion

        #region FormMethods

        [HttpPost("submitFormAddEmployee")]
        [AllowAnonymous]
        public async Task<IActionResult> submitformAddEmployee(IFormCollection iCollection)
        {
            IdObject response = null;
            try
            {
                #region Getting Data
                IFormCollection iCollectionImage = HttpContext.Request.Form;
                string data = iCollection["EmployeeObj"].ToString();
                string EmployeeAttachment = iCollection["EmployeeAttachment[]"].ToString();

                Employee deserializejsondata = JsonSerializer.Deserialize<Employee>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                int isEmployeeAlreadyExist = await _unitOfWork.GetRepositoryAsync<Employee>().GetAllQueryable().CountAsync(x => x.Cnic == deserializejsondata.Cnic);
                if (isEmployeeAlreadyExist > 0)
                {
                    return Ok(2);
                }
                deserializejsondata.EmployeeAttachments = iCollectionImage;
                if (deserializejsondata.Id != 0 && deserializejsondata.Id != -1)
                {
                    await _handler.UpdateAsync(deserializejsondata);

                    return Ok(1);

                }
                else
                {
                    response = await _handler.AddAsync(deserializejsondata);
                }
                if (response.Id > 0)
                {
                    return Ok(response.Id);

                }
                else
                {
                    return BadRequest();
                }
            }

            #endregion

            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("getAllEmployees")]

        public async Task<IActionResult> getAllEmployees([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var allEmployees = await _handler.getAllEmployeesAsync(pageNumber, pageSize);
            return Ok(allEmployees);
        }


        [HttpPost("searchEmployee")]

        public async Task<IActionResult> searchEmployee(IFormCollection collection, int pageNumber, int pageSize)
        {
            string searchedEmployeeModel = collection["searchedObj"].ToString();
            var deserializedModel = JsonSerializer.Deserialize<EmployeeSearch>(searchedEmployeeModel);

            var searchList = await _handler.SearchEmployee(deserializedModel, pageNumber, pageSize);
            return Ok(searchList);
        }


        #endregion
        [HttpPost("getEmployee")]
        public async Task<IActionResult> getEmployee(IdObject id)
        {
            //var deserializedModel = JsonSerializer.Deserialize<ID>(id);
            var getEmployee = await _handler.GetAsync(id);
            var serverPath = _webhosting.ContentRootPath;
            getEmployee.ImagePath = Path.Combine(serverPath, getEmployee.ImagePath);
            if (System.IO.File.Exists(getEmployee.ImagePath))
            {
                //getEmployee.Imagebase64 = "https://localhost:7221/daud/" + getEmployee.ImagePath;
                getEmployee.Imagebase64 = "data:image/gif;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(getEmployee.ImagePath));
            }

            return Ok(getEmployee);

        }
    }
}
