using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components.Services;
using PawfectMatch.Components;
using PawfectMatch.Data;

var builder = WebApplication.CreateBuilder(args);

// Add Authentication and Authorization services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login"; // Optional: Change this to your login page URL
    });

builder.Services.AddAuthorization();


// Register the DbContextFactory
builder.Services.AddDbContextFactory<PawfectMatchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PawfectMatchContext")
    ?? throw new InvalidOperationException("Connection string 'PawfectMatchContext' not found.")));



// Add other services
builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Register DogService
builder.Services.AddScoped<DogService>();
builder.Services.AddScoped<IMessagingBrokerClient, MessagingBrokerClient>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
app.UseAuthentication();  // Add this line
app.UseAuthorization();   // Add this line

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();