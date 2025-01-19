using AddressBook.src.Infrastructure.Models;

namespace AddressBook.src.Infrastructure.Repositories.Interfaces
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetJobs();
        Task<Job?> GetByIdAsync(int id);
    }
}
