using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SriLankanLifeVS.Models.EntityModels
{
    public class PlaceImageAdmin
    {
        public Guid Id { get; set; }
        public string ImageExtention { get; set; }
        public long FileSize { get; set; }
        public DateTime AddedDate { get; set; }
        public bool Active { get; set; }

        [ForeignKey("Place")]
        public Guid PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
