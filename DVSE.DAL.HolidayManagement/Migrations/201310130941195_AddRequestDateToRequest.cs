namespace DVSE.DAL.HolidayManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequestDateToRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "RequestDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "RequestDate");
        }
    }
}
