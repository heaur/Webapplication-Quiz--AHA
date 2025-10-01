using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Option
    {
        //pk 
        public int OptionID { get; set; }

        //FK

        public int QuestionId { get; set; }

        //text for the alternatives 
        public string Text { get; set; } = string.Empty;

        //if the alternativ is corect 
        public bool IsCorrect { get; set; }
    }
    
}