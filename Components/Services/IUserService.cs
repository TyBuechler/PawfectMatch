using Microsoft.AspNetCore.Identity.Data;
using PawfectMatch.Components.Data;
using System.Threading.Tasks;

namespace PawfectMatch.Components.Services
{
    public interface IUserService
    {
        Task<User> LoginAsync(User user);
        Task<User> RegisterUserAsync(User user);
        Task<User> GetUserByAccessTokenAsync(string accessToken);
        Task<User> RefreshTokenAsync(RefreshRequest refreshRequest);
    }
}
