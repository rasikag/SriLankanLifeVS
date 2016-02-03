using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SriLankanLifeVS.Models.EntityModels
{
    public class Event
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string QuickFacts { get; set; }
        public string Month { get; set; }

        [ForeignKey("Town")]
        public int TownId { get; set; }
        public virtual Town Town { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; }

        public virtual List<EventImageUser> ImageUsers { get; set; }
        public virtual List<EventImageAdmin> ImageAdmin { get; set; }
        public virtual List<EventCommentUser> CommentUser { get; set; }
        public virtual List<EventVideoUser> VideoUser { get; set; }
        public virtual List<EventVideoAdmin> VideoAdmin { get; set; }

        [ForeignKey("Place")]
        public Guid? PlaceId { get; set; }
        public virtual Place Place { get; set; }

    }
}
