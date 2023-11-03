namespace DataHandeling.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModule : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Modules", "Username", "dbo.Users");
            DropIndex("dbo.Modules", new[] { "Username" });
            AlterColumn("dbo.Modules", "Username", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Modules", "ModuleCode", c => c.String(nullable: false));
            AlterColumn("dbo.Modules", "ModuleName", c => c.String(nullable: false));
            CreateIndex("dbo.Modules", "Username");
            AddForeignKey("dbo.Modules", "Username", "dbo.Users", "Username", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modules", "Username", "dbo.Users");
            DropIndex("dbo.Modules", new[] { "Username" });
            AlterColumn("dbo.Modules", "ModuleName", c => c.String());
            AlterColumn("dbo.Modules", "ModuleCode", c => c.String());
            AlterColumn("dbo.Modules", "Username", c => c.String(maxLength: 128));
            CreateIndex("dbo.Modules", "Username");
            AddForeignKey("dbo.Modules", "Username", "dbo.Users", "Username");
        }
    }
}
