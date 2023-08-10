using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;
using Newtonsoft.Json;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.HRMIS;
using System.Linq.Expressions;
using pk.gov.pitb.cmo.contracts;
using Microsoft.EntityFrameworkCore;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class EventsHandler: BaseHandler
    {
        public EventsHandler(IUnitOfWork unitOfWork, IMapper mapper, ClaimsPrincipal claimsPrincipal) : base(unitOfWork, claimsPrincipal, mapper)
        {

        }


        public async Task<List<DirectiveEventVm>> GetDirectiveEventsByDirectiveId(string Id, int? skip, int? take, object? Params)
        {
            

            var query = _unitOfWork.GetRepositoryAsync<DirectiveEvent>().GetAllQueryable().Where(a => a.DirectiveId == Utility.Decrypt(Id));

            var totalCount = query.Count();

            int filteredCount = 0;

            var dictionary = new Dictionary<string, object>();

            var predicates = new Dictionary<string, string>();

            dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(Params?.ToString());

            if (dictionary.Count != 0)
            {

                predicates = JsonConvert.DeserializeObject<Dictionary<string, string>>(dictionary["Params"]?.ToString());
                // Create a parameter expression for the entity type
                var parameter = Expression.Parameter(typeof(DirectiveEvent), "a");
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
                    var lambda = Expression.Lambda<Func<DirectiveEvent, bool>>(condition, parameter);


                    // Apply the predicate to the query
                    query = query.Where(lambda);

                    filteredCount = query.Count();
                }
            }
            query = query.Skip(skip ?? 0).Take(take??10)
                    .Include(a=>a.Sender)
                    .Include(a=>a.Reciever);


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



            return await query.Select(a => new DirectiveEventVm
            {
                DocumentNumber = a.DocumentNumber,
                Reciever= a.Reciever.FullName,
                Sender = a.Sender.FullName,
                Remarks = a.Remarks,
                TypeId = a.TypeId,
                Type = a.TypeId == 1? "Follow Up": a.TypeId==2?"Reminder":a.TypeId==3?"Re-Opened":a.TypeId==4?"Forwarded":"Status",
                StatusId = a.StatusId,
                TotalCount = a.TotalCount,

            }).ToListAsync();
        }
        public async Task<IEnumerable<DirectiveEvent>> GetAll()
        {
            return await _unitOfWork.GetRepositoryAsync<DirectiveEvent>().GetAll();
        }

        public async Task<DirectiveEvent> GetAsync(int id)
        {
            return await _unitOfWork.GetRepositoryAsync<DirectiveEvent>().GetOne(a => a.Id == id);
        }

        public async Task<IdObject> AddAsync(DirectiveEventVm vm)
        {

            DirectiveEvent evenT = _mapper.Map<DirectiveEvent>(vm);
            evenT.CreatedAt = DateTime.Now;
            evenT.CreatedBy = base.GetUserId();
            evenT.Id = 0;

            evenT = await _unitOfWork.GetRepositoryAsync<DirectiveEvent>().Insert(evenT);


            return new IdObject(evenT.Id);
        }


        public async Task Update(DirectiveEventVm vm)
        {
            DirectiveEvent evenT = await _unitOfWork.GetRepositoryAsync<DirectiveEvent>().GetOne(a => a.Id == vm.Id);
            if (evenT != null)
            {
               
                evenT.UpdatedAt = DateTime.Now;
                evenT.UpdatedBy = base.GetUserId();
                await _unitOfWork.GetRepositoryAsync<DirectiveEvent>().Update(evenT.Id, evenT);
            }
        }


        public async Task Delete(DirectiveEventVm vm)
        {
            DirectiveEvent evenT = await _unitOfWork.GetRepositoryAsync<DirectiveEvent>().GetOne(a => a.Id == vm.Id);
            if (evenT != null)
            {

                evenT.DeletedAt = DateTime.Now;
                evenT.DeletedBy = base.GetUserId();
                evenT.IsDeleted = true;
                await _unitOfWork.GetRepositoryAsync<DirectiveEvent>().Update(evenT.Id, evenT, false, false);
            }
        }
    }
}
