using AddressBook.src.Infrastructure.Repositories.Implementation;
using AddressBook.src.Infrastructure.DbConext;
using AddressBook.src.Infrastructure.Repositories.Interfaces;

namespace AddressBook.src.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AddressBookDbContext context;

        public Lazy<IAddressEntryRepository> _AddressEntry { get; private set; }
        public Lazy<IDepartmentRepository> _Department { get; private set; }
        public Lazy<IJobRepository> _Job { get; private set; }
        public Lazy<IUserRepository> _User { get; private set; }

        public RepositoryManager(AddressBookDbContext context)
        {
            this.context = context;

            _AddressEntry = new Lazy<IAddressEntryRepository>(() => new AddressEntryRepository(this.context));
            _Department = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(this.context));
            _Job = new Lazy<IJobRepository>(() => new JobRepository(this.context));
            _User = new Lazy<IUserRepository>(() => new UserRepository(this.context));
        }

        public IAddressEntryRepository AddressEntryRepository => _AddressEntry.Value;

        public IDepartmentRepository DepartmentRepository => _Department.Value;

        public IJobRepository JobRepository => _Job.Value;
        public IUserRepository UserRepository => _User.Value;


    }
}
