using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components.Services;
using PawfectMatch.Data;
using Blazored.LocalStorage;
using PawfectMatch.Components;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient with your base address 
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7142/") 
});

// Set up cookie-based authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/Login"; 
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30); 
        options.AccessDeniedPath = "/access-denied"; 
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
builder.Services.AddScoped<IMessagingBrokerClient, MessagingBrokerClient>(); 

// Add Razor Components with interactive server rendering
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); 

// Add developer exception page for database errors (Development only)
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); 
}
else
{
    app.UseMigrationsEndPoint(); 
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Use Authentication and Authorization middleware
app.UseAuthentication(); 
app.UseAuthorization();  

// Map Razor Components for the Blazor application
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); 

app.Run();
