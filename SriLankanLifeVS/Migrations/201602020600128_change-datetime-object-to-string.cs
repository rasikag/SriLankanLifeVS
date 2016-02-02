namespace SriLankanLifeVS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedatetimeobjecttostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SliderImages", "AddedDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SliderImages", "AddedDate", c => c.DateTime(nullable: false));
        }
    }
}
