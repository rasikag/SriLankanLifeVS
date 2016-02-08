namespace SriLankanLifeVS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeinplacevideouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlaceVideoUsers", "UserName", c => c.String());
            AddColumn("dbo.PlaceVideoUsers", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlaceVideoUsers", "Email");
            DropColumn("dbo.PlaceVideoUsers", "UserName");
        }
    }
}
