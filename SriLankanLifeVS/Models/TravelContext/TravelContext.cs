using SriLankanLifeVS.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SriLankanLifeVS.Models.TravelContext
{
    public class TravelContext :DbContext
    {
        public TravelContext() : base("TravelDBConnection")
        {

        }

        public DbSet<SliderImage> SliderImages { get; set; }


    }
}
