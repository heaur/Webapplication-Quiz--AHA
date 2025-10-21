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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Define the relationship between Quiz and Question.
    // One Quiz can have many Questions.
    // The "HasMany" and "WithOne" methods set up a one-to-many relationship.
    // "HasForeignKey" tells EF Core which property in Question (QuizId) is the foreign key.
    // "OnDelete(DeleteBehavior.Cascade)" means: 
    // if a Quiz is deleted, all related Questions are automatically deleted too.
    modelBuilder.Entity<Quiz>()
        .HasMany(q => q.Questions)
        .WithOne(qn => qn.Quiz!)
        .HasForeignKey(qn => qn.QuizId)
        .OnDelete(DeleteBehavior.Cascade);

    // Define the relationship between Question and Option.
    // One Question can have many Options.
    // The same pattern as above: "QuestionId" is the foreign key inside the Option table.
    // Cascade delete ensures that when a Question is deleted, all its Options are deleted automatically.
    modelBuilder.Entity<Question>()
        .HasMany(qn => qn.Options)
        .WithOne(op => op.Question!)
        .HasForeignKey(op => op.QuestionId)
        .OnDelete(DeleteBehavior.Cascade);

    // Define the relationship between Quiz and its Owner (a User).
    // A Quiz belongs to one User, but a User can own many Quizzes (WithMany()).
    // "HasForeignKey" sets "OwnerId" in the Quiz table as the foreign key.
    // ".IsRequired(false)" means the relationship is optional:
    // A Quiz can exist without having an Owner assigned.
    modelBuilder.Entity<Quiz>()
        .HasOne(q => q.Owner)
        .WithMany()
        .HasForeignKey(q => q.OwnerId)
        .IsRequired(false);
}

    }
}

