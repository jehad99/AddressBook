﻿namespace AddressBook.src.Application.DTOs
{
    public class RegisterDTO
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
