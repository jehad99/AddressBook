using AddressBook.src.Application.DTOs;

namespace AddressBook.src.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO userRegisterDTO);
        Task<AuthResponseDTO> LoginAsync(LoginDTO userRegisterDTO);
    }
}
