using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SriLankanLifeVS.Models.EntityModels
{
    public class SliderImage
    {

        public int Id { get; set; }
        public string ImageName { get; set; }
        public bool Active { get; set; }
        public DateTime AddedDate { get; set; }

    }
}
