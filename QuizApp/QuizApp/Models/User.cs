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

        //Lists all quizzes created by the user
        public List<Quiz> Creations { get; set; }

        //Lists all quiz results taken by the user
        public List<Result> History { get; set; }
    }
}