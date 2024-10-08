using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class AcademicManagerController : Controller
    {
        public IActionResult ManagerReview()
        {
            return View();
        }
    }
}
