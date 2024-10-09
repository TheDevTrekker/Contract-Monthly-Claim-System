using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly userExamples _userExamples;
        //examples of users
        public List<Register> managerExample = new List<Register>();
        public List<Register> coordinatorExample = new List<Register>();
        public List<Register> lecturerExample = new List<Register>();

        public AuthController(ILogger<AuthController> logger, userExamples userExamples)
        {
            _logger = logger;
            _userExamples = userExamples;
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
            lecturerExample = _userExamples.Lecturer();
            coordinatorExample = _userExamples.Coordinator();
            managerExample = _userExamples.Manager();
            
            if (ModelState.IsValid)
            {
                var user = managerExample.FirstOrDefault(item => item.Email == model.Email && item.Password == model.Password);

                if (user == null)
                {
                    user = coordinatorExample.FirstOrDefault(item => item.Email == model.Email && item.Password == model.Password);
                }

                if (user == null)
                {
                    user = lecturerExample.FirstOrDefault(item => item.Email == model.Email && item.Password == model.Password);
                }

                if (user != null)
                {
                    HttpContext.Session.SetString("Name", user.FullName);
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Role", user.Role);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }
    }
}
