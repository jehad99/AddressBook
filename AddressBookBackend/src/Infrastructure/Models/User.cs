using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.src.Infrastructure.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public required string FullName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        public required string Password { get; set; }
    }
}
