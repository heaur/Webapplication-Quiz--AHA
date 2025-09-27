using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace QuizApp.Models{
    public class QuizDBContext : DbContext {
    public QuizDBContext(DbContextOptions<QuizDBContext> options) : base(options)
    {
        //oppretter db hvis den ikke finnes
        Database.EnsureCreated();
    }
    //tabell til db 
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Result> Results { get; set; }

}


}

