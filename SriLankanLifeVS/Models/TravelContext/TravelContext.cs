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

        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceCategory> PlaceCategories { get; set; }

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
