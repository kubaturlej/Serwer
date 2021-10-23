using Football.API.DTOs;
using Football.Domain.Entities;
using Football.Infrastructure.Exception;
using Football.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Football.API.Services
{
    public interface IUserService
    {
        public void RegisterUser(RegisterDto dto);
        public string LoginUser (LoginDto dto);
        public void DeleteUser(int id);
        
    }
    public class UserService : IUserService
    {
        private readonly FootballDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _config;

        public UserService(FootballDbContext dbContext, IPasswordHasher<User> passwordHasher, IConfiguration config)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _config = config; 
        }

        public void DeleteUser(int id)
        {
            var user = _dbContext.Users
             .Find(id);

            if (user is null)
            {
                throw new BadRequestException("Something went wrong ...");
            }

            _dbContext.Remove(user);
            _dbContext.SaveChanges();
        }

        public string LoginUser(LoginDto dto)
        {
            var user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("Invalid user name or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid user name or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterDto dto)
        {
            var newUser = new User
            {
                Email = dto.Email,
                NickName = dto.Nickname,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
    }
}
