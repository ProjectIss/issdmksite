namespace issDMKSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComplainNo = c.String(),
                        Dateofapplied = c.String(),
                        Detailofcomplain = c.String(),
                        DetailProof = c.String(),
                        Status = c.String(),
                        Feedback = c.String(),
                        ReviewComments = c.String(),
                        AdminName = c.String(),
                        DateandTimeofReact = c.String(),
                        MobilenNo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Blocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        blockName = c.String(),
                        blockSecretaryName = c.String(),
                        blockSecretaryAddress = c.String(),
                        mobileNo = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Panchayats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        panchayatName = c.String(),
                        panchayatSecretaryName = c.String(),
                        panchayatSecretaryAddress = c.String(),
                        mobileNo = c.String(),
                        email = c.String(),
                        blockId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                //.ForeignKey("dbo.Blocks", t => t.blockId, cascadeDelete: true)
                .Index(t => t.blockId);
            
            CreateTable(
                "dbo.signUps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Mobile = c.String(),
                        FatherName = c.String(),
                        Address = c.String(),
                        blockId = c.Int(nullable: false),
                        panchayatId = c.Int(nullable: false),
                        villageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                //.ForeignKey("dbo.Blocks", t => t.blockId, cascadeDelete: true)
                //.ForeignKey("dbo.Panchayats", t => t.panchayatId, cascadeDelete: true)
                //.ForeignKey("dbo.Villages", t => t.villageId, cascadeDelete: true)
                .Index(t => t.blockId)
                .Index(t => t.panchayatId)
                .Index(t => t.villageId);
            
            CreateTable(
                "dbo.Villages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        villageName = c.String(),
                        villageSecretaryName = c.String(),
                        villageSecretaryAddress = c.String(),
                        mobileNo = c.String(),
                        email = c.String(),
                        blockId = c.Int(nullable: false),
                        panchayatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                //.ForeignKey("dbo.Blocks", t => t.blockId, cascadeDelete: true)
                //.ForeignKey("dbo.Panchayats", t => t.panchayatId, cascadeDelete: true)
                .Index(t => t.blockId)
                .Index(t => t.panchayatId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        username = c.String(nullable: false, maxLength: 50),
                        password = c.String(nullable: false, maxLength: 50),
                        role = c.String(nullable: false, maxLength: 50),
                        status = c.String(maxLength: 50),
                        lastLogin = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.signUps", "villageId", "dbo.Villages");
            DropForeignKey("dbo.Villages", "panchayatId", "dbo.Panchayats");
            DropForeignKey("dbo.Villages", "blockId", "dbo.Blocks");
            DropForeignKey("dbo.signUps", "panchayatId", "dbo.Panchayats");
            DropForeignKey("dbo.signUps", "blockId", "dbo.Blocks");
            DropForeignKey("dbo.Panchayats", "blockId", "dbo.Blocks");
            DropIndex("dbo.Villages", new[] { "panchayatId" });
            DropIndex("dbo.Villages", new[] { "blockId" });
            DropIndex("dbo.signUps", new[] { "villageId" });
            DropIndex("dbo.signUps", new[] { "panchayatId" });
            DropIndex("dbo.signUps", new[] { "blockId" });
            DropIndex("dbo.Panchayats", new[] { "blockId" });
            DropTable("dbo.User");
            DropTable("dbo.Villages");
            DropTable("dbo.signUps");
            DropTable("dbo.Panchayats");
            DropTable("dbo.Blocks");
            DropTable("dbo.Applications");
        }
    }
}
