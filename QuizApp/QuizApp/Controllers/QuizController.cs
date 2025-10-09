using Microsoft.AspNetCore.Mvc;

public class QuizController : Controller
{
    public IActionResult Create()
    {
        return View(); // viser Views/Quiz/Create.cshtml
    }
}
