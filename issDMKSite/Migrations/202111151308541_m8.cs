namespace issDMKSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m8 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Applications", "blockId");
            AddForeignKey("dbo.Applications", "blockId", "dbo.Blocks", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "blockId", "dbo.Blocks");
            DropIndex("dbo.Applications", new[] { "blockId" });
        }
    }
}
