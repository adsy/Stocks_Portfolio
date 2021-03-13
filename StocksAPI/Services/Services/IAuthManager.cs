using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}
