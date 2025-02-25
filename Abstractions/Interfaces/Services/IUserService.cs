using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Services
{
    public interface IUserService
    {
        public Task<int> Register(RegisterModel model);
    }
}
