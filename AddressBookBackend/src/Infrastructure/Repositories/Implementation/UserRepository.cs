using AddressBook.src.Infrastructure.DbConext;
using AddressBook.src.Infrastructure.Models;
using AddressBook.src.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.src.Infrastructure.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AddressBookDbContext _context;

        public UserRepository(AddressBookDbContext context)
        {
            _context = context;
        }
        public async Task CreateUserAsync(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<User?> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
