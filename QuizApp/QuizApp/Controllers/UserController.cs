using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using System.Linq;


namespace QuizApp.Controllers
{
    // Controller responsible for user-related actions
    // such as creating a new user (registration), logging in, and logging out
    public class UserController : Controller
    {
        private readonly QuizDBContext _db;

        // Constructor: injects the database context (QuizDBContext)
        public UserController(QuizDBContext db)
        {
            _db = db;
        }


        // Displays the registration form when the user chooses create user from login page
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        
        // Handles the submitted registration form (when the user presses "Create Account")
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public IActionResult CreateUser(User user, string passwordConfirm)
        {
            // Check if the two entered passwords match
            if (user.Password != passwordConfirm)
            {
                // Add a general model error that will be displayed in the validation summary
                ModelState.AddModelError("", "Passwords do not match.");
                // Return the same view with the user's input so they can fix it
                return View(user);
            }

            // Check if the username already exists in the database
            if (_db.Users.Any(u => u.Username == user.Username))
            {
                ModelState.AddModelError("", "Username already exists. Please choose another one.");
                return View(user);
            }

            // If the form data is valid and username is unique:
            // Save the new user to the database
            _db.Users.Add(user);
            _db.SaveChanges();

            // Use TempData to send a one-time message that can be shown on the next page
            TempData["Success"] = "User created successfully. You can now log in.";

            // Redirect to the Login page after successful registration
            return RedirectToAction("Login");
        }
    }
}
