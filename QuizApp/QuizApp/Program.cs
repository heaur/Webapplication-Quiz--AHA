using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Connecting the application to the SQLite database
builder.Services.AddDbContext<QuizDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Build and configure the app later after registering session services (only one Build call will remain).

    // 3) Sessions (so we can remember logged-in users between requests)
//    - Distributed memory cache is required by Session.
//    - You can fine-tune cookie name, lifetime, etc.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".QuizApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(60); // user stays logged in for up to 60 min of inactivity
    options.Cookie.HttpOnly = true;                 // mitigate XSS
    options.Cookie.IsEssential = true;              // allow even if user hasn't consented to non-essential cookies
});

var app = builder.Build();

// Error handling & security headers
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 4) Enable sessions BEFORE Authorization (so controllers can read/write session)
app.UseSession();

app.UseAuthorization();

// Default route (covers UserController/Login/CreateUser etc.)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
