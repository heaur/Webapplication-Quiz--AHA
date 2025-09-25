using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace QuizApp.Models


{
    public class Questions
    {
        [Key]
        public int OptionId { get; set; }
    }
}