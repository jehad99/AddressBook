namespace AddressBook.src.Application.DTOs
{
    public class AuthResponseDTO
    {
        public required string Token { get; set; }
        public DateTime Expires { get; set; }

    }
}
