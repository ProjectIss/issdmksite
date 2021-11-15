namespace issDMKSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applications", "Dateofapplied", c => c.DateTime());
            AlterColumn("dbo.Applications", "DateandTimeofReact", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applications", "DateandTimeofReact", c => c.String());
            AlterColumn("dbo.Applications", "Dateofapplied", c => c.String());
        }
    }
}
