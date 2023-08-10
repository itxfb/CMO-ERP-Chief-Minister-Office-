using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pk.gov.pitb.cmo.contracts;
using pk.gov.pitb.cmo.directive.business.Handlers;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;
using pk.gov.pitb.cmo.directive.web.Core.Filters;
using pk.gov.pitb.cmo.directive.web.Core.Middleware;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace pk.gov.pitb.cmo.directive.web.Controllers.Directives
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectiveController : ControllerBase
    {

        readonly LetterHandler _handler;
        public DirectiveController(IUnitOfWork unitOfWork, LetterHandler handler, IMapper mapper, ClaimsPrincipal claims) : base(unitOfWork, mapper, claims)
        {
            _handler = handler;
        }

        [HttpPost("getAllLetters")]
        public async Task<IActionResult> GetAllAsync(int? skip = null, int? take= null, object? Params= null)
        {
            return Ok(await _handler.GetAll(skip,take,Params));
        }

        [HttpGet("getDirectiveCounts")]
        public async Task<IActionResult> GetDirectiveCountsAsync()
        {
            return Ok(await _handler.GetDirectivesCounts());
        }

        [HttpPost("getDirectivesById")]
        public async Task<IActionResult> GetDirectiveByLetterIdAsync(string Id,int? skip = null, int? take = null, object? Params = null)
        {         
            return Ok(await _handler.GetDirectiveByLetterIdAsync(Id, skip,take,Params));
        }

        [HttpPost("getResponsesbyId")]
        public async Task<IActionResult> GetResponseByDirectiveIdAsync(string id,int? skip, int? take, object? Params)
        {
            
            return Ok(await _handler.GetResponseByDirectiveIdAsync(id, skip,take,Params));
        }

        [HttpPost("getLetterAttachmentsById")]
        public async Task<IActionResult> GetLetterAttachmentsByIdAsync(IdObject id)
        {
            return Ok(await _handler.GetLetterAttachmentsByIdAsync(id));
        }

        [HttpPost("saveDirectiveResponse")]
        public async Task<IActionResult> SaveDirectiveResponse(IFormCollection iCollection)
        {

            string data = iCollection["response"].ToString();
            string ResponseFileType = iCollection["responseFileType"].ToString();
            var model = JsonSerializer.Deserialize<ResponseVM>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            model.DocumentFiles = HttpContext.Request.Form.Files;
            model.ResponseFileType = ResponseFileType;

            return Ok(await _handler.AddResponseAsync(model));  
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(IFormCollection collection)
        {

            string data = collection["directiveObj"].ToString();


            var model = JsonSerializer.Deserialize<LetterAddVm>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            model.LetterAttachments = collection.Files;

            return Ok(await _handler.AddAsync(model));


        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(DirectiveInstruction vm)
        {

            return Ok(await _handler.UpdateAsync(vm));

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(LetterAddVm vm)
        {

            return Ok(await _handler.DeleteAsync(vm));

        }
    }


}