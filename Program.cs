using PawfectMatch.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PawfectMatch.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<PawfectMatchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PawfectMatchContext") ?? throw new InvalidOperationException("Connection string 'PawfectMatchContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
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
    app.UseMigrationsEndPoint();
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