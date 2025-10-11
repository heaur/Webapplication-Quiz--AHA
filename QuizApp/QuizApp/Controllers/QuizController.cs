using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    // Controller for managing quizzes
    public class QuizController : Controller
    {
        //A Get method to create a new quiz with variables from models
        [HttpGet]
        public IActionResult Create()
        {
            var quiz = new Quiz
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Options = new List<Option>
                        {
                            new Option(),
                            new Option()
                        }
                    }
                }
            };
            return View(quiz);
        }

        
    }
}   