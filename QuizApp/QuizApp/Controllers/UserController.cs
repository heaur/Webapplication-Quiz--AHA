using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Implement your login logic here
        return RedirectToAction("Index", "Home");
    }
}
