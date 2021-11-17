namespace issDMKSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "villageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Applications", "villageId");
            //AddForeignKey("dbo.Applications", "villageId", "dbo.Villages", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "villageId", "dbo.Villages");
            DropIndex("dbo.Applications", new[] { "villageId" });
            DropColumn("dbo.Applications", "villageId");
        }
    }
}
