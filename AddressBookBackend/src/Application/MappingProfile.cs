using AddressBook.src.Application.DTOs;
using AddressBook.src.Infrastructure.Models;
using AutoMapper;

namespace AddressBook.src.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddressEntry, AddressEntryDTO>().ReverseMap();
            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<AddressEntryDTO, AddressEntry>().ReverseMap();
        }
    }
}
