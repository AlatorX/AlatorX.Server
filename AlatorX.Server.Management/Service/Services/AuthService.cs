using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AlatorX.Server.Management.Data.IRepositories;
using AlatorX.Server.Management.Domain.Entities;
using AlatorX.Server.Management.Service.DTOs.Users;
using AlatorX.Server.Management.Service.Exceptions;
using AlatorX.Server.Management.Service.Helpers;
using AlatorX.Server.Management.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AlatorX.Server.Management.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IRepository<User> userRepository, IConfiguration configuration)
        {
            this._userRepository = userRepository;
            _configuration = configuration;
        }

        public async ValueTask<LoginForResultDto> AuthenticateAsync(LoginDto dto)
        {
            var user = await this._userRepository.SelectAll()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());

            if (user == null || !PasswordHelper.Verify(dto.Password, user.Password))
                throw new AlatorException(400, "Username or password is incorrect");

            return new LoginForResultDto
            {
                Token = GenerateToken(user)
            };
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Audience = _configuration["JWT:Audience"],
                Issuer = _configuration["JWT:Issuer"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:Expire"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}