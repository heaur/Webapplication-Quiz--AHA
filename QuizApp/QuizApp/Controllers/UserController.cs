using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;      // For session extensions
using QuizApp.Models;
using System.Linq;

namespace QuizApp.Controllers
{
    // Controller responsible for user-related actions:
    // - Registering a new user (CreateUser)
    // - Logging in (Login)
    // - Logging out (Logout)
    public class UserController : Controller
    {
        private readonly QuizDBContext _db;

        // Constructor — injects the EF Core DbContext so we can access the Users table
        public UserController(QuizDBContext db)
        {
            _db = db;
        }

        // Displays the login page (Login.cshtml) with an empty form.
        [HttpGet]
        public IActionResult Login() => View();

        // Handles the form submission from the login page.
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public IActionResult Login(string username, string password)
        {
            // Look up the user by username.
            var user = _db.Users.FirstOrDefault(u => u.Username == username);

            // Validate credentials (This is not a hashed password. Might change this before the webaplication gets released in prod.)
            if (user == null || user.Password != password)
            {
                // Add a model error visible in the view's validation summary.
                ModelState.AddModelError("", "Invalid username or password.");
                // Re-render the login view so the user can try again.
                return View();
            }

            // Store minimal info in session to mark the user as logged in.
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);

            // One-time success message (display after redirect).
            TempData["Success"] = $"Welcome, {user.Username}!";

            // Redirect to home page (or wherever you want after login).
            return RedirectToAction("Index", "Home");
        }

        // Displays the registration form for creating a new user.
        [HttpGet]
        public IActionResult CreateUser() => View();

        // Handles the registration form submission and creates a new user.
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public IActionResult CreateUser(User user, string passwordConfirm)
        {
            // Basic server-side check: confirm that the two entered passwords match.
            if (user.Password != passwordConfirm)
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View(user);
            }

            // Ensure the username is unique.
            bool usernameTaken = _db.Users.Any(u => u.Username == user.Username);
            if (usernameTaken)
            {
                ModelState.AddModelError("", "Username already exists.");
                return View(user);
            }

            // If you later add more validation attributes on the User model, check ModelState here:
            if (!ModelState.IsValid)
            {
                // Re-render the form with validation messages.
                return View(user);
            }

            // Save the new user to the database.
            // NOTE: In production, DO NOT store plain text passwords.
            //       Hash the password and store the hash instead.
            _db.Users.Add(user);
            _db.SaveChanges();

            // Inform the user and send them to the login page.
            TempData["Success"] = "User created successfully. You can now log in.";
            return RedirectToAction("Login");
        }

        // Clears the session and redirects to the Login page.
        [HttpGet]
        public IActionResult Logout()
        {
            // Remove all session data (logs the user out).
            HttpContext.Session.Clear();

            // Optional: show a one-time message after logout.
            TempData["Success"] = "You have been logged out.";

            // Back to Login page.
            return RedirectToAction("Login");
        }
    }
}
