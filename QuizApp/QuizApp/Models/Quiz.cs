using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; } //PK


        [Required]
        [MaxLength(55)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        //Defines who is owner/created the Quiz
        public int OwnerId { get; set; } //FK

        //Lists all questions in the quiz
        public List<Question> Questions { get; set; }
    }
}