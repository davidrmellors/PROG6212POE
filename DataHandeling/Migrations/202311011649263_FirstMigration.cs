namespace DataHandeling.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ModuleID = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 128),
                        ModuleCode = c.String(),
                        ModuleName = c.String(),
                        NumberOfCredits = c.Int(nullable: false),
                        ClassHoursPerWeek = c.Double(nullable: false),
                        NumberOfWeeks = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ModuleID)
                .ForeignKey("dbo.Users", t => t.Username)
                .Index(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modules", "Username", "dbo.Users");
            DropIndex("dbo.Modules", new[] { "Username" });
            DropTable("dbo.Modules");
        }
    }
}
