using SriLankanLifeVS.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SriLankanLifeVS.Models.TravelContext
{
    public class TravelContext : DbContext
    {
        public TravelContext() : base("TravelDBConnection")
        {

        }
        
        public DbSet<District> Districts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<EventCommentUser> EventCommentsUser { get; set; }
        public DbSet<EventImageAdmin> EventImagesAdmin { get; set; }
        public DbSet<EventImageUser> EventImagesUser { get; set; }
        public DbSet<EventVideoAdmin> EventVideosAdmin { get; set; }
        public DbSet<EventVideoUser> EventVideosUser { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceCategory> PlaceCategories { get; set; }
        public DbSet<PlaceCommentUser> PlaceCommentUser { get; set; }
        public DbSet<PlaceImageAdmin> PlaceImagesAdmin { get; set; }
        public DbSet<PlaceImageUser> PlaceImagesUser { get; set; }
        public DbSet<PlaceVideoAdmin> PlaceVideosAdmin { get; set; }
        public DbSet<PlaceVideoUser> PlaceVideosUser { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<Town> Towns { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlaceCategory>()
            .HasMany(a => a.Places)
            .WithMany(b=>b.PlaceCategories)
            .Map(m =>
            {
                m.ToTable("PlaceJoinPalceCategory");
                m.MapLeftKey("PlaceCategoryId");
                m.MapRightKey("PlaceId");
            });


            modelBuilder.Entity<Event>()
            .HasMany(a => a.EventCategories)
            .WithMany(b => b.Events)
            .Map(m =>
            {
                m.ToTable("EventJoinEventCategory");
                m.MapLeftKey("EventId");
                m.MapRightKey("EventCategoryId");
            });

            //base.OnModelCreating(modelBuilder);
        }


    }
}
