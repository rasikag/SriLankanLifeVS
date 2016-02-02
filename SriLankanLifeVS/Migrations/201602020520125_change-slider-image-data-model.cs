namespace SriLankanLifeVS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesliderimagedatamodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SliderImages", "Size", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SliderImages", "Size");
        }
    }
}
