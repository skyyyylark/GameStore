using Models.DTOs;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<RefreshAndAccess> Login(LoginModel model);
        public Task<string> RefreshToken(string refreshToken);
    }
}
