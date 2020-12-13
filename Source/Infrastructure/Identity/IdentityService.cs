using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MakeMeRich.Application.Common.Interfaces;
using MakeMeRich.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MakeMeRich.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthenticationResult> LoginUserAsync(string email, string password)
        {
            var user = await _userManager
                .FindByEmailAsync(email)
                .ConfigureAwait(false);

            if (user is null)
            {
                return new AuthenticationResult
                {
                    Errors = new string[] { "User does not exist." }
                };
            }

            bool userHasValidPassword = await _userManager
                .CheckPasswordAsync(user, password)
                .ConfigureAwait(false);

            if (userHasValidPassword is false)
            {
                return new AuthenticationResult
                {
                    Errors = new string[] { "Username or password is wrong." }
                };
            }

            return GenerateAuthenticationResult(user);
        }

        public async Task<AuthenticationResult> CreateUserAsync(string email, string password)
        {
            var existingUser = await _userManager
                .FindByEmailAsync(email)
                .ConfigureAwait(false);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new string[] { "User with specified email already exists." }
                };
            }

            var newUser = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager
                .CreateAsync(newUser, password)
                .ConfigureAwait(false);

            if (result.Succeeded is false)
            {
                return new AuthenticationResult
                {
                    Errors = result.Errors.Select(error => error.Description),
                };
            }

            return GenerateAuthenticationResult(newUser);
        }

        private AuthenticationResult GenerateAuthenticationResult(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Succeeded = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
