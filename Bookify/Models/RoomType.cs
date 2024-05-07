using System.ComponentModel.DataAnnotations;

namespace Bookify.Models
{
    public class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price per night is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal PricePerNight { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
