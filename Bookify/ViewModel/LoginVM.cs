using System.ComponentModel.DataAnnotations;

namespace Bookify.ViewModel
{
    public class LoginVM
    {
        public int Id { get; set; }
    
        [Required]
        public string username { get; set;}
        [Required]
        public string Password { get; set; }
        public bool IsPresistent { get; set; }
    }
}
