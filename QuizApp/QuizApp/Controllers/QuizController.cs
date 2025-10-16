using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    // Controller for managing quizzes
    public class QuizController : Controller
    {
        //conecting the database
         private readonly QuizDBContext _db;
        public QuizController(QuizDBContext db) => _db = db; 
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                //Save the quiz to a database
                _db.Quizzes.Add(quiz);
                _db.SaveChanges();
                // For this example, we'll just redirect to a confirmation page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // If the model state is invalid, return the same view with the current quiz data
                return View(quiz);
            }
        }
    }
}   