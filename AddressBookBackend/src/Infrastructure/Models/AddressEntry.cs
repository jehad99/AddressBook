using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBook.src.Infrastructure.Models
{
    public class AddressEntry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public required string FullName { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }
        public required Job Job { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public required Department Department { get; set; }

        [Required]
        [Phone]
        public required string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public required DateOnly DateOfBirth { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Address { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public string? PhotoUrl { get; set; }

        [NotMapped]
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        [ForeignKey("User")]
        public int UserId { get; set; } 

        public required User User { get; set; }
    }
}
