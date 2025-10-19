using Microsoft.AspNetCore.Mvc;
using QuizApp.DAL.Interfaces;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly IUnitOfWork _uow;
        public QuizController(IUnitOfWork uow) => _uow = uow;

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new QuizCreateVm
            {
                Title = string.Empty,
                Description = null,
                OwnerId = 0, // Sett via innlogget bruker når auth kommer på plass
                Questions = new List<QuestionCreateVm>
                {
                    new QuestionCreateVm
                    {
                        Text = string.Empty,
                        Options = new List<OptionCreateVm> { new(), new() }
                    }
                }
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizCreateVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // Map VM -> domain
            var quiz = new Quiz
            {
                Title = vm.Title,
                Description = vm.Description,
                OwnerId = vm.OwnerId,
                Questions = vm.Questions.Select(q => new Question
                {
                    Text = q.Text,
                    Options = q.Options.Select(o => new Option
                    {
                        Text = o.Text,
                        IsCorrect = o.IsCorrect
                    }).ToList()
                }).ToList()
            };

            await _uow.Quizzes.AddAsync(quiz);
            await _uow.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }

    // ViewModels (unngår overposting)
    public class QuizCreateVm
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int OwnerId { get; set; }
        public List<QuestionCreateVm> Questions { get; set; } = new();
    }

    public class QuestionCreateVm
    {
        public string Text { get; set; } = string.Empty;
        public List<OptionCreateVm> Options { get; set; } = new();
    }

    public class OptionCreateVm
    {
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
