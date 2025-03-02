using Abstractions.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;
using Common.Resources;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<int> Register(RegisterModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                throw new BadHttpRequestException(ErrorMessages.RegFailed);

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new BadHttpRequestException(ErrorMessages.RegFailed);
            return StatusCodes.Status200OK;
        }
        

    }
}
