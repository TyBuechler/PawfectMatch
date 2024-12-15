using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;


namespace PawfectMatch.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly PawfectMatchContext _dbContext;

        public AuthenticationService(AuthenticationStateProvider authenticationStateProvider, PawfectMatchContext dbContext)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _dbContext = dbContext;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            // Check the user credentials against the database
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.username == username);

            if (user == null || user.password != password)
                return false;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, "User"), // Adjust roles as needed
            };

            var identity = new ClaimsIdentity(claims, "CustomAuth");
            var principal = new ClaimsPrincipal(identity);

            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(principal);

            return true;
        }

        public void Logout()
        {
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }
    }
}