namespace SriLankanLifeVS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistrictName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategotyName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EventName = c.String(),
                        Longitude = c.Long(nullable: false),
                        Latitude = c.Long(nullable: false),
                        Address = c.String(),
                        Description = c.String(),
                        QuickFacts = c.String(),
                        Month = c.String(),
                        TownId = c.Int(nullable: false),
                        PlaceId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .ForeignKey("dbo.Towns", t => t.TownId, cascadeDelete: true)
                .Index(t => t.TownId)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.EventCommentUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(),
                        Email = c.String(),
                        Comment = c.String(),
                        Active = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        EventId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.EventImageAdmins",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImageExtention = c.String(),
                        FileSize = c.Long(nullable: false),
                        Addeddate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        EventId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.EventImageUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImageExtention = c.String(),
                        FileSize = c.Long(nullable: false),
                        Addeddate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        UserName = c.String(),
                        Email = c.String(),
                        EventId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlaceName = c.String(),
                        Longitude = c.Long(nullable: false),
                        Latitude = c.Long(nullable: false),
                        Address = c.String(),
                        Description = c.String(),
                        QuickFacts = c.String(),
                        TownId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Towns", t => t.TownId, cascadeDelete: true)
                .Index(t => t.TownId);
            
            CreateTable(
                "dbo.PlaceCommentUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(),
                        Email = c.String(),
                        Comment = c.String(),
                        Active = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        PlaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.PlaceImageAdmins",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImageExtention = c.String(),
                        FileSize = c.Long(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        PlaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.PlaceImageUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImageExtention = c.String(),
                        FileSize = c.Long(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Email = c.String(),
                        UserName = c.String(),
                        PlaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.PlaceCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Towns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TownName = c.String(),
                        DistrictId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.PlaceVideoAdmins",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VideoURL = c.String(),
                        Active = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        PlaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.PlaceVideoUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VideoURL = c.String(),
                        Active = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        PlaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.EventVideoAdmins",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VideoURL = c.String(),
                        Active = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        EventId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.EventVideoUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        VideoURL = c.String(),
                        Active = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                        UserName = c.String(),
                        Email = c.String(),
                        EventId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.EventJoinEventCategory",
                c => new
                    {
                        EventId = c.Guid(nullable: false),
                        EventCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventId, t.EventCategoryId })
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.EventCategories", t => t.EventCategoryId, cascadeDelete: true)
                .Index(t => t.EventId)
                .Index(t => t.EventCategoryId);
            
            CreateTable(
                "dbo.PlaceJoinPalceCategory",
                c => new
                    {
                        PlaceCategoryId = c.Int(nullable: false),
                        PlaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlaceCategoryId, t.PlaceId })
                .ForeignKey("dbo.PlaceCategories", t => t.PlaceCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceCategoryId)
                .Index(t => t.PlaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventVideoUsers", "EventId", "dbo.Events");
            DropForeignKey("dbo.EventVideoAdmins", "EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "TownId", "dbo.Towns");
            DropForeignKey("dbo.Events", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.PlaceVideoUsers", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.PlaceVideoAdmins", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "TownId", "dbo.Towns");
            DropForeignKey("dbo.Towns", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.PlaceJoinPalceCategory", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.PlaceJoinPalceCategory", "PlaceCategoryId", "dbo.PlaceCategories");
            DropForeignKey("dbo.PlaceImageUsers", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.PlaceImageAdmins", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.PlaceCommentUsers", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.EventImageUsers", "EventId", "dbo.Events");
            DropForeignKey("dbo.EventImageAdmins", "EventId", "dbo.Events");
            DropForeignKey("dbo.EventJoinEventCategory", "EventCategoryId", "dbo.EventCategories");
            DropForeignKey("dbo.EventJoinEventCategory", "EventId", "dbo.Events");
            DropForeignKey("dbo.EventCommentUsers", "EventId", "dbo.Events");
            DropIndex("dbo.PlaceJoinPalceCategory", new[] { "PlaceId" });
            DropIndex("dbo.PlaceJoinPalceCategory", new[] { "PlaceCategoryId" });
            DropIndex("dbo.EventJoinEventCategory", new[] { "EventCategoryId" });
            DropIndex("dbo.EventJoinEventCategory", new[] { "EventId" });
            DropIndex("dbo.EventVideoUsers", new[] { "EventId" });
            DropIndex("dbo.EventVideoAdmins", new[] { "EventId" });
            DropIndex("dbo.PlaceVideoUsers", new[] { "PlaceId" });
            DropIndex("dbo.PlaceVideoAdmins", new[] { "PlaceId" });
            DropIndex("dbo.Towns", new[] { "DistrictId" });
            DropIndex("dbo.PlaceImageUsers", new[] { "PlaceId" });
            DropIndex("dbo.PlaceImageAdmins", new[] { "PlaceId" });
            DropIndex("dbo.PlaceCommentUsers", new[] { "PlaceId" });
            DropIndex("dbo.Places", new[] { "TownId" });
            DropIndex("dbo.EventImageUsers", new[] { "EventId" });
            DropIndex("dbo.EventImageAdmins", new[] { "EventId" });
            DropIndex("dbo.EventCommentUsers", new[] { "EventId" });
            DropIndex("dbo.Events", new[] { "PlaceId" });
            DropIndex("dbo.Events", new[] { "TownId" });
            DropTable("dbo.PlaceJoinPalceCategory");
            DropTable("dbo.EventJoinEventCategory");
            DropTable("dbo.EventVideoUsers");
            DropTable("dbo.EventVideoAdmins");
            DropTable("dbo.PlaceVideoUsers");
            DropTable("dbo.PlaceVideoAdmins");
            DropTable("dbo.Towns");
            DropTable("dbo.PlaceCategories");
            DropTable("dbo.PlaceImageUsers");
            DropTable("dbo.PlaceImageAdmins");
            DropTable("dbo.PlaceCommentUsers");
            DropTable("dbo.Places");
            DropTable("dbo.EventImageUsers");
            DropTable("dbo.EventImageAdmins");
            DropTable("dbo.EventCommentUsers");
            DropTable("dbo.Events");
            DropTable("dbo.EventCategories");
            DropTable("dbo.Districts");
        }
    }
}
