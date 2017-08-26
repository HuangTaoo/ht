namespace B3Butchery_TouchScreen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GridConfigs", "Goods_ID", c => c.Long());
            AddColumn("dbo.GridConfigs", "Goods_Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GridConfigs", "Goods_Name");
            DropColumn("dbo.GridConfigs", "Goods_ID");
        }
    }
}
