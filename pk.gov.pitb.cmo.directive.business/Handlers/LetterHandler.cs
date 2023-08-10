using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using System.Net.Http;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using System.Security.Cryptography;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Metrics;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;
using pk.gov.pitb.cmo.directive.domain.Exceptions;
using Microsoft.AspNetCore.Http;
using pk.gov.pitb.cmo.directive.domain.Enums;
using System.Linq.Expressions;
using Newtonsoft.Json;
using pk.gov.pitb.cmo.contracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class LetterHandler : BaseHandler
    {
        private readonly IFileManager fileManager;
        public LetterHandler(ClaimsPrincipal claimsPrincipal, IUnitOfWork unitOfWork, IFileManager _fileManager, IMapper mapper) : base(unitOfWork, claimsPrincipal, mapper)
        {
            this.fileManager = _fileManager;

        }

        public async Task<IdObject> AddAsync(LetterAddVm vm)
        {

            Letter c = new Letter()
            {
                ComputerNumberImprintDate = vm.ComputerNumberImprintDate,
                ComputerNumber = vm.ComputerNumber,
                LetterDate = vm.LetterDate,
                OfficeId = vm.OfficeId,
                Dictation = vm.Dictation,
                ConstituenceyId = vm.ConstituenceyId,
                PriorityId = vm.PriorityId,
                DiaryNumber = vm.DiaryNumber,
                ActionByIC = vm.ActionByIC,
                IsSentToMinister = vm.IsSentToMinister,
                LetterType = vm.LetterType,
                LetterNumber = vm.LetterNumber,
                LetterStatus = vm.LetterStatus,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                CreatedBy = base.GetUserId(),


            };

            if (vm.DirectiveInstructions?.Count > 0)
            {


                c.Directives = new List<Directive>();
                Directive d = null;

                foreach (var item in vm.DirectiveInstructions)
                {
                    d = new Directive()
                    {
                        LetterId = item.LetterId,
                        DepartmentId = item.DepartmentId,
                        SubDepartmentName = item.SubDepartmentName,
                        SubjectCodeId = item.SubjectCodeId,
                        SubjectText = item.SubjectText,
                        TimeLine = item.TimeLine,
                        ApplicantName = item.ApplicantName,
                        EstimatedCost = item.EstimatedCost,
                        AllocatedCost = item.AllocatedCost,
                        NatureOfSchemeId = item.NatureOfSchemeId,
                        SchemeMinutesOfMeeting = item.SchemeMinutesOfMeeting,
                        AdpNumber = item.AdpNumber,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                        DirectiveTypeId = Convert.ToInt32(item.DirectiveTypeId),
                        CreatedBy = base.GetUserId(),


                    };


                    c.Directives.Add(d);
                }
            }

            var letter = await _unitOfWork.GetRepositoryAsync<Letter>().Insert(c);

            var files = vm.LetterAttachments;
            var attachments = new List<Attachment>();
            Attachment att = null;

            for (var i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var Attachmentpath = fileManager.Save(files[i], files[i].FileName, "LettersPath", "eDirectives");
                var attachmentType = await _unitOfWork.GetRepositoryAsync<AttachmentType>().GetOne(a => a.Name == EntitiesEnum.Letter);
                if (attachmentType == null)
                {

                    attachmentType = _unitOfWork.GetRepositoryAsync<AttachmentType>().Insert(new AttachmentType { Name = EntitiesEnum.Letter, Abbreviation = "L" }).Result;
                }


                att = new Attachment()
                {

                    FileExtension = Path.GetExtension(files[i].FileName),
                    ContentType = file.ContentType,
                    DisplayName = file.FileName,
                    Path = Attachmentpath,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = base.GetUserId(),
                    AttachmentId = letter.Id,
                    AttachmentTypeId = attachmentType.Id

                };

                await _unitOfWork.GetRepositoryAsync<Attachment>().Insert(att);

            }

            return new IdObject(letter.Id);
        }

        public async Task<IdObject> AddResponseAsync(ResponseVM model)
        {   


            var directive = await _unitOfWork.GetRepositoryAsync<Directive>().GetOne(a => a.Id == model.DirectiveId);

            if (directive == null)
            {
                throw new GeneralException("Directive doesn't exist!");
            }

            var response = _mapper.Map<Response>(model);

            //validate response and get only the active response
            var record = await _unitOfWork.GetRepositoryAsync<Response>().GetOne(a => a.DirectiveId == model.DirectiveId && a.IsActive);
            if (record == null)
            {
                response.CreatedBy = base.GetUserId();
                response = await _unitOfWork.GetRepositoryAsync<Response>().Insert(response);
            }
            else
            {
                //record.ResponseText = response.ResponseText;
                //record.DocumentDescription = response.DocumentDescription;
                
                record.ResponseRemarks = response.ResponseRemarks;
                await _unitOfWork.GetRepositoryAsync<Response>().Update(record.Id, record);
            }

            //Save Files
            foreach (var file in response.DocumentFiles)
            {
                var attachmentPath = fileManager.Save(file, file.FileName, "ResponsesPath", "eDirectives");
                var attachmentType = await _unitOfWork.GetRepositoryAsync<AttachmentType>().GetOne(a => a.Name == model.ResponseFileType);
                if (attachmentType == null)
                {
                    attachmentType = _unitOfWork.GetRepositoryAsync<AttachmentType>().Insert(new AttachmentType { Name = model.ResponseFileType, Abbreviation = "R" }).Result;
                }

                await _unitOfWork.GetRepositoryAsync<Attachment>().Insert(new Attachment()
                {

                    FileExtension = Path.GetExtension(file.FileName),
                    ContentType = file.ContentType,
                    DisplayName = file.FileName,
                    Path = attachmentPath,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = base.GetUserId(),
                    AttachmentId = record!=null?record.Id:response.Id,
                    AttachmentTypeId = attachmentType.Id

                });
            }

            directive.StatusId = 1;
            directive.UpdatedBy = base.GetUserId();
            await _unitOfWork.GetRepositoryAsync<Directive>().Update(directive.Id, directive);

            return new IdObject(response.Id);
        }

        public async Task<List<LetterVM>> GetAll(int? skip, int? take, object? Params)
        {
            var totalCount = await _unitOfWork.GetRepositoryAsync<Letter>().Count(a => !a.IsDeleted);

            var userDeparments = await GetUserDepartmentIds();


            var letterTypeAttachment = await _unitOfWork.GetRepositoryAsync<AttachmentType>().GetOne(a => a.Name == EntitiesEnum.Letter);

            var query = _unitOfWork.GetRepositoryAsync<Letter>().GetAllQueryable()
                .Include(a => a.Directives)
                .Where(a => !a.IsDeleted && a.Directives.Any(a => userDeparments.Contains(a.DepartmentId.ToString())));


            int filteredCount = 0;
           
            var dictionary = new Dictionary<string, object>();

            var predicates = new Dictionary<string, string>();

            dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(Params?.ToString());

            if (dictionary.Count != 0)
            {
                
                predicates = JsonConvert.DeserializeObject<Dictionary<string, string>>(dictionary["Params"]?.ToString());
                // Create a parameter expression for the entity type
                var parameter = Expression.Parameter(typeof(Letter), "a");
                Expression condition = null;

                foreach (var filter in predicates)
                {

                    if (!string.IsNullOrEmpty(filter.Value.ToString()))
                    {
                        // Create property access expression for the current property name
                        var property = Expression.Property(parameter, filter.Key);

                        // Create constant expression for the current property value
                        var value = Expression.Constant(filter.Value.ToString());

                        if (filter.Key.Contains("Id"))
                        {
                            value = Expression.Constant(Utility.Decrypt(filter.Value).ToString());
                        }

                        var contains = default(MethodCallExpression);

                        if (property.Type == typeof(DateTime) || property.Type == typeof(DateTime?))
                        {

                            value = Expression.Constant(DateTime.Parse(filter.Value).ToString("yyyy-MM-dd"));

                            var propertyasString = Expression.Call(property, typeof(object).GetMethod("ToString"));

                            var valueasString = Expression.Call(value, typeof(object).GetMethod("ToString"));

                            contains = Expression.Call(propertyasString, "Contains", Type.EmptyTypes, valueasString);
                        }

                        else if(property.Type == typeof(int?) || property.Type == typeof(int))
                        {
                            var propertyasString = Expression.Call(property, typeof(object).GetMethod("ToString"));

                            var valueasString = Expression.Call(value, typeof(object).GetMethod("ToString"));

                            contains = Expression.Call(propertyasString, "Contains", Type.EmptyTypes, valueasString);
                        }
                       
                        else
                        {
                            contains = Expression.Call(property, "Contains", Type.EmptyTypes, value);
                        }


                        // Create AND condition with previous conditions (if any)
                        condition = condition == null ? contains : Expression.AndAlso(condition, contains);
                    }
                }



                if (condition != null)
                {
                    // Create lambda expression for the predicate
                    var lambda = Expression.Lambda<Func<Letter, bool>>(condition, parameter);


                    // Apply the predicate to the query
                    query = query.Where(lambda);

                   
                    filteredCount = query.Count();
                }
            }


            if (query.FirstOrDefault() != null)
            {
                query.First().DirectiveCounts = new DirectiveCountVM { PendingDepartmentCount = query.Count(a => a.Directives.Any(d => d.StatusId == 0)), PendingCMOCount = query.Count(a => a.Directives.Any(d => d.StatusId == 1)), ImplementedCount = query.Count(a => a.Directives.Any(d => d.StatusId == 2)), PendingDisposedOffCount = query.Count(a => a.Directives.Any(d => d.StatusId == 3)) };
            }

            query = query.Skip(skip ?? 0).Take(take ?? Convert.ToInt32(totalCount))
                .Include(a => a.Office)
                .Include(a => a.Priority)
                .Include(a => a.Constituency);
                

            if (query.FirstOrDefault() != null)
            {
                if (filteredCount != 0)
                    query.First().TotalCount = filteredCount;
                else
                    query.First().TotalCount = totalCount;

               

            }

            var res = await query
                .Select(a => new LetterVM
                {
                    Id = a.Id,
                    ComputerNumberImprintDate = a.ComputerNumberImprintDate.ToString("dd-MM-yyyy"),
                    ComputerNumber = a.ComputerNumber,
                    Constituency = a.Constituency.Name,
                    ActionByIC = a.ActionByIC == true ? "Yes" : "No",
                    IsSentToM = a.IsSentToMinister == true ? "Yes" : "No",
                    LetterDate = a.LetterDate.ToString("dd-MM-yyyy"),
                    LetterNumber = a.LetterNumber,
                    DiaryNumber = a.DiaryNumber,
                    Office = a.Office.Name,
                    Prority = a.Priority.Name,
                    Status = a.LetterStatus,
                    Type = a.LetterType == "1" ? "Multiple" : "Single",
                    DirectiveCounts = a.DirectiveCounts,
                    TotalCount = a.TotalCount
                   
                })
               .ToListAsync();

            res = res.Select( a =>
            {
                a.Documents = _unitOfWork.GetRepositoryAsync<Attachment>().Get(b => b.AttachmentId == a.Id && b.AttachmentTypeId == letterTypeAttachment.Id).Result;
                return a;
            }).ToList();

            return res;
        }


        public async Task<DirectiveCountVM> GetDirectivesCounts()
        {
            var records = await _unitOfWork.GetRepositoryAsync<Directive>().Get(a=>!a.IsDeleted);
           
            return new DirectiveCountVM {  PendingDepartmentCount= records.Where(a => a.StatusId == 0).Count(), PendingCMOCount= records.Where(a => a.StatusId == 1).Count(), ImplementedCount = records.Where(a => a.StatusId == 2).Count(), PendingDisposedOffCount = records.Where(a => a.StatusId == 3).Count() };
            
        }

        public async Task<List<DirectiveInstructionDTO>> GetDirectiveByLetterIdAsync(string Id, int? skip, int? take, object? Params)
        {

            var userDepartments = await GetUserDepartmentIds();

            var query = _unitOfWork.GetRepositoryAsync<Directive>().GetAllQueryable().Where(a => a.LetterId == Utility.Decrypt(Id) && userDepartments.Contains(a.DepartmentId.ToString()));

            var totalCount = query.Count();

            int filteredCount = 0;

            var dictionary = new Dictionary<string, object>();

            var predicates = new Dictionary<string, string>();

            dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(Params?.ToString());

            if (dictionary.Count != 0)
            {

                predicates = JsonConvert.DeserializeObject<Dictionary<string, string>>(dictionary["Params"]?.ToString());
                // Create a parameter expression for the entity type
                var parameter = Expression.Parameter(typeof(Directive), "a");
                Expression condition = null;

                foreach (var filter in predicates)
                {

                    if (!string.IsNullOrEmpty(filter.Value.ToString()))
                    {
                        // Create property access expression for the current property name
                        var property = Expression.Property(parameter, filter.Key);

                        // Create constant expression for the current property value
                        var value = Expression.Constant(filter.Value.ToString());

                        if (filter.Key.Contains("Id"))
                        {
                            value = Expression.Constant(Utility.Decrypt(filter.Value).ToString());
                        }

                        var contains = default(MethodCallExpression);

                        if (property.Type == typeof(DateTime) || property.Type == typeof(DateTime?))
                        {

                            value = Expression.Constant(DateTime.Parse(filter.Value).ToString("yyyy-MM-dd"));

                            var propertyasString = Expression.Call(property, typeof(object).GetMethod("ToString"));

                            var valueasString = Expression.Call(value, typeof(object).GetMethod("ToString"));

                            contains = Expression.Call(propertyasString, "Contains", Type.EmptyTypes, valueasString);
                        }

                        else if (property.Type == typeof(int?) || property.Type == typeof(int))
                        {
                            var propertyasString = Expression.Call(property, typeof(object).GetMethod("ToString"));

                            var valueasString = Expression.Call(value, typeof(object).GetMethod("ToString"));

                            contains = Expression.Call(propertyasString, "Contains", Type.EmptyTypes, valueasString);
                        }

                        else
                        {
                            contains = Expression.Call(property, "Contains", Type.EmptyTypes, value);
                        }


                        // Create AND condition with previous conditions (if any)
                        condition = condition == null ? contains : Expression.AndAlso(condition, contains);
                    }
                }



                if (condition != null)
                {
                    // Create lambda expression for the predicate
                    var lambda = Expression.Lambda<Func<Directive, bool>>(condition, parameter);


                    // Apply the predicate to the query
                    query = query.Where(lambda);

                    filteredCount = query.Count();
                }
            }
            query = query.Skip(skip ?? 0).Take(Convert.ToInt32(totalCount))
                .Include(a=>a.Department)
                .Include(a => a.NatureOfScheme)
                .Include(a => a.Subject);


            if (query.FirstOrDefault() != null)
            {
                if (filteredCount != 0)
                {
                    query.First().TotalCount = filteredCount;
                  
                }
                else
                {
                    query.First().TotalCount = totalCount;
                }
               
            }



            return await query.Select(a => new DirectiveInstructionDTO
            {

                ApplicantName = a.ApplicantName,
                AdpNumber = a.AdpNumber,
                AllocatedCost = a.AllocatedCost,
                DepartmentName = a.Department.Name,
                NatureOfSchemeName = a.NatureOfScheme.Name,
                SubjectCodeName = a.Subject.Code,
                LetterId = a.LetterId,
                DirectiveTypeId = a.DirectiveTypeId.Value,
                TimeLine = a.TimeLine,
                TimelineDays = Math.Ceiling(a.TimeLine.Subtract(DateTime.Now).TotalDays).ToString(),
                DepartmentId = a.DepartmentId,
                SubDepartmentName = a.SubDepartmentName,
                EstimatedCost = a.EstimatedCost,
                StatusId = a.StatusId,
                Status = a.StatusId == 1 ? "Pending With CMO" : a.StatusId == 0 ? "Pending With Department" : a.StatusId == 2 ? "Implemented" : a.StatusId == 3 ? "Re-Sent For Follow Up" : "Disposed",
                TypeName = a.DirectiveTypeId.Value == 1 ? "Multiple" : "Single",
                SubjectText = a.SubjectText,
                Id = a.Id,
                NatureOfSchemeId = a.NatureOfSchemeId,
                SchemeMinutesOfMeeting = a.SchemeMinutesOfMeeting,
                SubjectCodeId = a.SubjectCodeId,
                TotalCount = a.TotalCount,

            }).ToListAsync();
        }

        public async Task<List<ResponseVM>> GetResponseByDirectiveIdAsync(string id,int? skip,int? take, object? Params)
        {
            var response = await _unitOfWork.GetRepositoryAsync<Response>().Get(a => a.DirectiveId == Utility.Decrypt(id));

            var totalCount = await _unitOfWork.GetRepositoryAsync<Response>().GetCount(a => !a.IsDeleted && a.DirectiveId == Utility.Decrypt(id));

            int filteredCount = 0;

            var dictionary = new Dictionary<string, object>();

            var predicates = new Dictionary<string, string>();

            dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(Params?.ToString());

            if (dictionary.Count != 0)
            {

                predicates = JsonConvert.DeserializeObject<Dictionary<string, string>>(dictionary["Params"]?.ToString());
                // Create a parameter expression for the entity type
                var parameter = Expression.Parameter(typeof(Response), "a");
                Expression condition = null;

                foreach (var filter in predicates)
                {

                    if (!string.IsNullOrEmpty(filter.Value.ToString()))
                    {
                        // Create property access expression for the current property name
                        var property = Expression.Property(parameter, filter.Key);

                        // Create constant expression for the current property value
                        var value = Expression.Constant(filter.Value.ToString());

                        if (filter.Key.Contains("Id"))
                        {
                            value = Expression.Constant(Utility.Decrypt(filter.Value).ToString());
                        }

                        var contains = default(MethodCallExpression);

                        if (property.Type == typeof(DateTime) || property.Type == typeof(DateTime?))
                        {

                            value = Expression.Constant(DateTime.Parse(filter.Value).ToString("yyyy-MM-dd"));

                            var propertyasString = Expression.Call(property, typeof(object).GetMethod("ToString"));

                            var valueasString = Expression.Call(value, typeof(object).GetMethod("ToString"));

                            contains = Expression.Call(propertyasString, "Contains", Type.EmptyTypes, valueasString);
                        }

                        else if (property.Type == typeof(int?) || property.Type == typeof(int))
                        {
                            var propertyasString = Expression.Call(property, typeof(object).GetMethod("ToString"));

                            var valueasString = Expression.Call(value, typeof(object).GetMethod("ToString"));

                            contains = Expression.Call(propertyasString, "Contains", Type.EmptyTypes, valueasString);
                        }

                        else
                        {
                            contains = Expression.Call(property, "Contains", Type.EmptyTypes, value);
                        }


                        // Create AND condition with previous conditions (if any)
                        condition = condition == null ? contains : Expression.AndAlso(condition, contains);
                    }
                }



                if (condition != null)
                {
                    // Create lambda expression for the predicate
                    var lambda = Expression.Lambda<Func<Response, bool>>(condition, parameter);


                    // Apply the predicate to the query
                    response = response.AsQueryable().Where(lambda);

                    filteredCount = response.Count();
                }
            }


            response = response.Skip(skip ?? 0).Take(take ?? totalCount).OrderByDescending(a => a.CreatedAt);

            

            var responseAttachmentType = await _unitOfWork.GetRepositoryAsync<AttachmentType>().GetOne(a => a.Name == EntitiesEnum.Response);
            var CMOAttachmentType = await _unitOfWork.GetRepositoryAsync<AttachmentType>().GetOne(a => a.Name == EntitiesEnum.CMOResponse);

            if(CMOAttachmentType== null)
            {
                CMOAttachmentType = await _unitOfWork.GetRepositoryAsync<AttachmentType>().Insert(new AttachmentType() { Name = EntitiesEnum.CMOResponse, Abbreviation = "CMO" });
            }


            var vmResponse = response.Select(a => new ResponseVM
            {
                Id = a.Id,
                DirectiveId = a.DirectiveId,
                ResponseText = a.ResponseText,
                DocumentDescription = a.DocumentDescription ?? "",
                ResponseRemarks = a.ResponseRemarks ?? "",
                ResponseAttachments = _unitOfWork.GetRepositoryAsync<Attachment>().Get(x => x.AttachmentId == a.Id && x.AttachmentTypeId == responseAttachmentType.Id).Result.Select(a => new AttachmentVM { Id= a.Id,AttachmentId = a.Id,DisplayName= a.DisplayName, AttachmentTypeId = a.AttachmentTypeId, Path = a.Path }).ToList(),
                CMOAttachments = _unitOfWork.GetRepositoryAsync<Attachment>().Get(x => x.AttachmentId == a.Id && x.AttachmentTypeId == CMOAttachmentType.Id).Result.Select(a => new AttachmentVM { Id= a.Id,AttachmentId = a.Id,DisplayName= a.DisplayName, AttachmentTypeId = a.AttachmentTypeId, Path = a.Path }).ToList()
            }).ToList();

            if (vmResponse.FirstOrDefault() != null)
            {
                if (filteredCount != 0)
                    vmResponse.First().TotalCount = filteredCount;
                else
                    vmResponse.First().TotalCount = totalCount;
            }

            return vmResponse;

        }

        public async Task<List<AttachmentVM>> GetLetterAttachmentsByIdAsync(IdObject id)
        {

            var attachmentType = await _unitOfWork.GetRepositoryAsync<AttachmentType>().GetOne(a => a.Name == EntitiesEnum.Letter);

            var attachment = await _unitOfWork.GetRepositoryAsync<Attachment>().Get(a => a.AttachmentId == id.Id && a.AttachmentTypeId == attachmentType.Id);

            return attachment.Select(a => new AttachmentVM { Id = a.Id, AttachmentId = a.Id, DisplayName = a.DisplayName, AttachmentTypeId = a.AttachmentTypeId, Path = a.Path }).ToList();
        }
       
        public async Task<Letter> DeleteAsync(LetterAddVm vm)
        {

            var letter = await _unitOfWork.GetRepositoryAsync<Letter>().GetOne(a => a.Id == vm.Id);

            letter.IsDeleted = true;
            letter.DeletedAt = DateTime.Now;
            letter.DeletedBy = base.GetUserId();
            await _unitOfWork.GetRepositoryAsync<Letter>().Update(letter.Id, letter, false, false);


            return letter;

        }

        public async Task<DirectiveInstruction> UpdateAsync(DirectiveInstruction vm)
        {

            var directive = _mapper.Map<Directive>(vm);

            if(directive.StatusId == 3)
            {
                var getResponses = await _unitOfWork.GetRepositoryAsync<Response>().Get(a => a.DirectiveId == directive.Id && a.IsActive);
                
                getResponses= getResponses.Select(a =>
                {
                    a.IsActive = false;
                    return a;
                });

                foreach (var response in getResponses)
                {
                    await _unitOfWork.GetRepositoryAsync<Response>().Update(response.Id, response);
                }
            }

            directive.UpdatedAt = DateTime.Now;
            directive.UpdatedBy = base.GetUserId();
            if(directive.StatusId!=null)
                await _unitOfWork.GetRepositoryAsync<Directive>().Update(directive.Id, directive);

            return vm;

        }





    }
}
