using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace QuizApp.Models


{
    public class Questions
    {
         [Key]                                // PK
        public int QuestionId { get; set; }

        [Required]                           // Må høre til quiz
        public int QuizId { get; set; }

        [Required]                           // tekst kan ikke være tom
        [MaxLength(500)]                     // Maks 500 tegn
        public string Text { get; set; }

        



    }
}