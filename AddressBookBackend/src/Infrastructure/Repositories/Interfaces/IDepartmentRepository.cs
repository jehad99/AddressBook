using AddressBook.src.Infrastructure.Models;

namespace AddressBook.src.Infrastructure.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
    }
}
