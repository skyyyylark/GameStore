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

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IStringLocalizer _localizer;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IStringLocalizerFactory factory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _localizer = factory.Create("Common.Resources.SharedResource", "Common");
        }

        public async Task<int> Register(RegisterModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                throw new BadHttpRequestException(_localizer["RegistrationFailure"]);

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new BadHttpRequestException(_localizer["RegistrationFailure"]);
            return StatusCodes.Status200OK;
        }
        

    }
}
