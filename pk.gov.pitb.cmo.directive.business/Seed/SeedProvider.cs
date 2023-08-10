using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Entities;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using pk.gov.pitb.cmo.directive.domain.Enums;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace pk.gov.pitb.cmo.directive.business.Seed
{
    public class SeedProvider : ISeedProvider
    {
        private readonly RepositoryContext _db;
        private readonly IOptions<List<AppUser>> _dbOptions;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SeedProvider(RepositoryContext db, IOptions<List<AppUser>> dbOptions,
            IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _dbOptions = dbOptions;
            _hostingEnvironment = hostingEnvironment;
        }
        public void InitDevelopment()
        {
            _db.Database.EnsureCreated();

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {

                    SeedRequiredData();

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Handle exception or rollback the transaction
                    transaction.Rollback();
                    throw ex; // rethrow the exception to handle it further if needed
                }
            }


        }

        public void InitProduction()
        {

            _db.Database.EnsureCreated();
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                   

                    SeedRequiredData();

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Handle exception or rollback the transaction
                    transaction.Rollback();
                    throw ex; // rethrow the exception to handle it further if needed
                }
            }

        }

        private void SeedRequiredData()
        {



            if (!_db.AppUsers.Any())
            {

               

                var applicationType = new ApplicationType() { Name = "Web Domain" };

                var departmentType = new DepartmentType() { Name = "eDirective" };

                var usertype = new UserType() { Name = RolesEnum.CMS };

                var cmsRole = new Role() { Name = RolesEnum.CMS, IsSystem = true };

               

                _db.Roles.AddRange(
                    cmsRole,
                    new Role { Name = RolesEnum.Representative, IsSystem = true },
                     new Role { Name = RolesEnum.SectionOfficer, IsSystem = true }
                    );

                _db.UserTypes.AddRange(usertype, new UserType() { Name = RolesEnum.Representative }, new UserType() { Name = RolesEnum.FocalPerson });

                _db.SaveChanges();


                var user = new AppUser() { Username = "Admin", Password = "123", CreatedBy = 1, FullName = "Admin", UserTypeId = usertype.Id, IsLocked = false, IsTwoFactorEnabled = false, IsAuthority = true };

                var office = new Office() { Abbreviation = "AS (FS)", Name = "Admin Office" };

                _db.AppUsers.Add(
                   user
                    );

                _db.Offices.Add(office);

                _db.DepartmentTypes.Add(departmentType);


                _db.SaveChanges();


                _db.UserRoles.Add(new UserRoles { RoleId = cmsRole.Id, UserId = user.Id });

                var department = new Department() { Name = "HRMIS", DepartmentTypeId = departmentType.Id, Abbreviation = "HR", OfficeId = office.Id };

                _db.Departments.Add(department);

                _db.ApplicationTypes.Add(applicationType);


                _db.SaveChanges();

                _db.UserDepartments.Add(new UserDepartments { UserId = user.Id, DepartmentId = department.Id });

                var application = new Application() { ApplicationName = "eDirectives", ApplicationTypeId = applicationType.Id, AppURL="/edirectives/dashboard", IconPath= "assets/images/icons/Icon_2.png" };

                _db.Applications.Add(application);

                _db.Subjects.Add(new Subject { Code = "Code - 1", Description = "Code - 1 Description" });

                var province = new Province() { Name = "Punjab" };

                _db.Provinces.Add(province);


                var constituencyType = new ConstituencyType() { Name = "CT -1" };
                _db.ConstituencyTypes.Add(constituencyType);
               
                _db.SaveChanges();

                _db.Constituencies.Add(new Constituency() { Name = "Constituency -1", ConstituencyTypeId = constituencyType.Id, Code="CS - 1" });

                _db.Priorities.Add(new Priority() { Name = "P1", ColorHexCode = "#000000" });

                _db.NatureOfSchemes.Add(new NatureOfSchemes() { Name = "NOS - 1" });
             

                var feature = new Feature { ApplicationId = application.Id, Name = "Dashboard", DisplayName = "Dashboard", Path = "/edirectives/dashboard" };
                var feature2 = new Feature { ApplicationId = application.Id, Name = "Administration", DisplayName = "Administration", Path = "#" };
                var feature3 = new Feature { ApplicationId = application.Id, Name = "Directives", DisplayName = "Directives", Path = "#" };


                _db.Features.AddRange(feature, feature2,feature3);


                _db.SaveChanges();

                //With parent Adminstration
                var feature4 = new Feature { ApplicationId = application.Id, Name = "Role", DisplayName = "Role", Path = "/edirectives/role", ParentId = feature2.Id };
                var feature5 = new Feature { ApplicationId = application.Id, Name = "Feature", DisplayName = "Feature", Path = "/edirectives/features", ParentId = feature2.Id };
                var feature6 = new Feature { ApplicationId = application.Id, Name = "Profile", DisplayName = "Profile", Path = "/edirectives/profile", ParentId = feature2.Id };

                //With parent Directives
                var feature7 = new Feature { ApplicationId = application.Id, Name = "Add Directive", DisplayName = "Add Directive", Path = "/edirectives/add-directive", ParentId = feature3.Id };
                var feature8 = new Feature { ApplicationId = application.Id, Name = "View Directives", DisplayName = "View Directives", Path = "/edirectives/view-directives", ParentId = feature3.Id };

                _db.Features.AddRange(feature4,feature5, feature6,feature7,feature8);

                _db.SaveChanges();

                _db.RoleFeaturePermissions.AddRange(new RoleFeaturePermission() { ApplicationId = application.Id, FeatureId = feature.Id, RoleId = cmsRole.Id },
                                                    new RoleFeaturePermission() { ApplicationId = application.Id, FeatureId = feature2.Id, RoleId = cmsRole.Id },
                                                    new RoleFeaturePermission() { ApplicationId = application.Id, FeatureId = feature3.Id, RoleId = cmsRole.Id },
                                                    new RoleFeaturePermission() { ApplicationId = application.Id, FeatureId = feature4.Id, RoleId = cmsRole.Id },
                                                    new RoleFeaturePermission() { ApplicationId = application.Id, FeatureId = feature5.Id, RoleId = cmsRole.Id },
                                                    new RoleFeaturePermission() { ApplicationId = application.Id, FeatureId = feature6.Id, RoleId = cmsRole.Id },
                                                    new RoleFeaturePermission() { ApplicationId = application.Id, FeatureId = feature7.Id, RoleId = cmsRole.Id },
                                                    new RoleFeaturePermission() { ApplicationId = application.Id, FeatureId = feature8.Id, RoleId = cmsRole.Id }
                                                    );


                _db.UserFeaturePermissions.AddRange(new UserFeaturePermission() { ApplicationId = application.Id, FeatureId = feature.Id, UserId = user.Id },
                                                    new UserFeaturePermission() { ApplicationId = application.Id, FeatureId = feature2.Id, UserId = user.Id },
                                                    new UserFeaturePermission() { ApplicationId = application.Id, FeatureId = feature3.Id, UserId = user.Id },
                                                    new UserFeaturePermission() { ApplicationId = application.Id, FeatureId = feature4.Id, UserId = user.Id },
                                                    new UserFeaturePermission() { ApplicationId = application.Id, FeatureId = feature5.Id, UserId = user.Id },
                                                    new UserFeaturePermission() { ApplicationId = application.Id, FeatureId = feature6.Id, UserId = user.Id },
                                                    new UserFeaturePermission() { ApplicationId = application.Id, FeatureId = feature7.Id, UserId = user.Id },
                                                    new UserFeaturePermission() { ApplicationId = application.Id, FeatureId = feature8.Id, UserId = user.Id }
                                                    );


                var division = new Division() { Name = "Lahore", ProvinceId = province.Id };

                _db.Divisions.Add(division);

                _db.SaveChanges();

                _db.Districts.Add(new District() { Name = "Lahore", DivisionId = division.Id });

                _db.DirectiveTypes.AddRange(new DirectiveType() { Name = "Multiple" },
                                            new DirectiveType() { Name = "Single" }
                                           );

                _db.SaveChanges();
            }


        }





    }

}
