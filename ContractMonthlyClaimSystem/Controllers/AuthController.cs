using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        //examples of users
        public List<Register> managerExample = new List<Register>();
        public List<Register> coordinatorExample = new List<Register>();
        public List<Register> lecturerExample = new List<Register>();

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
            return RedirectToAction("Index", "Home");
        }




        [HttpPost]
        public IActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Login(Login model)
        {

            return View(model);
        }
    }
}
