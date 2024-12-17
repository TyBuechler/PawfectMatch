using Microsoft.JSInterop;
using System.Threading.Tasks;

public class AuthenticationService
{
    private readonly IJSRuntime _jsRuntime;
    private const string TokenKey = "auth_token";

    public AuthenticationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    // Store the token in localStorage or sessionStorage
    public async Task StoreToken(string token)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
    }

    // Retrieve the token from localStorage or sessionStorage
    public async Task<string> GetToken()
    {
        return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
    }

    // Remove the token from storage
    public async Task RemoveToken()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
    }

    // Check if user is authenticated by verifying token presence
    public async Task<bool> IsAuthenticated()
    {
        var token = await GetToken();
        return !string.IsNullOrEmpty(token);
    }
}
