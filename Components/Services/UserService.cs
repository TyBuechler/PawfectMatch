using Microsoft.AspNetCore.Identity.Data;
using PawfectMatch.Components.Data;
using System.Threading.Tasks;

namespace PawfectMatch.Components.Services
{
    public class UserService : IUserService
    {
        public Task<User> LoginAsync(User user)
        {
            // Implement login logic here
            throw new NotImplementedException();
        }

        public Task<User> RegisterUserAsync(User user)
        {
            // Implement register logic here
            throw new NotImplementedException();
        }

        public Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            // Implement logic to get user by access token
            throw new NotImplementedException();
        }

        public Task<User> RefreshTokenAsync(RefreshRequest refreshRequest)
        {
            // Implement logic to refresh token
            throw new NotImplementedException();
        }
    }
}
