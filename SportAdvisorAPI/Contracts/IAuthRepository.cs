using Microsoft.AspNetCore.Identity;
using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(RegisterUserDto user);
        Task<AuthResponseDTO?> Login(LoginUserDTO user);
        Task<string> GenerateToken(User user);
        Task<bool> IsEmailConfirmed(LoginUserDTO loginUserDTO);
    }
}