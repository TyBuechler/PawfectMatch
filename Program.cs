using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components.Services;
using PawfectMatch.Data;
using Blazored.LocalStorage;
using PawfectMatch.Components;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient with your base address (pointing to the UserAuthentication API)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7142/") // Replace with the correct API base URL
});

// Set up cookie-based authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token"; // Custom cookie name
        options.LoginPath = "/Login"; // Redirect path for login
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30); // Cookie expiry
        options.AccessDeniedPath = "/access-denied"; // Redirect path for access denial
    });

// Add authorization policies
builder.Services.AddAuthorization();

// Register the DbContextFactory for PawfectMatch with the connection string
builder.Services.AddDbContextFactory<PawfectMatchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PawfectMatchContext")
    ?? throw new InvalidOperationException("Connection string 'PawfectMatchContext' not found.")));

// Add support for Entity Framework-specific features in Razor QuickGrid
builder.Services.AddQuickGridEntityFrameworkAdapter();

// Add Blazored.LocalStorage for storing data in the browser
builder.Services.AddBlazoredLocalStorage();

// Register custom services like DogService and MessagingBrokerClient
builder.Services.AddScoped<DogService>();
builder.Services.AddScoped<IMessagingBrokerClient, MessagingBrokerClient>(); // Ensure MessagingBrokerClient is implemented

// Add Razor Components with interactive server rendering
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // Required for Blazor Server interactivity

// Add developer exception page for database errors (Development only)
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Enforce strict transport security
}
else
{
    app.UseMigrationsEndPoint(); // Enable database migrations endpoint in development
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Use Authentication and Authorization middleware
app.UseAuthentication(); // Ensures authentication middleware is invoked
app.UseAuthorization();  // Ensures authorization middleware is invoked

// Map Razor Components for the Blazor application
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Enable interactive server rendering for Blazor Server

app.Run();
