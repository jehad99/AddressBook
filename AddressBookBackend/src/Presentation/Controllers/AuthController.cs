using AddressBook.src.Application.DTOs;
using AddressBook.src.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.src.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthController(IServiceManager service)
        {
            _service = service;
        }
        [HttpPost("login")] //Login is HttpPost because it's has sensitive data and should not be included in the URL as query parameters.
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var user = await _service.AuthService.LoginAsync(dto);
            return Ok(user);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var user = await _service.AuthService.RegisterAsync(dto);
            return Ok(user);
        }
    }
}
