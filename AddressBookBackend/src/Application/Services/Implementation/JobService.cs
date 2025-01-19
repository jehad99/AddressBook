using AddressBook.src.Application.DTOs;
using AddressBook.src.Application.Services.Interfaces;
using AddressBook.src.Infrastructure.Repositories;
using AutoMapper;

namespace AddressBook.src.Application.Services.Implementation
{
    public class JobService : IJobService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public JobService(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<JobDTO>> GetAllAsync()
        {
            var jobs = await this.repository.JobRepository.GetJobs();
            return this.mapper.Map<List<JobDTO>>(jobs);
        }
        public async Task<JobDTO> GetByIdAsync(int id)
        {
            var job = await this.repository.JobRepository.GetByIdAsync(id);
            return this.mapper.Map<JobDTO>(job);
        }
    }
}
