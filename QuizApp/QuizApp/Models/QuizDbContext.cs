using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
namespace QuizApp.Models;
{
    public class QuizDBContext : DbContext{
        public QuizDBContext(DbContextOptions<>options): base(options)
        {
            Database.Ensurecreated();
        }
    }





}

