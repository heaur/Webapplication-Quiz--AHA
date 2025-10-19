using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace QuizApp.Models
{
    public class QuizDBContext : DbContext
    {
        public QuizDBContext(DbContextOptions<QuizDBContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<Option> Options { get; set; } = default!;
        public DbSet<Quiz> Quizzes { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Result> Results { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Quiz (1) -> (many) Questions
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(z => z.Questions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question (1) -> (many) Options
            modelBuilder.Entity<Option>()
                .HasOne(o => o.Question)
                .WithMany(q => q.Options)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Quiz (many) -> (1) Owner (User)
            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.Owner)
                .WithMany(u => u.Creations)
                .HasForeignKey(q => q.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Result koblinger
            modelBuilder.Entity<Result>()
                .HasOne(r => r.User)
                .WithMany(u => u.History)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Quiz)
                .WithMany()
                .HasForeignKey(r => r.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // Valgfritt: modellér "riktig svar"-pekeren hvis du ønsker FK til Option
            // (per nå er AnswerOptionID bare en int uten navigasjon, det er ok.)
            // Hvis du vil knytte den opp, må Option ha en inverse navigation eller vi lar den stå som scalar.

            base.OnModelCreating(modelBuilder);
        }
    }
}

