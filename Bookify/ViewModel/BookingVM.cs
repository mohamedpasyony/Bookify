using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModel
{
    public class BookingVM
    {
        public int BookingId { get; set; }


        [Required(ErrorMessage = "Check-in Date is required")]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Check-out Date is required")]
        public DateTime CheckOutDate { get; set;}
        [Required(ErrorMessage = "price required")]
        public decimal TotalPrice { get; set; }
        [Required(ErrorMessage = "room type required")]
        public int RoomTypeId {  get; set; }

        [Required(ErrorMessage = "guest name requried")]
        public string GuestId {  get; set; }
    }
}
