﻿using AddressBook.src.Application.DTOs;
using AddressBook.src.Application.Filters;
using AddressBook.src.Application.Services;
using AddressBook.src.Application.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.src.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressEntriesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AddressEntriesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entry = await _service.AddressEntryService.GetByIdAsync(id);
            return Ok(entry);
        }
        [HttpGet]
        public async Task<IActionResult> GetAddressEntries([FromQuery]AddressEntryQueryParameters queryParameters)
        {
            var entries = await _service.AddressEntryService.GetFilteredAddressEntriesAsync(queryParameters);
            return Ok(entries);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] AddressEntryDTO dto)
        {
            if (dto.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Photo.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    dto.PhotoUrl = Convert.ToBase64String(fileBytes);
                }
            }

            await _service.AddressEntryService.AddEntryAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] AddressEntryDTO dto)
        {
            if (dto.Photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await dto.Photo.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    dto.PhotoUrl = Convert.ToBase64String(fileBytes);
                }
            }

            await _service.AddressEntryService.UpdateEntryAsync(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.AddressEntryService.DeleteEntryAsync(id);
            return Ok();
        }
    }
}
