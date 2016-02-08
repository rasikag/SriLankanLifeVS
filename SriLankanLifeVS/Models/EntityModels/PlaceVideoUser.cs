using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SriLankanLifeVS.Models.EntityModels
{
    public class PlaceVideoUser
    {
        public Guid Id { get; set; }
        public string VideoURL { get; set; }
        public bool Active { get; set; }
        public DateTime AddedDate { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        [ForeignKey("Place")]
        public Guid PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
