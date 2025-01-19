using AddressBook.src.Application.Services.Implementation;
using AddressBook.src.Application.Services.Interfaces;
using AddressBook.src.Infrastructure.Repositories;
using AutoMapper;

namespace AddressBook.src.Application.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        private Lazy<IJobService> _jobService;
        private Lazy<IDepartmentService> _departmentService;
        private Lazy<IAddressEntryService> _addressEntryService;
        private Lazy<IAuthService> _authService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration config)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _config = config;

            _jobService = new Lazy<IJobService>(() => new JobService(_repositoryManager, _mapper));
            _departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(_repositoryManager, _mapper));
            _addressEntryService = new Lazy<IAddressEntryService>(() => new AddressEntryService(_repositoryManager, _mapper, _httpContextAccessor));
            _authService = new Lazy<IAuthService>(() => new AuthService(_repositoryManager, _config));

        }

        public IJobService JobService => _jobService.Value;

        public IDepartmentService DepartmentService => _departmentService.Value;
        public IAuthService AuthService => _authService.Value;
        public IAddressEntryService AddressEntryService => _addressEntryService.Value;
    }
}
