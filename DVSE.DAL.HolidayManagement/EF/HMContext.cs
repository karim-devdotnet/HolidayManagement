using DVSE.DAL.HolidayManagement.Entity;
using GenericRepository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVSE.DAL.HolidayManagement.EF
{
    // - note: to update the database uncomment DbContext so that HMContext extends the DbContext class
    // - after you update the database comment the DbContext and let HmContext extend EntitiesContext again
    // - to update the database to the latest migration run the following command in the package manager console:
    //   update-database -startupprojectname:dvse.dal.holidaymanagement
    // - to update the database to a specific migration run the following command:
    //   update-database -startupprojectname:dvse.dal.holidaymanagement -targetmigration:MigrationName
    // - to create a migration make changes to the entities classes and then run the following command:
    //   add-migration MigrationName -startupprojectname:dvse.dal.holidaymanagement
    // - for further info look here: http://msdn.microsoft.com/en-us/data/jj591621.aspx and  
    //   http://coding.abel.nu/2012/03/ef-migrations-command-reference/
    public class HMContext : EntitiesContext // DbContext // EntitiesContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<HolidayPeriod> HolidayPeriods { get; set; }

        public DbSet<HolidayInformation> HolidayInformations { get; set; }

        public DbSet<LegalHoliday> LegalHolidays { get; set; }

        public DbSet<Purpose> Purposes { get; set; }

        public HMContext()
            : base("HolidayManagementDb")
        { 
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder
                .Entity<Request>()
                .HasOptional(x => x.HolidayPeriod)
                .WithMany()
                .HasForeignKey(x => x.HolidayPeriodId);

            modelBuilder
                .Entity<HolidayPeriod>()
                .HasOptional(x => x.Request)
                .WithMany()
                .HasForeignKey(x => x.RequestId);
        }
    }
}
