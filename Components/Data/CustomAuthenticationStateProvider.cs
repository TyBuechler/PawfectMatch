using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PawfectMatch.Components.Services;
using System.Net.Http;

namespace PawfectMatch.Components.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;

        // Temporary in-memory storage for tokens
        private string _accessToken;
        private string _refreshToken;

        public CustomAuthenticationStateProvider(
            IUserService userService,
            HttpClient httpClient)
        {
            _userService = userService;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;

            if (!string.IsNullOrEmpty(_accessToken))
            {
                User user = await _userService.GetUserByAccessTokenAsync(_accessToken);
                identity = GetClaimsIdentity(user);
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task MarkUserAsAuthenticated(User user)
        {
            _accessToken = user.AccessToken;
            _refreshToken = user.RefreshToken;

            var identity = GetClaimsIdentity(user);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            _accessToken = null;
            _refreshToken = null;

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.EmailAddress != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.EmailAddress),
                    new Claim(ClaimTypes.Role, user.Role.RoleDesc),
                    new Claim("IsUserEmployedBefore1990", IsUserEmployedBefore1990(user))
                }, "apiauth_type");
            }

            return claimsIdentity;
        }

        private string IsUserEmployedBefore1990(User user)
        {
            return user.HireDate.HasValue && user.HireDate.Value.Year < 1990 ? "true" : "false";
        }
    }
}
