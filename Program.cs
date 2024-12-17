using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components.Services;
using PawfectMatch.Components;
using PawfectMatch.Data;
using PawfectMatch.Components.Pages;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add Authentication and Authorization services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; // Optional: Change this to your login page URL
    });

builder.Services.AddAuthorization(); // Add authorization


// Register the DbContextFactory
builder.Services.AddDbContextFactory<PawfectMatchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PawfectMatchContext")
    ?? throw new InvalidOperationException("Connection string 'PawfectMatchContext' not found.")));

// Add configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add other services
builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DogService>();
builder.Services.AddScoped<IMessagingBrokerClient, MessagingBrokerClient>();
builder.Services.AddHttpClient();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Build the application
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
app.UseAuthentication();  // This must be called before UseAuthorization
app.UseAuthorization();   // This must come after UseAuthentication

// Map the Razor components and interactive server components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Run the application
app.Run();
