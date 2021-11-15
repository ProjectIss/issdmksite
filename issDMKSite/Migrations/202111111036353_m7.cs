namespace issDMKSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "departmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Applications", "departmentId");
            //AddForeignKey("dbo.Applications", "departmentId", "dbo.Departments", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "departmentId", "dbo.Departments");
            DropIndex("dbo.Applications", new[] { "departmentId" });
            DropColumn("dbo.Applications", "departmentId");
        }
    }
}
