using AddressBook.src.Application.Filters;
using AddressBook.src.Application.Utilities;
using AddressBook.src.Infrastructure.DbConext;
using AddressBook.src.Infrastructure.Models;
using AddressBook.src.Infrastructure.Repositories.Interfaces;
using AddressBook.src.Presentation.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.src.Infrastructure.Repositories.Implementation
{
    public class AddressEntryRepository : IAddressEntryRepository
    {
        private readonly AddressBookDbContext _context;

        public AddressEntryRepository(AddressBookDbContext context)
        {
            _context = context;
        }

        public async Task AddEntryAsync(AddressEntry addressEntry)
        {
            await _context.AddressEntries.AddAsync(addressEntry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntryAsync(AddressEntry addressEntry)
        {
             _context.AddressEntries.Remove(addressEntry);
            await _context.SaveChangesAsync();
        }

        public async Task<AddressEntry?> GetByIdAsync(int id, bool trackChanges)
        {
            return trackChanges
                ? await _context.AddressEntries.FirstOrDefaultAsync(e => e.Id == id)
                : await _context.AddressEntries.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }
        public IQueryable<AddressEntry> GetAllByUserId(int userId, bool trackChanges)
        {
            return trackChanges
            ? _context.AddressEntries.Where(e => e.UserId == userId)
            : _context.AddressEntries.Where(e => e.UserId == userId).AsNoTracking();
        }
        public async Task<PaginatedList<AddressEntry>> GetFilteredEntriesAsync(AddressEntryQueryParameters queryParams)
        {
            var query = GetAllByUserId(queryParams.UserId,false)
                .ApplyFilters(queryParams)
                .OrderByDescending(e => e.Id);

            var totalRecords = await query.CountAsync();
            var paginatedData = await query
                .Skip((queryParams.Page - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();
            return new PaginatedList<AddressEntry>(paginatedData, totalRecords, queryParams.Page, queryParams.PageSize);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}



