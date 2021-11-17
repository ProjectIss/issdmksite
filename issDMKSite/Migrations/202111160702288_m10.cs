namespace issDMKSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyUpdates",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        dailyDetail = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DailyUpdates");
        }
    }
}
