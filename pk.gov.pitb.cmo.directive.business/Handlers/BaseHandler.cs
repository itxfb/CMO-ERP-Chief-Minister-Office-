using pk.gov.pitb.cmo.directive.domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pk.gov.pitb.cmo.directive.domain.Models.Common;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Administration;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using pk.gov.pitb.cmo.directive.domain.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Buffers.Text;
using System.Data.Common;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Metadata;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Data.Abstractions;
using pk.gov.pitb.cmo.contracts;
using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;
using pk.gov.pitb.cmo.directive.domain.Enums;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Account;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class BaseHandler
    {
        internal readonly IUnitOfWork _unitOfWork;
        internal readonly IMapper _mapper;
        internal readonly ClaimsPrincipal _claims;

        public BaseHandler(IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal, IMapper mapper)
        {
            _claims = claimsPrincipal;

            _unitOfWork = unitOfWork;

            _mapper = mapper;


        }

        internal int GetUserId()
        {

            var id = _claims.Claims.FirstOrDefault();
            return Convert.ToInt32(id.Value);

            //return 101;
        }
        public async Task<object> GetData(string? tableName = "", int? skip = 0, int? take = 10, object? paramS = null, string? columns = "", string? dtoName = "")
        {
            // get context assembly
            var assembly = typeof(RepositoryContext).Assembly;


            

            // Get all loaded assemblies
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Get DTO assembly
            var filteredAssemblies = assemblies.Where(assembly => assembly.GetTypes().Any(type => type.Namespace == EntitiesEnum.DTOAssembly )).FirstOrDefault();
            
            var dto = filteredAssemblies.GetTypes().FirstOrDefault(dto => dto.Name == dtoName);
            //table for getting allowed assemblies

            var dictionary = new Dictionary<string, object>();

            var predicates = new Dictionary<string, string>();

            if (paramS != null)
            {
                dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(paramS?.ToString());
                predicates = JsonConvert.DeserializeObject<Dictionary<string, string>>(dictionary["predicates"]?.ToString());
            }

            // Search for the entity type by name within the assembly
            var entityType = assembly.GetTypes().FirstOrDefault(t => t.Name == tableName);

            // Get the GetRepositoryAsync method from the IUnitOfWork interface
            var getRepositoryAsyncMethod = typeof(IUnitOfWork).GetMethod("GetRepositoryAsync").MakeGenericMethod(entityType);

            // Invoke the GetRepositoryAsync method on the _uow instance
            var repository = getRepositoryAsyncMethod.Invoke(_unitOfWork, null);

            // Get the GetSkipTake method from the IRepositoryAsync<TEntity> interface
            var getSkipTakeMethod = typeof(IRepositoryAsync<>).MakeGenericType(entityType).GetMethod("GetSkipTakeWithInclude");

            // Get the GetCount method from the IRepositoryAsync<TEntity> interface
            var getCount = typeof(IRepositoryAsync<>).MakeGenericType(entityType).GetMethod("GetCount");





            //Preparing Predicates

            var parameter = Expression.Parameter(entityType, "a");

            var deletePredicate = Expression.Not(Expression.Property(parameter, "IsDeleted"));
            var predicatesExpressions = new List<Expression>();

            var finalPredicate = default(Expression);

            if (predicates != null && predicates.Count > 0)
            {
                foreach (var predicate in predicates)
                {
                    if (!string.IsNullOrEmpty(predicate.Value ?? ""))
                    {
                        var prop = predicate.Key;
                        var val = predicate.Value;

                        if (prop.Contains("Id"))
                        {
                            val = Utility.Decrypt(val).ToString();
                        }

                        var property = Expression.Property(parameter, prop);
                        var value = Expression.Constant(val);

                        var equals = default(BinaryExpression);

                        if (bool.TryParse(val, out bool boolValue))
                        {
                            value = Expression.Constant(boolValue);

                            var convertedProp = Expression.Convert(property, typeof(bool));

                            equals = Expression.Equal(convertedProp, value);
                        }
                        else if (int.TryParse(val, out int intValue))
                        {
                            value = Expression.Constant(intValue);

                            var convertedProp = Expression.Convert(property, typeof(int));

                            equals = Expression.Equal(convertedProp, value);
                        }
                        else if (DateTime.TryParse(val, out DateTime dateTime))
                        {
                            value = Expression.Constant(dateTime);
                            var convertedProp = Expression.Convert(property, typeof(DateTime));
                            equals = Expression.Equal(convertedProp, value);
                        }
                        else
                        {
                            equals = Expression.Equal(property, value);
                        }

                        predicatesExpressions.Add(equals);
                    }
                }



                // Combine predicatesExpressions using AndAlso operator
                if (predicatesExpressions.Count != 0)
                {

                    Expression combinedExpression = predicatesExpressions.Aggregate(Expression.AndAlso);

                    finalPredicate = Expression.Lambda(Expression.AndAlso(combinedExpression, deletePredicate), parameter);

                }
                else
                    finalPredicate = Expression.Lambda(deletePredicate, parameter);
            }

            else
                finalPredicate = Expression.Lambda(deletePredicate, parameter);

            if(dto==null)
            {
                dto = entityType;
            }
            // Create a generic type array with the type argument LetterVM
            var genericTypeArgs = new[] { dto };

            // Make the GetSkipTakeWithInclude method generic with LetterVM as the type argument
            getSkipTakeMethod = getSkipTakeMethod.MakeGenericMethod(genericTypeArgs);


            // Invoke the GetSkipTake method on the repository instance and await the result

            var dataTask = (Task)getSkipTakeMethod.Invoke(repository, new object[] { finalPredicate, skip, take, columns });


            await dataTask;

            var countTask = (Task)getCount.Invoke(repository, new object[] { Expression.Lambda(deletePredicate, parameter) });
            await countTask;

            // Access the data property using reflection
            var dataProperty = dataTask.GetType().GetProperty("Result");
            var data = dataProperty.GetValue(dataTask);


            // Access the data value using reflection
            var count = countTask.GetType().GetProperty("Result").GetValue(countTask);





            if (data is IEnumerable<object> enumerableData)
            {
                // Get the first item in the collection
                var firstRow = enumerableData.FirstOrDefault();


                if (firstRow != null)
                {

                    // Update the value of the 'totalCount' property
                    var totalCountProperty = firstRow.GetType().GetProperty("TotalCount");


                    // Convert totalCount to Nullable<Int64> before assigning
                    var totalCountValue = count != "" ? Convert.ToInt64(count) : 0;
                    totalCountProperty.SetValue(firstRow, totalCountValue);
                }




            }


            //Serialization for Ignoring Reference Loopings
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var res = JsonConvert.SerializeObject(data, settings);

            return res;

            //return data;



        }

        internal async Task<string> GetUserDepartmentIds()
        {
            var userId = GetUserId();
            var userDepartments = await _unitOfWork.GetRepositoryAsync<UserDepartments>().Get(a => a.UserId == userId && !a.IsDeleted);
            if (userDepartments.Any())
            {
                return string.Join(",", userDepartments.Select(a => a.DepartmentId));
            }
            return null;
            //return Convert.ToInt32(_user.Identity.Name);
        }

        public async Task<List<ConstituencyType>> GetAllConstituencyTypes(int? skip = -1, int? limit = -1, string? searchText = "")
        {
            IEnumerable<ConstituencyType>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<ConstituencyType>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, limit.Value == -1 ? 10 : limit.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<ConstituencyType>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, limit.Value == -1 ? 10 : limit.Value);

            }

            if (result.Count<ConstituencyType>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<ConstituencyType>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }
        public List<ProfileListVm> GetAllSignatories()
        {
            return _unitOfWork.GetRepositoryAsync<AppUser>().GetAllQueryable().Where(x => x.IsActive && x.IsAuthority).Select(u => new ProfileListVm() { Id = u.Id, Username = u.Username }).ToList();
        }
        public async Task<List<DepartmentType>> GetAllDepartmentTypesAsync(int? skip, int? take, string? searchText)
        {
            IEnumerable<DepartmentType>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<DepartmentType>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<DepartmentType>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<DepartmentType>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<DepartmentType>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }

        public async Task<List<Department>> GetAllDepartments(int? skip, int? take, string? searchText)
        {
            IEnumerable<Department>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<Department>().GetSkipTake(a => !a.IsDeleted && a.ParentId==null, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<Department>().GetSkipTake(a => !a.IsDeleted && a.ParentId == null && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<Department>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<Department>().GetCount(a => !a.IsDeleted && a.ParentId == null);

            return result.ToList();
        }


        public async Task<List<Office>> GetAllOffices(int? skip, int? take, string? searchText)
        {
            IEnumerable<Office>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<Office>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<Office>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<Office>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<Office>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }
        public async Task<List<District>> GetAllDistricts(int? skip, int? take, string? searchText)
        {

            IEnumerable<District>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<District>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<District>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<District>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<District>().GetCount(a => !a.IsDeleted);

            return result.ToList();

        }

        public async Task<List<Division>> GetAllDivisionsByUser(int? skip, int? take, string? searchText)
        {
            IEnumerable<Division>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<Division>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<Division>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<Division>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<Division>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }

        public async Task<List<Constituency>> GetAllConstituencies(int? skip, int? take, string? searchText)
        {
            IEnumerable<Constituency>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<Constituency>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);


            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<Constituency>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            result = result.Select(a =>
            {
                a.ConstituencyType = _unitOfWork.GetRepositoryAsync<ConstituencyType>().GetOne(b=>b.Id == a.ConstituencyTypeId).Result;
                return a;
            });


            if (result.Count<Constituency>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<Constituency>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }

        public async Task<List<DirectiveType>> GetAllDirectiveTypes(int? skip, int? take, string? searchText)
        {
            IEnumerable<DirectiveType>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<DirectiveType>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<DirectiveType>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<DirectiveType>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<DirectiveType>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }
        public async Task<List<Subject>> GetAllSubjectsAsync(int? skip, int? take, string? searchText)
        {
            IEnumerable<Subject>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<Subject>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<Subject>().GetSkipTake(a => !a.IsDeleted && (a.Code.Contains(searchText) || a.Description.Contains(searchText)), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<Subject>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<Subject>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }

        public async Task<List<Priority>> GetAllPrioritiesAsync(int? skip, int? take, string? searchText)
        {
            IEnumerable<Priority>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<Priority>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<Priority>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<Priority>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<Priority>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }
        public async Task<List<Party>> GetAllPartiesAsync(int? skip, int? take, string? searchText)
        {
            IEnumerable<Party>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<Party>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<Party>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<Party>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<Party>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }

        public async Task<List<Designation>> GetAllDesignationsAsync(int? skip, int? take, string? searchText)
        {
            IEnumerable<Designation>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<Designation>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<Designation>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<Designation>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<Designation>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }


        public async Task<List<AppUser>> GetApplicantsByConstituencyIdAsync(int id)
        {
            var result = await _unitOfWork.GetRepositoryAsync<AppUser>().Get(x =>
                x.UserTypeId == (int)ProfileTypeEnum.Elected &&
                x.ConstituencyId.Value == id);

            return result.ToList();
        }


        public async Task<List<Role>> GetAllRolesAsync(int? skip, int? take, string? searchText)
        {
            IEnumerable<Role>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<Role>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<Role>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<Role>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<Role>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }

        public async Task<List<NatureOfSchemes>> GetAllNatureOfSchemesAsync(int? skip, int? take, string? searchText)
        {
            IEnumerable<NatureOfSchemes>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                result = await _unitOfWork.GetRepositoryAsync<NatureOfSchemes>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }
            else
            {
                result = await _unitOfWork.GetRepositoryAsync<NatureOfSchemes>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);

            }

            if (result.Count<NatureOfSchemes>() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<NatureOfSchemes>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }


        public async Task<List<ProfileListVm>> GetAllProfilesAsync(int? skip, int? take, string? searchText)
        {
            IEnumerable<ProfileListVm>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
               var model = await _unitOfWork.GetRepositoryAsync<AppUser>().GetSkipTake(a => !a.IsDeleted && a.UserTypeId==2, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);
                result = _mapper.Map<List<ProfileListVm>>(model);
            }
            else
            {
                var model = await _unitOfWork.GetRepositoryAsync<AppUser>().GetSkipTake(a => !a.IsDeleted && a.UserTypeId == 2 && a.FullName.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);
                result = _mapper.Map<List<ProfileListVm>>(model);
            }

            if (result.Count() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<AppUser>().GetCount(a => !a.IsDeleted && a.UserTypeId == 2);

            return result.ToList();
        }

        public async Task<List<UserListVM>> GetAllUsersListAsync(int? skip,int? take, string? searchText)
        {
            IEnumerable<UserListVM>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                var model = await _unitOfWork.GetRepositoryAsync<AppUser>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);
                result = _mapper.Map<List<UserListVM>>(model);
            }
            else
            {
                var model = await _unitOfWork.GetRepositoryAsync<AppUser>().GetSkipTake(a => !a.IsDeleted && a.FullName.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);
                result = _mapper.Map<List<UserListVM>>(model);
            }

            if (result.Count() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<AppUser>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }
        public async Task<List<ProfileTypesVm>> GetAllProfileTypesAsync(int? skip, int? take, string? searchText)
        {
            IEnumerable<ProfileTypesVm>? result = default;
            if (string.IsNullOrEmpty(searchText))
            {
                var model = await _unitOfWork.GetRepositoryAsync<UserType>().GetSkipTake(a => !a.IsDeleted, skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);
                result = _mapper.Map<List<ProfileTypesVm>>(model);
            }
            else
            {
                var model = await _unitOfWork.GetRepositoryAsync<UserType>().GetSkipTake(a => !a.IsDeleted && a.Name.Contains(searchText), skip.Value == -1 ? 0 : skip.Value, take.Value == -1 ? 10 : take.Value);
                result = _mapper.Map<List<ProfileTypesVm>>(model);
            }

            if (result.Count() != 0)
                result.First().TotalCount = await _unitOfWork.GetRepositoryAsync<UserType>().GetCount(a => !a.IsDeleted);

            return result.ToList();
        }
    }
}
