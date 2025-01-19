using AddressBook.src.Infrastructure.DbConext;
using AddressBook.src.Infrastructure.Models;
using AddressBook.src.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.src.Infrastructure.Repositories.Implementation
{
    public class JobRepository : IJobRepository
    {
        private readonly AddressBookDbContext _context;

        public JobRepository(AddressBookDbContext context)
        {
            _context = context;
        }

        public async Task<Job?> GetByIdAsync(int id)
        {
            return await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            return await _context.Jobs.AsNoTracking().ToListAsync();
        }
    }
}
