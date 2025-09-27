using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } //PK

        [Required]
        [MaxLength(25)]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        public List<Quiz> Quizzes { get; set; }
        public List<Result> Results { get; set; }
    }
}