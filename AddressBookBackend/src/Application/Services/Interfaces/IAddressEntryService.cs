using AddressBook.src.Application.DTOs;
using AddressBook.src.Application.Filters;
using AddressBook.src.Application.Utilities;

namespace AddressBook.src.Application.Services.Interfaces
{
    public interface IAddressEntryService
    {
        Task DeleteEntryAsync(int id);
        Task<AddressEntryDTO> GetByIdAsync(int id);
        Task UpdateEntryAsync(int id, AddressEntryDTO addressEntry);
        Task AddEntryAsync(AddressEntryDTO addressEntry);
        Task<PaginatedList<AddressEntryDTO>> GetFilteredAddressEntriesAsync(AddressEntryQueryParameters queryParameters);
    }
}
