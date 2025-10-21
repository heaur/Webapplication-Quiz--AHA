using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using System.Linq;

namespace QuizApp.Controllers
{
    // Controller responsible for creating and saving new quizzes
    public class QuizController : Controller
    {
        private readonly QuizDBContext _db;

        // Constructor — receives QuizDBContext from the framework
        public QuizController(QuizDBContext db) => _db = db;

        // GET: Quiz/Create
        // Displays the Create Quiz page with one empty question and two empty options
        [HttpGet]
        public IActionResult Create()
        {
            // Create a new Quiz object with one Question and two Options by default
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

            // Pass the quiz model to the view
            return View(quiz);
        }

        // POST: Quiz/Create
        // Handles form submission when the user creates a new quiz
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps prevent Cross-Site Request Forgery (CSRF) attacks
        public IActionResult Create(Quiz quiz)
        {
            //Cleans up any empty fields before validation
            if (quiz.Questions != null)
            {
                // Remove questions that have no text
                quiz.Questions = quiz.Questions
                    .Where(q => q != null && !string.IsNullOrWhiteSpace(q.Text))
                    .ToList();

                // Loop through each question
                foreach (var q in quiz.Questions)
                {
                    // Ensure Options list is not null
                    q.Options ??= new List<Option>();

                    // Remove options that have no text
                    q.Options = q.Options
                        .Where(o => o != null && !string.IsNullOrWhiteSpace(o.Text))
                        .ToList();
                }
            }

            //Validation rules

            // If no questions exist after cleaning, add a model error
            if (quiz.Questions == null || quiz.Questions.Count == 0)
                ModelState.AddModelError("", "Add at least one question.");

            // Loop through each question with its index
            foreach (var (q, idx) in quiz.Questions.Select((q, i) => (q, i)))
            {
                // Ensure each question has at least two options
                if (q.Options.Count < 2)
                    ModelState.AddModelError($"Questions[{idx}].Options", "Each question must have at least two options.");

                // Ensure there is at least one correct option (optional rule)
                if (!q.Options.Any(o => o.IsCorrect))
                    ModelState.AddModelError($"Questions[{idx}].Options", "Mark at least one option as correct.");
            }

            // If validation fails, redisplay the same view with error messages
            if (!ModelState.IsValid)
            {
                return View(quiz);
            }

            // ---If validation succeeds, save the quiz to the database---

            // quiz.OwnerId = <set this if you have a logged-in user> if not using, leave as is.;

            // and will save all of them in one transaction.
            _db.Quizzes.Add(quiz);
            _db.SaveChanges();

            // Redirect to the home page after saving successfully
            return RedirectToAction("Index", "Home");
        }
    }
}
