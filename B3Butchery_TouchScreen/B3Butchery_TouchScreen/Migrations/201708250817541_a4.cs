namespace B3Butchery_TouchScreen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GridConfigs", "ProductDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GridConfigs", "ProductDate");
        }
    }
}
