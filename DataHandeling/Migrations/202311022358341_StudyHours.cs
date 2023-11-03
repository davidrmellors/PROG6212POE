namespace DataHandeling.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudyHours : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudyHours",
                c => new
                {
                    StudyHoursID = c.Int(nullable: false, identity: true),
                    WeekNumber = c.Int(nullable: false),
                    RemainingHours = c.Double(nullable: false),
                    ModuleID = c.Int(nullable: false)
                })
                .PrimaryKey(t => t.StudyHoursID)
                .ForeignKey("dbo.Modules", t => t.ModuleID)
                .Index(t => t.ModuleID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.StudyHours", "ModuleID", "dbo.Modules");
            DropIndex("dbo.StudyHours", new[] { "ModuleID" });
            DropTable("dbo.StudyHours");
        }
    }
}
