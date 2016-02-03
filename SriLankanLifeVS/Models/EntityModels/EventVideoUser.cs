using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace SriLankanLifeVS.Models.EntityModels
{
    public class EventVideoUser
    {
        public Guid Id { get; set; }
        public string VideoURL { get; set; }
        public bool Active { get; set; }
        public DateTime AddedDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        [ForeignKey("Event")]
        public Guid EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
