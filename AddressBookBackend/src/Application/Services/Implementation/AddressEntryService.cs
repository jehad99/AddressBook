using AddressBook.src.Application.DTOs;
using AddressBook.src.Application.Filters;
using AddressBook.src.Application.Services.Interfaces;
using AddressBook.src.Application.Utilities;
using AddressBook.src.Infrastructure.Models;
using AddressBook.src.Infrastructure.Repositories;
using AutoMapper;
using System.Security.Claims;

namespace AddressBook.src.Application.Services.Implementation
{
    public class AddressEntryService : IAddressEntryService
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AddressEntryService(IRepositoryManager repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task AddEntryAsync(AddressEntryDTO addressEntryDto)
        {
            var addressEntry = this.mapper.Map<AddressEntry>(addressEntryDto);
            //Just for testing purposes, in real world scenario, we would get the user id from the token
            addressEntry.UserId = GetCurrentUserId();
            await this.repository.AddressEntryRepository.AddEntryAsync(addressEntry);
        }

        public async Task DeleteEntryAsync(int id)
        {
            var addressEntry = await this.repository.AddressEntryRepository.GetByIdAsync(id, false);
            if (addressEntry == null)
            {
                throw new Exception("Address entry not found");
            }
            await this.repository.AddressEntryRepository.DeleteEntryAsync(addressEntry);
        }

        public async Task<PaginatedList<AddressEntryDTO>> GetFilteredAddressEntriesAsync(AddressEntryQueryParameters queryParameters)
        {
            queryParameters.UserId = GetCurrentUserId();
            
            var addressEntries = await this.repository.AddressEntryRepository.GetFilteredEntriesAsync(queryParameters);
            var addressEntryDtos = this.mapper.Map<List<AddressEntryDTO>>(addressEntries.Items);
            return new PaginatedList<AddressEntryDTO>(addressEntryDtos, addressEntries.TotalCount, addressEntries.CurrentPage, addressEntries.PageSize);
        }

        public async Task<AddressEntryDTO> GetByIdAsync(int id)
        {
            var addressEntry = await this.repository.AddressEntryRepository.GetByIdAsync(id, false);
            if (addressEntry == null)
            {
                throw new Exception("Address entry not found");
            }
            return this.mapper.Map<AddressEntryDTO>(addressEntry);
        }

        public async Task UpdateEntryAsync(int id, AddressEntryDTO addressEntryDto)
        {
            var addressEntry = await this.repository.AddressEntryRepository.GetByIdAsync(id, true);
            if (addressEntry == null)
            {
                throw new Exception("Address entry not found");
            }
            this.mapper.Map(addressEntryDto, addressEntry);
            await this.repository.AddressEntryRepository.SaveAsync();
        }

        private int GetCurrentUserId()
        {
            var userId = this.httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User is not authenticated.");

            return int.Parse(userId);
        }
    }
}
