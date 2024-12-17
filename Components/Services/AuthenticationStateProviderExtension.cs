using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PawfectMatch.Components.Services
{
    public static class AuthenticationStateProviderExtensions
    {
        /// <summary>
        /// Retrieves the current user's ClaimsPrincipal.
        /// </summary>
        /// <param name="provider">The AuthenticationStateProvider instance.</param>
        /// <returns>The ClaimsPrincipal representing the current user.</returns>
        public static async Task<ClaimsPrincipal> GetUserAsync(this AuthenticationStateProvider provider)
        {
            var authState = await provider.GetAuthenticationStateAsync();
            return authState.User;
        }

        /// <summary>
        /// Checks whether the current user is authenticated.
        /// </summary>
        /// <param name="provider">The AuthenticationStateProvider instance.</param>
        /// <returns>True if the user is authenticated; otherwise, false.</returns>
        public static async Task<bool> IsUserAuthenticatedAsync(this AuthenticationStateProvider provider)
        {
            var user = await provider.GetUserAsync();
            return user.Identity?.IsAuthenticated ?? false;
        }
    }
}
