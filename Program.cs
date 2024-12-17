using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components.Services;
using PawfectMatch.Data;
using System.Net.Http;
using PawfectMatch.Components;
using Blazored.LocalStorage;
using PawfectMatch.Services;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient with your base address (pointing to the UserAuthentication API)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7142/") // Change this to the correct API URL
});

// Set up authentication using cookies (this is for a fallback in case you want Cookie-based login flow)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token"; // Custom cookie name
        options.LoginPath = "/Login"; // Redirect path for login
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30); // Cookie expiry
        options.AccessDeniedPath = "/access-denied"; // Redirect path for access denial
    });

// Set up authorization
builder.Services.AddAuthorization();

// Register the DbContextFactory with the connection string for PawfectMatch
builder.Services.AddDbContextFactory<PawfectMatchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PawfectMatchContext")
    ?? throw new InvalidOperationException("Connection string 'PawfectMatchContext' not found.")));

// Add additional services
builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddBlazoredLocalStorage();

// Register custom services like DogService and MessagingBrokerClient
builder.Services.AddScoped<DogService>();
builder.Services.AddScoped<IMessagingBrokerClient, MessagingBrokerClient>();

// Register custom AuthenticationService
builder.Services.AddScoped<AuthenticationService>();

// Add HttpClient and Razor Components
builder.Services.AddHttpClient();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // Razor components for Blazor Server

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Use Authentication and Authorization middleware
app.UseAuthentication();  // Ensures the Authentication middleware is invoked
app.UseAuthorization();   // Ensures Authorization middleware is invoked

// Map Razor Components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Only needed for Blazor Server

app.Run();
