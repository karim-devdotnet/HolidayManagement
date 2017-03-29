namespace DVSE.DAL.HolidayManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        HireDate = c.DateTime(),
                        RoleId = c.Int(nullable: false),
                        TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.TeamId)
                .Index(t => t.RoleId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HolidayInformation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DaysAvailable = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.Employee_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Request",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        RequestNote = c.String(),
                        Accepted = c.Boolean(),
                        RequestStateNote = c.String(),
                        CancelDate = c.DateTime(),
                        EmployeeId = c.Int(nullable: false),
                        PurposeId = c.Int(),
                        HolidayPeriodId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Purpose", t => t.PurposeId)
                .ForeignKey("dbo.HolidayPeriod", t => t.HolidayPeriodId)
                .Index(t => t.EmployeeId)
                .Index(t => t.PurposeId)
                .Index(t => t.HolidayPeriodId);
            
            CreateTable(
                "dbo.Purpose",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HolidayPeriod",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Note = c.String(),
                        CancelDate = c.DateTime(),
                        Cancelled = c.Boolean(),
                        EmployeeId = c.Int(nullable: false),
                        RequestId = c.Int(),
                        PurposeId = c.Int(nullable: false),
                        LegalHolidayId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Request", t => t.RequestId)
                .ForeignKey("dbo.Purpose", t => t.PurposeId, cascadeDelete: true)
                .ForeignKey("dbo.LegalHoliday", t => t.LegalHolidayId)
                .Index(t => t.EmployeeId)
                .Index(t => t.RequestId)
                .Index(t => t.PurposeId)
                .Index(t => t.LegalHolidayId);
            
            CreateTable(
                "dbo.LegalHoliday",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.HolidayPeriod", new[] { "LegalHolidayId" });
            DropIndex("dbo.HolidayPeriod", new[] { "PurposeId" });
            DropIndex("dbo.HolidayPeriod", new[] { "RequestId" });
            DropIndex("dbo.HolidayPeriod", new[] { "EmployeeId" });
            DropIndex("dbo.Request", new[] { "HolidayPeriodId" });
            DropIndex("dbo.Request", new[] { "PurposeId" });
            DropIndex("dbo.Request", new[] { "EmployeeId" });
            DropIndex("dbo.HolidayInformation", new[] { "Employee_Id" });
            DropIndex("dbo.Employee", new[] { "TeamId" });
            DropIndex("dbo.Employee", new[] { "RoleId" });
            DropForeignKey("dbo.HolidayPeriod", "LegalHolidayId", "dbo.LegalHoliday");
            DropForeignKey("dbo.HolidayPeriod", "PurposeId", "dbo.Purpose");
            DropForeignKey("dbo.HolidayPeriod", "RequestId", "dbo.Request");
            DropForeignKey("dbo.HolidayPeriod", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.Request", "HolidayPeriodId", "dbo.HolidayPeriod");
            DropForeignKey("dbo.Request", "PurposeId", "dbo.Purpose");
            DropForeignKey("dbo.Request", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.HolidayInformation", "Employee_Id", "dbo.Employee");
            DropForeignKey("dbo.Employee", "TeamId", "dbo.Team");
            DropForeignKey("dbo.Employee", "RoleId", "dbo.Role");
            DropTable("dbo.LegalHoliday");
            DropTable("dbo.HolidayPeriod");
            DropTable("dbo.Purpose");
            DropTable("dbo.Request");
            DropTable("dbo.Team");
            DropTable("dbo.HolidayInformation");
            DropTable("dbo.Role");
            DropTable("dbo.Employee");
        }
    }
}
