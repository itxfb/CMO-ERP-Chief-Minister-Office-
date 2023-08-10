using AutoMapper;
using pk.gov.pitb.cmo.directive.domain.Contracts;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels;
using System.Security.Claims;
using pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;
using pk.gov.pitb.cmo.directive.web.HRMIS;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.HRMIS;
using pk.gov.pitb.cmo.directive.domain.Data.Abstractions;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;

namespace pk.gov.pitb.cmo.directive.business.Handlers
{
    public class EmployeeHandler : BaseHandler
    {
        readonly IMapper _mapper;
        readonly IUnitOfWork _unitOfWork;
        private IFileManager fileManager;

        public EmployeeHandler(IUnitOfWork unitOfWork, ClaimsPrincipal claimsPrincipal, IFileManager fileManager, IMapper mapper) : base(unitOfWork, claimsPrincipal, mapper)
        {
            this.fileManager = fileManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<Gender>> GetAllGenderAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<Gender>().GetAll();

            return data.ToList();
        }
        public async Task<List<MartialStatus>> GetAllMartialStatusAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<MartialStatus>().GetAll();

            return data.ToList();
        }
        public async Task<List<AccessColor>> getAccessColourAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<AccessColor>().GetAll();

            return data.ToList();
        }
        public async Task<List<CardType>> getCardTypeAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<CardType>().GetAll();

            return data.ToList();

        }
        public async Task<List<EmployeesColor>> getEmployeeColourAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<EmployeesColor>().GetAll();

            return data.ToList();
        }
        public async Task<List<EmployeesStatus>> getEmployeeStatusAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<EmployeesStatus>().GetAll();

            return data.ToList();
        }
        public async Task<List<EmployeesSubStatus>> getEmployeeSubStatusAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<EmployeesSubStatus>().GetAll();

            return data.ToList();
        }
        public async Task<List<PostNature>> getPostNatureAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<PostNature>().GetAll();

            return data.ToList();
        }
        public async Task<List<PostsPresently>> getPostPresentlyAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<PostsPresently>().GetAll();

            return data.ToList();
        }
        public async Task<List<ServiceGroup>> getServiceGroupAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<ServiceGroup>().GetAll();

            return data.ToList();

        }
        public async Task<List<Shift>> getShiftsAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<Shift>().GetAll();

            return data.ToList();
        }
        public async Task<List<JobType>> getJobTypeAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<JobType>().GetAll();

            return data.ToList();
        }
        public async Task<List<Designation>> getDesignationAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<Designation>().GetAll();

            return data.ToList();
        }
        public async Task<List<Department>> getDepartmentsAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<Department>().GetAll();

            return data.ToList();
        }
        public async Task<List<Bpsscale>> getScalesAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<Bpsscale>().GetAll();

            return data.ToList();
        }
        public async Task<List<Qualification>> getQualificationAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<Qualification>().GetAll();

            return data.ToList();
        }
        public async Task<List<Domicile>> getDomicilesAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<Domicile>().GetAll();

            return data.ToList();
        }
        public async Task<List<Religion>> getReligionsAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<Religion>().GetAll();

            return data.ToList();
        }
        public async Task<List<BloodGroup>> getBloodGroupsAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<BloodGroup>().GetAll();

            return data.ToList();
        }
        public async Task<List<AppointmentMode>> getAppointmentModeAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<AppointmentMode>().GetAll();

            return data.ToList();
        }
        public async Task<List<AccessColor>> getAccessColorsAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<AccessColor>().GetAll();

            return data.ToList();
        }
        public async Task<List<Country>> getCountriesAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<Country>().GetAll();

            return data.ToList();
        }
        public async Task<List<PoliceStation>> getgetPoliceStationAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<PoliceStation>().GetAll();

            return data.ToList();
        }
        public async Task<List<Office>> getOfficesAsync()
        {
            var data = await _unitOfWork.GetRepositoryAsync<Office>().GetAll();

            return data.ToList();
        }

        public async Task<IdObject> File(Employee vm)
        {
            try
            {
                var pathName = "HRMISPath";
                var files = vm.EmployeeAttachments; ;
                Attachment att = null;
                for (var i = 0; i < files.Count(); i++)
                {
                    var file = files.Files[i];
                    var Attachmentpath = fileManager.Save(files.Files[i], files.Files[i].FileName, pathName, "HRMIS");
                    var attachmentType = await _unitOfWork.GetRepositoryAsync<AttachmentType>().GetOne(a => a.Name == "HRMIS");
                    if (attachmentType == null)
                    {

                        attachmentType = _unitOfWork.GetRepositoryAsync<AttachmentType>().Insert(new AttachmentType { Name = "HRMIS", Abbreviation = "HR" }).Result;
                    }


                    att = new Attachment()
                    {

                        FileExtension = Path.GetExtension(files.Files[i].FileName),
                        ContentType = file.ContentType,
                        DisplayName = file.FileName,
                        Path = Attachmentpath,
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                        CreatedBy = base.GetUserId(),
                        AttachmentId = 43,
                        AttachmentTypeId = attachmentType.Id

                    };

                    await _unitOfWork.GetRepositoryAsync<Attachment>().Insert(att);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new IdObject(1);

        }
        public async Task<IdObject> AddAsync(Employee vm)
        {
            vm.Id = 0;
            Employee model = _mapper.Map<Employee>(vm);

            vm.EmployeesColors.ForEach(color => color.CreatedBy = base.GetUserId());
            vm.EmployeesPostingsInfos[0].CreatedBy = base.GetUserId();
            vm.EmployeesJobsInfos[0].CreatedBy = base.GetUserId();
            vm.CreatedBy = base.GetUserId();

            var files = vm.EmployeeAttachments; ;
            Attachment att = null;

            var insertedEmpRecord = await _unitOfWork.GetRepositoryAsync<Employee>().Insert(vm);
            var EmpFileAttachment = vm.EmployeeAttachments;

            for (var i = 0; i < files.Count(); i++)
            {
                var file = files.Files[i];
                var Attachmentpath = fileManager.Save(files.Files[i], files.Files[i].FileName, "HRMISPath", "HRMIS");
                var attachmentType = await _unitOfWork.GetRepositoryAsync<AttachmentType>().GetOne(a => a.Name == "HRMIS");
                if (attachmentType == null)
                {

                    attachmentType = _unitOfWork.GetRepositoryAsync<AttachmentType>().Insert(new AttachmentType { Name = "HRMIS", Abbreviation = "HR" }).Result;
                }


                att = new Attachment()
                {

                    FileExtension = Path.GetExtension(files.Files[i].FileName),
                    ContentType = file.ContentType,
                    DisplayName = file.FileName,
                    Path = Attachmentpath,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    CreatedBy = base.GetUserId(),
                    AttachmentId = insertedEmpRecord.Id,
                    AttachmentTypeId = attachmentType.Id

                };

                await _unitOfWork.GetRepositoryAsync<Attachment>().Insert(att);

            }

            return new IdObject(insertedEmpRecord.Id);
        }

        public async Task<List<Employee>> getAllEmployeesAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _unitOfWork.GetRepositoryAsync<Employee>().Count(a => !a.IsDeleted);

            var data = await _unitOfWork.GetRepositoryAsync<Employee>().GetAllQueryable().Where(a => !a.IsDeleted)
                .Include(e => e.EmployeesColors)
                    .ThenInclude(ec => ec.AccessColor)

                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.Department)
                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.Designation)
                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.JobType)
                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.AppointmentMode)
                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.ServiceGroup)
                 .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.Bpsscale)

                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.CardType)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.EmployeeStatus)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.EmployeeSubStatus)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.MartialStatus)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.PostNature)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.PostPresently)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.Shift)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.Office)
                .Include(e => e.EmployeesPostingsInfos)
                 .ThenInclude(p => p.Police)

                 .ToListAsync();
            data = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            if (data.FirstOrDefault() != null)
            {
                data.First().TotalCount = totalCount;
            }
            return data;
        }

        public async Task<List<Employee>> SearchEmployee(EmployeeSearch model, int pageNumber, int pageSize)
        {
            var totalCount = await _unitOfWork.GetRepositoryAsync<Employee>().Count(a => !a.IsDeleted);

            var query = _unitOfWork.GetRepositoryAsync<Employee>()
                        .GetAllQueryable();

            query = query
                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.Department)
                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.Designation)
                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.Bpsscale)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.Office);

            var parameter = Expression.Parameter(typeof(Employee), "e");
            var conditions = new List<Expression>();
            Expression propertyExpression;

            var properties = typeof(EmployeeSearch).GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(model);
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                {
                    if (value is int intValue && intValue > 0)
                    {

                        if (property.Name == "DesignationId")
                        {
                            var joinExpression = Expression.Property(parameter, "EmployeesJobsInfos");
                            var firstItemExpression = Expression.Call(typeof(Enumerable), "FirstOrDefault", new[] { typeof(EmployeesJobsInfo) }, joinExpression);
                            var designationProperty = Expression.Property(firstItemExpression, "Designation");
                            var designationNameProperty = Expression.Property(designationProperty, "Id");
                            var propertyasString = Expression.Call(designationNameProperty, typeof(object).GetMethod("ToString"));
                            var containsExpression = Expression.Call(propertyasString, "Contains", Type.EmptyTypes, Expression.Constant(value.ToString()));
                            conditions.Add(containsExpression);
                        }
                        else if (property.Name == "DepartmentId")
                        {
                            var joinExpression = Expression.Property(parameter, "EmployeesJobsInfos");
                            var firstItemExpression = Expression.Call(typeof(Enumerable), "FirstOrDefault", new[] { typeof(EmployeesJobsInfo) }, joinExpression);
                            var designationProperty = Expression.Property(firstItemExpression, "Department");
                            var designationNameProperty = Expression.Property(designationProperty, "Id");
                            var propertyasString = Expression.Call(designationNameProperty, typeof(object).GetMethod("ToString"));
                            var containsExpression = Expression.Call(propertyasString, "Contains", Type.EmptyTypes, Expression.Constant(value.ToString()));
                            conditions.Add(containsExpression);
                        }
                        else if (property.Name == "OfficeNameId")
                        {
                            var joinExpression = Expression.Property(parameter, "EmployeesPostingsInfos");
                            var firstItemExpression = Expression.Call(typeof(Enumerable), "FirstOrDefault", new[] { typeof(EmployeesPostingsInfo) }, joinExpression);
                            var officeProperty = Expression.Property(firstItemExpression, "Office");
                            var designationNameProperty = Expression.Property(officeProperty, "Id");
                            var propertyasString = Expression.Call(designationNameProperty, typeof(object).GetMethod("ToString"));
                            var containsExpression = Expression.Call(propertyasString, "Contains", Type.EmptyTypes, Expression.Constant(value.ToString()));
                            conditions.Add(containsExpression);
                        }
                        else if (property.Name == "Bps")
                        {
                            var joinExpression = Expression.Property(parameter, "EmployeesJobsInfos");
                            var firstItemExpression = Expression.Call(typeof(Enumerable), "FirstOrDefault", new[] { typeof(EmployeesJobsInfo) }, joinExpression);
                            var designationProperty = Expression.Property(firstItemExpression, "Bpsscale");
                            var designationNameProperty = Expression.Property(designationProperty, "Id");
                            var propertyasString = Expression.Call(designationNameProperty, typeof(object).GetMethod("ToString"));
                            var containsExpression = Expression.Call(propertyasString, "Contains", Type.EmptyTypes, Expression.Constant(value.ToString()));
                            conditions.Add(containsExpression);
                        }

                    }
                    else if (value.GetType() == typeof(string) || value.GetType() == typeof(string))
                    {
                        if (property.Name == "JoiningDate")
                        {
                            var joinExpression = Expression.Property(parameter, "EmployeesJobsInfos");
                            var firstItemExpression = Expression.Call(typeof(Enumerable), "FirstOrDefault", new[] { typeof(EmployeesJobsInfo) }, joinExpression);
                            var designationProperty = Expression.Property(firstItemExpression, "JoiningDate");
                            var propertyasString = Expression.Call(designationProperty, typeof(object).GetMethod("ToString"));
                            var containsExpression = Expression.Call(propertyasString, "Contains", Type.EmptyTypes, Expression.Constant(value.ToString()));
                            conditions.Add(containsExpression);
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            propertyExpression = Expression.Property(parameter, property.Name);

                            var containsExpression = Expression.Call(propertyExpression, "Contains", Type.EmptyTypes, Expression.Constant(value));
                            conditions.Add(containsExpression);
                        }
                    }

                }
            }

            var combinedCondition = conditions.Any() ? conditions.Aggregate(Expression.AndAlso) : null;
            var predicate = combinedCondition != null ? Expression.Lambda<Func<Employee, bool>>(combinedCondition, parameter) : null;

            if (predicate != null)
            {
                query = query.Where(predicate);
                var count = query.Count();
                if (count != 0)
                {
                    query.First().TotalCount = count;
                }
            }
            else
            {
                query = query;
                pageNumber = 0;
                query.First().TotalCount = totalCount;
            }
            int offset = (pageNumber - 1) * pageSize;

            var filteredQuery = query
                .Skip(offset > -0 ? offset : 0)
                .Take(pageSize);

            return await filteredQuery.ToListAsync();
        }

        public async Task<EmployeVM> GetAsync(IdObject id)
        {

            //var idd = Convert.ToInt32(id);
            EmployeVM vmModel = new EmployeVM();
       
            var result = await _unitOfWork.GetRepositoryAsync<Employee>().GetAllQueryable().Where(x => x.Id == id.Id && !x.IsDeleted).Include(e=>e.EmployeesColors)
                
                .Include(e => e.EmployeesJobsInfos) 
                    .ThenInclude(j => j.Department)
                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.Designation)
                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.JobType)
                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.AppointmentMode)
                .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.ServiceGroup)
                 .Include(e => e.EmployeesJobsInfos)
                    .ThenInclude(j => j.Bpsscale)

                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.CardType)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.EmployeeStatus)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.EmployeeSubStatus)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.MartialStatus)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.PostNature)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.PostPresently)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.Shift)
                .Include(e => e.EmployeesPostingsInfos)
                    .ThenInclude(p => p.Office)
                .Include(e => e.EmployeesPostingsInfos)
                 .ThenInclude(p => p.Police).ToListAsync();

            var employee = result.FirstOrDefault();
            var employeeJobsInfo = result.FirstOrDefault()?.EmployeesJobsInfos.FirstOrDefault();
            var postingInfo = result.FirstOrDefault()?.EmployeesPostingsInfos.FirstOrDefault();

            var domicile = await _unitOfWork.GetRepositoryAsync<Domicile>().GetOne(x=>x.Id==employee.Domicile);
            var country = await _unitOfWork.GetRepositoryAsync<Country>().GetOne(x => x.Id == employee.Training);
            var gender = await _unitOfWork.GetRepositoryAsync<Gender>().GetOne(x => x.Id == employee.Gender);
            var religion = await _unitOfWork.GetRepositoryAsync<Religion>().GetOne(x => x.Id == employee.Religion);
            var bloodGroup = await _unitOfWork.GetRepositoryAsync<BloodGroup>().GetOne(x => x.Id == employee.BloodGroup);
            var qualification = await _unitOfWork.GetRepositoryAsync<Qualification>().GetOne(x => x.Id == employee.AcademicQualification);
            var attachmentRecord = _unitOfWork.GetRepositoryAsync<Attachment>().GetOne(x => x.AttachmentTypes.Abbreviation == "HR" && x.AttachmentId == id.Id);

            vmModel.EmployeeName = employee?.EmployeeName;
            //vmModel.EmployeeName = result.FirstOrDefault()?.Bld.Name;
            vmModel.FatherName = employee?.FatherName;
            vmModel.DateBirth = result.FirstOrDefault()?.DateBirth.Date;
            vmModel.Religion = employee?.Religion;
            vmModel.ReligionName = religion?.Name;
            vmModel.Cnic = employee?.Cnic;
            vmModel.BloodGroup = employee?.BloodGroup;
            vmModel.BloodGroupName = bloodGroup?.Name;
            vmModel.Gender = employee?.Gender;
            vmModel.GenderName = gender?.GenderType;
            vmModel.PresentAddress = employee?.PresentAddress;
            vmModel.PermanentAddress = employee?.PermanentAddress;
            vmModel.Training = employee?.Training;
            vmModel.TrainingName = country?.CountryName;
            vmModel.Domicile = employee?.Domicile;
            vmModel.DomicileName = domicile?.ProvinceName;
            vmModel.Mobile = employee?.Mobile;
            vmModel.Phone = employee?.Phone;
            vmModel.OfficialEmail = employee?.OfficialEmail;
            vmModel.PersonalEmail = employee?.PersonalEmail;
            vmModel.AcademicQualification = employee?.AcademicQualification;
            vmModel.AcademicQualificationName = qualification?.DegreeName;
            vmModel.ImagePath = attachmentRecord?.Result.Path;
            vmModel.FileNo = employeeJobsInfo?.FileNo;
            vmModel.DepartmentId = employeeJobsInfo?.DepartmentId;
            vmModel.DepartmentName = employeeJobsInfo?.Department.Name;
            vmModel.JobTypeId = employeeJobsInfo?.JobTypeId;
            vmModel.JobTypeName = employeeJobsInfo?.JobType.JobTypeName;
            vmModel.DesignationId = employeeJobsInfo?.DesignationId;
            vmModel.DesignationName = employeeJobsInfo?.Designation.Name;
            vmModel.Bps = employeeJobsInfo?.Bps;
            vmModel.BpsName = employeeJobsInfo?.Bpsscale.Name;
            vmModel.JoiningDate = employeeJobsInfo?.JoiningDate.Date;
            vmModel.Ntn = employeeJobsInfo?.Ntn;
            vmModel.VendorNo = employeeJobsInfo?.VendorNo;
            vmModel.Pifra = employeeJobsInfo?.Pifra;
            vmModel.Ddoname = employeeJobsInfo?.Ddoname;
            vmModel.Ddocode = employeeJobsInfo?.Ddocode;
            vmModel.Pifra = employeeJobsInfo?.Pifra;
            vmModel.BasicPay = employeeJobsInfo?.BasicPay;
            vmModel.AppointmentModeId = employeeJobsInfo?.AppointmentModeId;
            vmModel.AppointmentModeName = employeeJobsInfo?.AppointmentMode.AppointmentName;
            vmModel.ServiceGroupId = employeeJobsInfo?.ServiceGroupId;
            vmModel.ServiceGroupName = employeeJobsInfo?.ServiceGroup.ServiceName;
            vmModel.Ctpnumber = employeeJobsInfo?.Ctpnumber;

            vmModel.OfficeNameId = postingInfo?.OfficeNameId;
            vmModel.OfficeName = postingInfo?.Office.Name;
            vmModel.PostPresentlyId = postingInfo?.PostPresentlyId;
            vmModel.PostPresentlyName = postingInfo?.PostPresently?.PostPresentlyName;
            vmModel.PostNatureId = postingInfo?.PostNatureId;
            vmModel.PostNatureName = postingInfo?.PostNature.PostName;
            vmModel.JoiningDateCm = postingInfo?.JoiningDateCm.Date;
            vmModel.ShiftId = postingInfo?.ShiftId;
            vmModel.ShiftName = postingInfo?.Shift.ShiftName;
            vmModel.PoliceStation = postingInfo?.PoliceStation;
            vmModel.PoliceStationName = postingInfo?.Police.StationName;
            vmModel.EmployeeStatusId = postingInfo?.EmployeeStatusId;
            vmModel.EmployeeStatusName = postingInfo?.EmployeeStatus.StatusName;
            vmModel.EmployeeSubStatusId = postingInfo?.EmployeeSubStatusId;
            vmModel.EmployeeSubStatusName = postingInfo?.EmployeeSubStatus?.EmployeeSubName;
            vmModel.RepatriationDate = postingInfo?.RepatriationDate.Value.Date;
            vmModel.CardTypeId = postingInfo?.CardTypeId;
            vmModel.CardTypeName = postingInfo?.CardType.CardName;
            vmModel.MartialStatusId = postingInfo?.MartialStatusId;
            vmModel.MartialStatusName = postingInfo?.MartialStatus.Status;
            vmModel.CardIssued = postingInfo?.CardIssued;

            vmModel.Remarks = postingInfo?.Remarks;
            vmModel.colors = new List<ColorAccess>();

            for (int i = 0; i < result[0].EmployeesColors.Count(); i++)
            {
                ColorAccess cAccess = new ColorAccess();
                cAccess.Id = result[0].EmployeesColors[i].AccessColorId;

                vmModel.colors.Add(cAccess);
            }
            return vmModel;

        }
        public async Task UpdateAsync(Employee model)
        {

            model.EmployeesColors.ForEach(color =>
            {
                color.CreatedBy = base.GetUserId();
                color.EmployeeId = model.Id;
            });
            model.EmployeesPostingsInfos[0].UpdatedBy = base.GetUserId();
            model.EmployeesJobsInfos[0].UpdatedBy = base.GetUserId();
            model.CreatedBy = base.GetUserId();

            var files = model.EmployeeAttachments; ;
            Attachment att = null;

            var insertedEmpRecord = await _unitOfWork.GetRepositoryAsync<Employee>().Update(model.Id, model);
            if (model.EmployeesJobsInfos.Count > 0)
            {
                EmployeesJobsInfo jobs = await _unitOfWork.GetRepositoryAsync<EmployeesJobsInfo>().GetOne(x => x.EmployeeId == model.Id);
                if (jobs != null)
                {
                    jobs.EmployeeId = model.Id;
                    jobs.DesignationId = model.EmployeesJobsInfos[0].DesignationId;
                    jobs.JobTypeId = model.EmployeesJobsInfos[0].JobTypeId;
                    jobs.ServiceGroupId = model.EmployeesJobsInfos[0].ServiceGroupId;
                    jobs.DepartmentId = model.EmployeesJobsInfos[0].DepartmentId;
                    jobs.AppointmentModeId = model.EmployeesJobsInfos[0].AppointmentModeId;
                    jobs.BasicPay = model.EmployeesJobsInfos[0].BasicPay;
                    jobs.Ctpnumber = model.EmployeesJobsInfos[0].Ctpnumber;
                    jobs.Ddocode = model.EmployeesJobsInfos[0].Ddocode;
                    jobs.Ddoname = model.EmployeesJobsInfos[0].Ddoname;
                    jobs.FileNo = model.EmployeesJobsInfos[0].FileNo;
                    jobs.JoiningDate = model.EmployeesJobsInfos[0].JoiningDate;
                    jobs.Pifra = model.EmployeesJobsInfos[0].Pifra;
                    jobs.VendorNo = model.EmployeesJobsInfos[0].VendorNo;
                    jobs.Bps = model.EmployeesJobsInfos[0].Bps;
                    jobs.Ntn = model.EmployeesJobsInfos[0].Ntn;
                    jobs.UpdatedAt = DateTime.Now;
                    jobs.UpdatedBy = base.GetUserId();
                }
                await _unitOfWork.GetRepositoryAsync<EmployeesJobsInfo>().Update(jobs.Id, jobs);
            }

            // Update the first item in EmployeesPostingsInfos

            if (model.EmployeesPostingsInfos.Count > 0)
            {
                EmployeesPostingsInfo postingjobs = await _unitOfWork.GetRepositoryAsync<EmployeesPostingsInfo>().GetOne(x => x.EmployeeId == model.Id);
                if (postingjobs != null)
                {
                    postingjobs.EmployeeId = model.Id;
                    postingjobs.OfficeNameId = model.EmployeesPostingsInfos[0].OfficeNameId;
                    postingjobs.PostPresentlyId = model.EmployeesPostingsInfos[0].PostPresentlyId;
                    postingjobs.PostNatureId = model.EmployeesPostingsInfos[0].PostNatureId;
                    postingjobs.JoiningDateCm = model.EmployeesPostingsInfos[0].JoiningDateCm;
                    postingjobs.ShiftId = model.EmployeesPostingsInfos[0].ShiftId;
                    postingjobs.PoliceStation = model.EmployeesPostingsInfos[0].PoliceStation;
                    postingjobs.EmployeeStatusId = model.EmployeesPostingsInfos[0].EmployeeStatusId;
                    postingjobs.EmployeeSubStatusId = model.EmployeesPostingsInfos[0].EmployeeSubStatusId;
                    postingjobs.RepatriationDate = model.EmployeesPostingsInfos[0].RepatriationDate;
                    postingjobs.CardTypeId = model.EmployeesPostingsInfos[0].CardTypeId;
                    postingjobs.MartialStatusId = model.EmployeesPostingsInfos[0].MartialStatusId;
                    postingjobs.Remarks = model.EmployeesPostingsInfos[0].Remarks;
                    postingjobs.CardIssued = model.EmployeesPostingsInfos[0].CardIssued;
                    postingjobs.UpdatedAt = DateTime.Now;
                    postingjobs.UpdatedBy = base.GetUserId();
                }
                await _unitOfWork.GetRepositoryAsync<EmployeesPostingsInfo>().Update(postingjobs.Id, postingjobs);
            }

            if (model.EmployeesColors.Count > 0)
            {

                foreach (var c in model.EmployeesColors)
                {

                    EmployeesColor empClrObj = await _unitOfWork.GetRepositoryAsync<EmployeesColor>().GetOne(x => x.AccessColorId == c.AccessColorId && x.EmployeeId == model.Id);
                    if (empClrObj != null)
                    {
                        empClrObj.AccessColorId = empClrObj.AccessColorId;
                        empClrObj.UpdatedAt = DateTime.Now;
                        empClrObj.UpdatedBy = base.GetUserId();

                        await _unitOfWork.GetRepositoryAsync<EmployeesColor>().AddUpdate(empClrObj, empClrObj.Id);
                    }
                    else
                    {
                        EmployeesColor clr = new EmployeesColor();
                        clr.EmployeeId = c.EmployeeId;
                        clr.AccessColorId = c.AccessColorId;
                        clr.CreatedAt = DateTime.Now;
                        clr.CreatedBy = base.GetUserId();
                        await _unitOfWork.GetRepositoryAsync<EmployeesColor>().AddUpdate(clr);

                    }
                    var listAllAssignedEmployeeColor = _unitOfWork.GetRepositoryAsync<EmployeesColor>().GetAllQueryable().Where(x => x.EmployeeId == model.Id).ToList();
                    var deletedList = listAllAssignedEmployeeColor.Except(model.EmployeesColors).ToList();

                    var delList = listAllAssignedEmployeeColor.Where(x => !model.EmployeesColors.Any(y => y.AccessColorId == x.AccessColorId)).ToList();

                    foreach (var item in delList)
                    {
                        var del = _unitOfWork.GetRepositoryAsync<EmployeesColor>().Delete(item.Id);

                    }
                }
            }
            var EmpFileAttachment = model.EmployeeAttachments;

            for (var i = 0; i < files.Count(); i++)
            {
                var file = files.Files[i];
                var Attachmentpath = fileManager.Save(files.Files[i], files.Files[i].FileName, "HRMISPath", "HRMIS");
                var attachmentType = await _unitOfWork.GetRepositoryAsync<AttachmentType>().GetOne(a => a.Name == "HRMIS");
                Attachment rec = await _unitOfWork.GetRepositoryAsync<Attachment>().GetOne(x => x.AttachmentId == insertedEmpRecord.Id &&  x.AttachmentTypes.Abbreviation == "HR");
                rec = new Attachment()
                {
                    Id = rec.Id,
                    FileExtension = Path.GetExtension(files.Files[i].FileName),
                    ContentType = file.ContentType,
                    DisplayName = file.FileName,
                    Path = Attachmentpath,
                    AttachmentId = rec.AttachmentId,
                    AttachmentTypeId = rec.AttachmentTypeId,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = base.GetUserId(),
                };
                await _unitOfWork.GetRepositoryAsync<Attachment>().Update(rec.Id, rec);
            }
        }
        //public async Task<EmployeVM> GetProfile(IdObject id)
        //{
        //    EmployeVM vmModel = new EmployeVM();
        //    var result = await _unitOfWork.GetRepositoryAsync<Employee>().GetAllQueryable().Where(x => x.Id == id.Id && !x.IsDeleted).Include(x => x.EmployeesJobsInfos).Include(x => x.EmployeesPostingsInfos).Include(x => x.EmployeesColors).ToListAsync();

        //}
    }
}
