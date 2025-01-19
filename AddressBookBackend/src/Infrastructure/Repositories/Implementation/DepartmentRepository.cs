using AddressBook.src.Infrastructure.DbConext;
using AddressBook.src.Infrastructure.Models;
using AddressBook.src.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.src.Infrastructure.Repositories.Implementation
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AddressBookDbContext _context;

        public DepartmentRepository(AddressBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.AsNoTracking().ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
