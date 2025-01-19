using AddressBook.src.Infrastructure.Repositories.Interfaces;

namespace AddressBook.src.Infrastructure.Repositories
{
    public interface IRepositoryManager
    {
        IAddressEntryRepository AddressEntryRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IJobRepository JobRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
