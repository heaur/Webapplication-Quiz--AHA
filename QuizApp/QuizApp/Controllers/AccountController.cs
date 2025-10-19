using Microsoft.AspNetCore.Mvc;

namespace QuizApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // TODO: Implementer auth
            return RedirectToAction("Index", "Home");
        }
    }
}
