namespace B3Butchery_TouchScreen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppSettintType = c.Int(nullable: false),
                        IntValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BiaoQians",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GridConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BiaoQianId = c.Int(nullable: false),
                        Name = c.String(),
                        GuiGe = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BiaoQians", t => t.BiaoQianId, cascadeDelete: true)
                .Index(t => t.BiaoQianId);
            
            CreateTable(
                "dbo.GridAddedNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GridConfigId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GridConfigs", t => t.GridConfigId, cascadeDelete: true)
                .Index(t => t.GridConfigId);
            
            CreateTable(
                "dbo.InputRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GirdConfigId = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GridConfigs", t => t.GirdConfigId, cascadeDelete: true)
                .Index(t => t.GirdConfigId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GridConfigs", "BiaoQianId", "dbo.BiaoQians");
            DropForeignKey("dbo.InputRecords", "GirdConfigId", "dbo.GridConfigs");
            DropForeignKey("dbo.GridAddedNumbers", "GridConfigId", "dbo.GridConfigs");
            DropIndex("dbo.InputRecords", new[] { "GirdConfigId" });
            DropIndex("dbo.GridAddedNumbers", new[] { "GridConfigId" });
            DropIndex("dbo.GridConfigs", new[] { "BiaoQianId" });
            DropTable("dbo.InputRecords");
            DropTable("dbo.GridAddedNumbers");
            DropTable("dbo.GridConfigs");
            DropTable("dbo.BiaoQians");
            DropTable("dbo.AppSettings");
        }
    }
}
