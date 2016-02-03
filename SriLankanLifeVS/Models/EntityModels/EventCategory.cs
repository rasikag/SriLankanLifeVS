using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SriLankanLifeVS.Models.EntityModels
{
    public class EventCategory
    {
        public int Id { get; set; }
        public string CategotyName { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
