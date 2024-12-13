using PawfectMatch.Components.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PawfectMatch.Components.Data;
using PawfectMatch;
using Microsoft.AspNetCore.Authentication.Cookies; // Add this for cookie authentication
using Microsoft.AspNetCore.Components;
using PawfectMatch.Components;

var builder = WebApplication.CreateBuilder(args);

// Register services with DI container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register NavigationManager and HttpClient with base address
builder.Services.AddScoped<HttpClient>(sp =>
    new HttpClient { BaseAddress = new Uri(sp.GetRequiredService<NavigationManager>().BaseUri) });

// Register IUserService and its implementation
builder.Services.AddScoped<IUserService, UserService>();

// Register AuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)  // Specify the authentication scheme
    .AddCookie(options =>
    {
        // Configure cookie options (you can customize these settings)
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
    });

// Register authorization services
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SeniorEmployee", policy =>
        policy.RequireClaim("IsUserEmployedBefore1990", "true"));
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Add anti-forgery middleware
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();