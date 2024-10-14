using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;

        // Static list to store users
        private static List<Register> registeredUsers = new List<Register>();
        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["Title"] = "Register";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been successfully logged out.";
            return RedirectToAction("Index", "Home");
        }




        [HttpPost]
        public IActionResult Register(Register model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                TempData["ErrorMessage"] = "Passwords do not match.";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                // Check if the email already exists
                if (registeredUsers.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    TempData["ErrorMessage"] = "Registration failed. Email is already registered.";
                    return View(model);
                }

                // Add new user to the list
                registeredUsers.Add(model);
                TempData["SuccessMessage"] = "Registration successful! You can now log in.";
                return RedirectToAction("Login");
            }

            TempData["ErrorMessage"] = "Registration failed. Please correct the errors below.";
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {

            // Validate login credentials
            var user = registeredUsers.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("Role", user.Role);
                HttpContext.Session.SetString("Name", user.FullName);
                HttpContext.Session.SetString("Email", user.Email);
                TempData["SuccessMessage"] = "Login successful! Welcome to your account.";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Invalid email or password.";
            return View(model);
        }
    }
}
