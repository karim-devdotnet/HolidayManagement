namespace DVSE.DAL.HolidayManagement.Migrations
{
    using DVSE.DAL.HolidayManagement.Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DVSE.DAL.HolidayManagement.EF.HMContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DVSE.DAL.HolidayManagement.EF.HMContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Roles.AddOrUpdate(
                x => x.Name,
                new Role { Name = "AdminUser" },
                new Role { Name = "NormalUser" });

            context.Purposes.AddOrUpdate(x => x.Description,
                new Purpose { Description = "sickness" },
                new Purpose { Description = "death" },
                new Purpose { Description = "personal" },
                new Purpose { Description = "holiday" });
        }
    }
}
