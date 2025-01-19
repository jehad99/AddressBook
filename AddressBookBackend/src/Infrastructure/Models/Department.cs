using System.ComponentModel.DataAnnotations;

namespace AddressBook.src.Infrastructure.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
    }
}
