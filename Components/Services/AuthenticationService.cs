using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components.Pages.Models;
using System.Security.Claims;

namespace PawfectMatch.Services
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> PeoProducts { get; set; }
    }
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;

        public AuthenticationService(AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
        }

        // Login method to authenticate and set authentication state
        public async Task Login(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, "User") // Set the role or any other claim as needed
            };

            var identity = new ClaimsIdentity(claims, "Custom");
            var principal = new ClaimsPrincipal(identity);

            // Save authentication information in local storage or cookies
            await _localStorageService.SetItemAsync("authToken", "SomeToken"); // Token or session data
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(principal);
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("authToken");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }
    }
}
