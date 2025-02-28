using Abstractions.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.Resources;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        private static Dictionary<string, string> _refreshTokens = new Dictionary<string, string>();

        public async Task<RefreshAndAccess> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new UnauthorizedAccessException(string.Format(ErrorMessages.Unauthorized));
            }

            var accessToken = new JwtSecurityTokenHandler().WriteToken(GenerateAccessToken(user.UserName));
            var refreshToken = Guid.NewGuid().ToString();
            var refreshAndAccess = new RefreshAndAccess();
            
            _refreshTokens[refreshToken] = user.UserName;

            refreshAndAccess.RefreshToken = refreshToken;
            refreshAndAccess.AccessToken = accessToken;


            return refreshAndAccess;
        }


        public async Task<string> RefreshToken(string refreshToken)
        {
            if (_refreshTokens.TryGetValue(refreshToken, out var userName))
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return null;
                }

                var newAccessToken = new JwtSecurityTokenHandler().WriteToken(GenerateAccessToken(user.UserName));

                return newAccessToken;
            }

            return null; 
        }


        public JwtSecurityToken GenerateAccessToken(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, "SuperAdmin")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
