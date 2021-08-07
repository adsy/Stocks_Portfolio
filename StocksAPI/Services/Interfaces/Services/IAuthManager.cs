using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);

        Task<string> CreateToken();
    }
}