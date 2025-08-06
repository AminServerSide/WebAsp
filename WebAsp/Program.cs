// You'll likely need these using statements for Entity Framework Core
using Microsoft.EntityFrameworkCore;
using WebAsp.Data;

// Replace 'YourApp.Data' with the actual namespace where your AppDbContext is located
using WebAsp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// This section is the new equivalent of the 'ConfigureServices' method in Startup.cs

// 1. DBContext Configuration from your old Startup.cs
// I've assumed you are using SQL Server and your connection string is named "DefaultConnection" in appsettings.json
// Please adjust if you use a different database or connection string name.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// This line was already in your Program.cs
builder.Services.AddControllersWithViews();




var app = builder.Build();

AppDbInitializer.Seed(app);

// Configure the HTTP request pipeline.
// This section is the new equivalent of the 'Configure' method in Startup.cs
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// 2. Added UseStaticFiles()
// This is important for serving CSS, JavaScript, and image files in an MVC app.
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// The MapStaticAssets() and .WithStaticAssets() methods are not standard
// in ASP.NET Core. They might be from a specific library you are using.
// If you get an error here, you may need to check if you're missing a NuGet package.
// app.MapStaticAssets(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// .WithStaticAssets();


app.Run();

// You will also need to make sure your connection string is in your 'appsettings.json' file.
// It should look something like this:
/*
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YourDbName;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
*/
