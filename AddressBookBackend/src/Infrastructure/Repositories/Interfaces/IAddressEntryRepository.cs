
using AddressBook.src.Application.Filters;
using AddressBook.src.Application.Utilities;
using AddressBook.src.Infrastructure.Models;

namespace AddressBook.src.Infrastructure.Repositories.Interfaces
{
    public interface IAddressEntryRepository
    {
        Task SaveAsync();
        Task DeleteEntryAsync(AddressEntry addressEntry);
        Task<AddressEntry?> GetByIdAsync(int id, bool trackChanges);
        Task AddEntryAsync(AddressEntry addressEntry);
        Task<PaginatedList<AddressEntry>> GetFilteredEntriesAsync(AddressEntryQueryParameters queryParams );
    }
}
