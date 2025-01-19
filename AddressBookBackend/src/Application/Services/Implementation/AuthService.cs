using AddressBook.src.Application.DTOs;
using AddressBook.src.Application.Exceptions;
using AddressBook.src.Application.Services.Interfaces;
using AddressBook.src.Infrastructure.Models;
using AddressBook.src.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AddressBook.src.Application.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager _repository;
        private readonly IConfiguration _config;

        public AuthService(IRepositoryManager repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }
        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _repository.UserRepository.GetUserByEmailAsync(loginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
            {
                throw new Exception("Invalid email or password");
            }

            return GenerateToken(user);
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO userRegisterDTO)
        {
            var existingUser = await _repository.UserRepository.GetUserByEmailAsync(userRegisterDTO.Email);
            if(existingUser != null)
            {
                throw new EmailAlreadyExistsException("User with this email already exists.");
            }

            var user = new User
            {
                FullName = userRegisterDTO.FullName,
                Email = userRegisterDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password)
            };
            await _repository.UserRepository.CreateUserAsync(user);
            return GenerateToken(user);
        }
        private AuthResponseDTO GenerateToken(User user)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var keyString = jwtSettings["Key"] ?? throw new Exception("JWT key is not configured");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var validTo = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["lifetime"]));
            var validTo = DateTime.Now.AddDays(2);
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: validTo,
                signingCredentials: credentials
            );
            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),

                Expires = token.ValidTo,
            };
        }
    }
}
