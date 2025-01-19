using AddressBook.src.Application.Services.Interfaces;

namespace AddressBook.src.Application.Services
{
    public interface IServiceManager
    {
        IAddressEntryService AddressEntryService { get; }
        IJobService JobService { get; }
        IDepartmentService DepartmentService { get; }
        IAuthService AuthService { get; }
    }
}
