using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace QuizApp.Models{
    public class QuizDBContext : DbContext
    {
        public QuizDBContext(DbContextOptions<QuizDBContext> options) : base(options)
        {
            //oppretter db hvis den ikke finnes
            Database.EnsureCreated();
        }
        //tabell til db 
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<Option> Options { get; set; } = default!;
        public DbSet<Quiz> Quizzes { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Result> Results { get; set; } = default!;
    }
}

