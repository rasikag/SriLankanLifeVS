namespace SriLankanLifeVS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SriLankanLifeVS.Models.EntityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<SriLankanLifeVS.Models.TravelContext.TravelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SriLankanLifeVS.Models.TravelContext.TravelContext context)
        {

            context.Districts.AddOrUpdate(
              p => p.Id,
              new District { Id = 1, DistrictName = "Rathnapura" },
              new District { Id = 2, DistrictName = "Colombo" },
              new District { Id = 3, DistrictName = "Gampaha" },
              new District { Id = 4, DistrictName = "kurunagala" },
              new District { Id = 5, DistrictName = "Puththalama" },
              new District { Id = 6, DistrictName = "Kandy" }
            );

            context.Towns.AddOrUpdate(p => p.Id,
                new Town { Id = 1, TownName = "Nivithigala", DistrictId = 1 },
                new Town { Id = 2, TownName = "Rathnapura", DistrictId = 1 },
                new Town { Id = 2, TownName = "Kalawana", DistrictId = 1 },
                new Town { Id = 4, TownName = "Boralla", DistrictId = 2 },
                new Town { Id = 5, TownName = "Maradana", DistrictId = 2 },
                new Town { Id = 6, TownName = "Kiribathgoda", DistrictId = 2 },
                new Town { Id = 7, TownName = "Kandy", DistrictId = 6 },
                new Town { Id = 8, TownName = "Peradeniya", DistrictId = 6 });

            context.Places.AddOrUpdate(p=>p.Id,
                new Place { Id= Guid.NewGuid() , PlaceName = "TestPlace001" , TownId = 1});

            context.Events.AddOrUpdate(p=>p.Id,
                new Event { Id= Guid.NewGuid(),EventName = "TestEvent001",TownId = 1});

        }
    }
}
