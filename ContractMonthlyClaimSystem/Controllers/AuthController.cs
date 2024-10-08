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
        public List<Register> managerExample = new List<Register>();

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
            managerExample = _userExamples.manager();
            
            if (ModelState.IsValid)
            {
                foreach (var item in managerExample)
                {
                    if (model.Email == item.Email && model.Password == item.Password)
                    {
                        HttpContext.Session.SetString("Role", item.Role);
                        return RedirectToAction("ManagerReview", "AcademicManager");
                    }
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
