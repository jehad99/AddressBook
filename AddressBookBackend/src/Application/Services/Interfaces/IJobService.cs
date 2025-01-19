using AddressBook.src.Application.DTOs;

namespace AddressBook.src.Application.Services.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<JobDTO>> GetAllAsync();
        Task<JobDTO> GetByIdAsync(int id);
    }
}
