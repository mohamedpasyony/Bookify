using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Models
{

    public class Booking
    {
        [Key]
        public int BookingId { get; set; }


        [Required(ErrorMessage = "Check-in Date is required")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Check-out Date is required")]
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        [ForeignKey("Room")]
        public int RoomNum { get; set; }
        public virtual Room Room { get; set; }

        [ForeignKey("User")]
        public string GuestId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
