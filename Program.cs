using PawfectMatch.Components;
using PawfectMatch.Data; // Ensure you include the namespace for PawfectMatchContext
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components.Services;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContextFactory
builder.Services.AddDbContextFactory<PawfectMatchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PawfectMatchContext")
    ?? throw new InvalidOperationException("Connection string 'PawfectMatchContext' not found.")));

// Add other services
builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Register DogService
builder.Services.AddScoped<DogService>();

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
