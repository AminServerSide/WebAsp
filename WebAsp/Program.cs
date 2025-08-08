using Microsoft.EntityFrameworkCore;
using WebAsp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")) // Ensure this matches appsettings.json
           .EnableSensitiveDataLogging(false)); // Disable sensitive data logging in production

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed the database with error handling
try
{
    AppDbInitializer.Seed(app);
}
catch (Exception ex)
{
    // Log the exception (e.g., using ILogger)
    Console.WriteLine($"Seeding failed: {ex.Message}");
    throw; // Re-throw to let the application handle it
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();