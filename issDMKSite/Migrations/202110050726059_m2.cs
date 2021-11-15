namespace issDMKSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "blockId", c => c.Int(nullable: false));
            CreateIndex("dbo.User", "blockId");
            //AddForeignKey("dbo.User", "blockId", "dbo.Blocks", "Id", cascadeDelete: true);
            DropColumn("dbo.User", "lastLogin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "lastLogin", c => c.DateTime());
            DropForeignKey("dbo.User", "blockId", "dbo.Blocks");
            DropIndex("dbo.User", new[] { "blockId" });
            DropColumn("dbo.User", "blockId");
        }
    }
}
