using AddressBook.src.Infrastructure.Models;

namespace AddressBook.src.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
    }
}
