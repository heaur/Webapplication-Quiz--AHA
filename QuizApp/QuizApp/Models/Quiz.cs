using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuizApp.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; } //PK

        [Required]
        [MaxLength(55)]
        public string Title { get; set; } = string.Empty; //default value

        [MaxLength(255)]
        public string? Description { get; set; } //makes nullable

        //Defines who is owner/created the Quiz
        [BindNever] //Never bind from form when creating quiz. Set by the system.
        public int OwnerId { get; set; } //FK

        [ValidateNever]             // do not validate this from the form
        public User? Owner { get; set; }  // make it nullable to avoid “required” validation

        //Lists all questions in the quiz
        public List<Question> Questions { get; set; } = new(); //initialiserer en tom liste
    }
}