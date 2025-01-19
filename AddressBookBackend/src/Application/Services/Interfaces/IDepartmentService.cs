using AddressBook.src.Application.DTOs;

namespace AddressBook.src.Application.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentDTO> GetByIdAsync(int id);
        Task<IEnumerable<DepartmentDTO>> GetAllAsync();
    }
}
