using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Models;
using ToDoApp.Infrastructure.Entities;

namespace ToDoApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(UserManager<IdentityUserEntity> userManager, IConfiguration configuration, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterModel model)
        {
            // Map RegisterModel to IdentityUserEntity
            var identityUser = new IdentityUserEntity
            {
                UserName = model.Username,
                Email = model.Email
            };

            // Use UserManager to create the user, which should handle ID generation
            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (!result.Succeeded)
            {
                return new AuthResponse
                {
                    IsAuthenticated = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return await GenerateJwtToken(identityUser);
        }


        public async Task<AuthResponse> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new AuthResponse { IsAuthenticated = false, Errors = new[] { "Invalid credentials" } };
            }

            return await GenerateJwtToken(user);
        }

        private async Task<AuthResponse> GenerateJwtToken(IdentityUserEntity user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(5),
                SigningCredentials = credentials,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenCreated = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponse
            {
                IsAuthenticated = true,
                Token = tokenHandler.WriteToken(tokenCreated),
                Expiration = DateTime.Now.AddDays(10)
            };

        }
    }
}
