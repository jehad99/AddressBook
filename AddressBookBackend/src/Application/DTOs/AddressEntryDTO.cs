namespace AddressBook.src.Application.DTOs
{
    public class AddressEntryDTO
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required int JobId { get; set; }
        public int DepartmentId { get; set; }
        public required string MobileNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public required string Address { get; set; }
        public required string Email { get; set; }
        public IFormFile? Photo { get; set; } 
        public string? PhotoUrl { get; set; }
    }
}
