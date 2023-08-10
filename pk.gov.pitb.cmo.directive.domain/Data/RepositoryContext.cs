using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using pk.gov.pitb.cmo.directive.domain.Entities.Common;
using pk.gov.pitb.cmo.directive.domain.Entities.eDirectives;
using pk.gov.pitb.cmo.directive.domain.Entities.Hrmis;
using pk.gov.pitb.cmo.directive.domain.Entities.HRMIS;
using pk.gov.pitb.cmo.directive.web.HRMIS;
using static System.Net.Mime.MediaTypeNames;

namespace pk.gov.pitb.cmo.directive.domain.Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User Feature Relations

            modelBuilder.Entity<UserFeaturePermission>()
            .HasOne<AppUser>(s => s.User)
            .WithMany(g => g.UserFeaturePermissions)
            .HasForeignKey(s => s.UserId)
            .IsRequired(false);

            modelBuilder.Entity<UserFeaturePermission>()
             .HasOne<Feature>(s => s.Feature)
             .WithMany(g => g.UserFeatures)
             .HasForeignKey(s => s.FeatureId)
             .OnDelete(DeleteBehavior.Restrict)
             .IsRequired(true);


            modelBuilder.Entity<UserFeaturePermission>()
            .HasOne<Entities.Common.Application>(s => s.Application)
            .WithMany(g => g.UserFeatures)
            .HasForeignKey(s => s.ApplicationId)
            .IsRequired(true);


            //Role Feature Relations
            modelBuilder.Entity<RoleFeaturePermission>()
               .HasOne<Role>(s => s.Role)
               .WithMany(g => g.RolePermissions)
               .HasForeignKey(s => s.RoleId)
               .IsRequired(true);

            modelBuilder.Entity<UserRoles>()
              .HasOne<AppUser>(s => s.User)
              .WithMany(g => g.UserRoles)
              .HasForeignKey(s => s.UserId)
              .IsRequired(true);

            ////Feature-Role Relation
            modelBuilder.Entity<RoleFeaturePermission>()
              .HasOne<Feature>(s => s.Feature)
              .WithMany(g => g.RoleFeatures)
              .HasForeignKey(s => s.FeatureId)
              .OnDelete(DeleteBehavior.Restrict);




            //AppUser Relations

            modelBuilder.Entity<AppUser>()
               .HasOne<Constituency>(s => s.Constituency)
               .WithMany(ad => ad.User)
               .HasForeignKey(ad => ad.ConstituencyId)
               .IsRequired(false);

            modelBuilder.Entity<AppUser>()
                  .HasOne<Designation>(s => s.Designation)
                  .WithMany(ad => ad.User)
                  .HasForeignKey(ad => ad.DesignationId)
                  .IsRequired(false);

            modelBuilder.Entity<AppUser>()
                   .HasOne<Party>(s => s.Party)
                   .WithMany(ad => ad.User)
                   .HasForeignKey(ad => ad.PartyId)
                   .IsRequired(false);

            modelBuilder.Entity<AppUser>()
                  .HasOne<District>(s => s.District)
                  .WithMany(ad => ad.User)
                  .HasForeignKey(ad => ad.ResidentDistrictId)
                  .IsRequired(false);

            modelBuilder.Entity<AppUser>()
                  .HasOne<UserType>(s => s.UserType)
                  .WithMany(ad => ad.Users)
                  .HasForeignKey(ad => ad.UserTypeId)
                  .IsRequired(true);
         

            //Application Relations
            modelBuilder.Entity<Entities.Common.Application>()
                .HasOne<ApplicationType>(a => a.ApplicationType)
                .WithMany(ad => ad.Applications)
                .HasForeignKey(x => x.ApplicationTypeId)
                .IsRequired(true);


            modelBuilder.Entity<Feature>()
             .HasOne<Entities.Common.Application>(a => a.Application)
             .WithMany(ad => ad.Features)
             .HasForeignKey(x => x.ApplicationId)
             .IsRequired(true);

            //Department Relations
            modelBuilder.Entity<Department>()
                .HasOne<DepartmentType>(a => a.DepartmentType)
                .WithMany(ad => ad.Departments)
                .HasForeignKey(x => x.DepartmentTypeId)
                .IsRequired(true);

            modelBuilder.Entity<UserDepartments>()
               .HasOne<Department>(a => a.Department)
               .WithMany(ad => ad.UserDepartments)
               .HasForeignKey(x => x.DepartmentId)
               .IsRequired(true);

            modelBuilder.Entity<UserDepartments>()
              .HasOne<AppUser>(a => a.User)
              .WithMany(ad => ad.UserDepartments)
              .HasForeignKey(x => x.UserId)
              .IsRequired(true);

            modelBuilder.Entity<Department>()
                .HasOne<Office>(a => a.Office)
                .WithMany(a => a.Departments)
                .HasForeignKey(x => x.OfficeId)
                .IsRequired(false);

            //Directives Relations
            modelBuilder.Entity<Directive>()
                .HasOne<Letter>(a => a.Letter)
                .WithMany(a => a.Directives)
                .HasForeignKey(x => x.LetterId)
                .IsRequired(true);


            modelBuilder.Entity<Letter>()
               .HasOne<Office>(a => a.Office)
               .WithMany(a => a.Letters)
               .HasForeignKey(a => a.OfficeId)
               .IsRequired(false);

            modelBuilder.Entity<Letter>()
             .HasOne<Priority>(a => a.Priority)
             .WithMany(a => a.Letters)
             .HasForeignKey(a => a.PriorityId)
             .IsRequired(false);

            modelBuilder.Entity<Letter>()
           .HasOne<Constituency>(a => a.Constituency)
           .WithMany(a => a.Letters)
           .HasForeignKey(a => a.ConstituenceyId)
           .IsRequired(false);

            modelBuilder.Entity<Response>()
                .HasOne(a => a.Directive)
                .WithMany(a => a.Response)
                .HasForeignKey(a => a.DirectiveId)
                .IsRequired(false);

            //District Relations
            modelBuilder.Entity<District>()
                .HasOne(a => a.Divison)
                .WithMany(a => a.Districts)
                .HasForeignKey(a => a.DivisionId)
                .IsRequired(true);






        }


        #region EDirective DbSets

        //Users
        public virtual DbSet<AppUser?> AppUsers { get; set; }
        public virtual DbSet<UserType?> UserTypes { get; set; }


        //Roles
        public virtual DbSet<Role?> Roles { get; set; }
        public virtual DbSet<UserRoles?> UserRoles { get; set; }

        //Applications
        public virtual DbSet<Entities.Common.Application?> Applications { get; set; }
        public virtual DbSet<ApplicationType?> ApplicationTypes { get; set; }

        //Features
        public virtual DbSet<Feature?> Features { get; set; }
        public virtual DbSet<UserFeaturePermission?> UserFeaturePermissions { get; set; }

        public virtual DbSet<RoleFeaturePermission?> RoleFeaturePermissions { get; set; }

        //Offices

        public virtual DbSet<Office> Offices { get; set; }

        //Departments
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentType> DepartmentTypes { get; set; }
        public virtual DbSet<UserDepartments> UserDepartments { get; set; }

        //Directives
        public virtual DbSet<Letter> Letters { get; set; }
        public virtual DbSet<Directive> Directives { get; set; }
        public virtual DbSet<DirectiveType> DirectiveTypes { get; set; }

        public virtual DbSet<DirectiveEvent> DirectiveEvents { get; set; }

        public virtual DbSet<Response> Responses { get; set; }

        //Category
        public virtual DbSet<Category> Categories { get; set; }

        //NatureOfSchemes
        public virtual DbSet<NatureOfSchemes> NatureOfSchemes { get; set; } 
        
        //Prority Coloring
        public virtual DbSet<Priority> Priorities { get; set; }

        //Subjects
        public virtual DbSet<Subject> Subjects { get; set; }

        //Constituencies
        public virtual DbSet<Constituency> Constituencies { get; set; }
        public virtual DbSet<ConstituencyType> ConstituencyTypes { get; set; }

        //Districts and Divisions
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }

        //Parties
        public virtual DbSet<Party> Parties { get; set; }

        // Designation
        public virtual DbSet<Designation> Designations { get; set; }

        //Config Table
        public virtual DbSet<DataConfig> DataConfigurations { get; set; }

        //Attachments
        public virtual DbSet<Attachment> Attachments { get; set; }

        public virtual DbSet<AttachmentType> AttachmentTypes { get; set; }

        #endregion

        #region HRMIS DbSets
        public virtual DbSet<AccessColor> AccessColors { get; set; }

        public virtual DbSet<JobType> JobTypes { get; set; }
        public virtual DbSet<MartialStatus> MartialStatuses { get; set; }
        public virtual DbSet<PostNature> PostNatures { get; set; }

        public virtual DbSet<PostsPresently> PostsPresently { get; set; }

        public virtual DbSet<ServiceGroup> ServiceGroups { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<EmployeesColor> EmployeesColors { get; set; }

        public virtual DbSet<Shift> Shifts { get; set; }

        public virtual DbSet<EmployeesJobsInfo> EmployeesJobsInfos { get; set; }

        public virtual DbSet<EmployeesPostingsInfo> EmployeesPostingsInfos { get; set; }

        public virtual DbSet<EmployeesStatus> EmployeesStatuses { get; set; }

        public virtual DbSet<EmployeesSubStatus> EmployeesSubStatuses { get; set; }

        public virtual DbSet<BloodGroup> BloodGroups { get; set; }

        public virtual DbSet<Bpsscale> Bpsscales { get; set; }

        public virtual DbSet<CardType> CardTypes { get; set; }

        public virtual DbSet<Domicile> Domiciles { get; set; }

        public virtual DbSet<Gender> Genders { get; set; }

        public virtual DbSet<Qualification> Qualifications { get; set; }

        public virtual DbSet<Religion> Religions { get; set; }

        public virtual DbSet<AppointmentMode> AppointmentModes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<PoliceStation> PoliceStations { get; set; }

        #endregion
    }
}
