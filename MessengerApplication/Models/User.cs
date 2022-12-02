using System.ComponentModel.DataAnnotations;

namespace MessengerApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*field is required")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "*the name length must be between 3 and 30 characters")]
        public string Name { get; set; }
        public bool HasNewMessages { get; set; }
    }
}