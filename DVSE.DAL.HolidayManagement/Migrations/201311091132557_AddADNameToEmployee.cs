namespace DVSE.DAL.HolidayManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddADNameToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "ADName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "ADName");
        }
    }
}
