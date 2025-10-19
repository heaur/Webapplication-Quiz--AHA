using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizApp.Models;
using QuizApp.DAL.Interfaces;
using QuizApp.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var dbPath = Path.Combine(builder.Environment.ContentRootPath, "QuizDatabase.db");
builder.Services.AddDbContext<QuizDBContext>(opt =>
    opt.UseSqlite($"Data Source={dbPath}"));

// DbContext (SQLite)
builder.Services.AddDbContext<QuizDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// DAL (Repositories + Unit of Work)
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
