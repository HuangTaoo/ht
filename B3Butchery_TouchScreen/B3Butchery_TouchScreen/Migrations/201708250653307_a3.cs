namespace B3Butchery_TouchScreen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GridConfigs", "IsCommited", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GridConfigs", "IsCommited");
        }
    }
}
