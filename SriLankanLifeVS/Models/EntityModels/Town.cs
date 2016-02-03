using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace SriLankanLifeVS.Models.EntityModels
{
    public class Town
    {
        public int Id { get; set; }
        public string TownName { get; set; }

        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public virtual List<Place> Places { get; set; }

    }
}
