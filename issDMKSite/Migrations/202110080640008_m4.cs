namespace issDMKSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Applications", "blockId", "dbo.Blocks");
            DropIndex("dbo.Applications", new[] { "blockId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Applications", "blockId");
            AddForeignKey("dbo.Applications", "blockId", "dbo.Blocks", "Id", cascadeDelete: true);
        }
    }
}
