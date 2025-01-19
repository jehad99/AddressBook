using AddressBook.src.Application.DTOs;
using AddressBook.src.Application.Services.Interfaces;
using AddressBook.src.Infrastructure.Repositories;
using AutoMapper;

namespace AddressBook.src.Application.Services.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public DepartmentService(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            var jobs = await this.repository.DepartmentRepository.GetAllAsync();
            return this.mapper.Map<List<DepartmentDTO>>(jobs);
        }
        public async Task<DepartmentDTO> GetByIdAsync(int id)
        {
            var job = await this.repository.DepartmentRepository.GetByIdAsync(id);
            return this.mapper.Map<DepartmentDTO>(job);
        }
    }
}
