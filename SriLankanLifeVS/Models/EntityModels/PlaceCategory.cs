using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SriLankanLifeVS.Models.EntityModels
{
    public class PlaceCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Place> Places { get; set; }
    }
}
