using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SriLankanLifeVS.Models.EntityModels
{
    public class Place
    {
        public Guid Id { get; set; }
        public string PlaceName { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string QuickFacts { get; set; }

        [ForeignKey("Town")]
        public int TownId { get; set; }
        public virtual Town Town { get; set; }
        
        public ICollection<PlaceCategory> PlaceCategories { get; set; }

        public virtual List<PlaceImageUser> ImageUsers { get; set; }
        public virtual List<PlaceImageAdmin> ImageAdmin { get; set; }
        public virtual List<PlaceCommentUser> CommentUser { get; set; }
        public virtual List<PlaceVideoUser> VideoUser { get; set; }
        public virtual List<PlaceVideoAdmin> VideoAdmin { get; set; }

        public virtual List<Event> Events { get; set; }
        
    }
}
