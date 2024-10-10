using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult LecturerAcc()
        {
            HttpContext.Session.SetString("Role", "Lecturer");
            HttpContext.Session.SetString("Name", "Phillip du Preez");
            HttpContext.Session.SetString("Email", "phillip@gmail.com");
            return View("Index", "Home");
        }

        [HttpGet]
        public IActionResult CoordinatorAcc()
        {
            HttpContext.Session.SetString("Role", "Coordinator");
            HttpContext.Session.SetString("Name", "Petra Korf");
            HttpContext.Session.SetString("Email", "petra@gmail.com");
            return View("Index", "Home");
        }

        [HttpGet]
        public IActionResult ManagerAcc()
        {
            HttpContext.Session.SetString("Role", "Manager");
            HttpContext.Session.SetString("Name", "Carl du Toit");
            HttpContext.Session.SetString("Email", "carl@gmail.com");
            return View("Index", "Home");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
