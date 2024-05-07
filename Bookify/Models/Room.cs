using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Models
{
    public class Room
    {
        [Key]
        public int RoomNum { get; set; }
        public string Status { get; set; }
        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual Booking Booking { get; set; }

    }
}

