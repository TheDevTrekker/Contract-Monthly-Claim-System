using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class AcademicManagerController : Controller
    {
        private readonly ClaimService _claimService;

        public AcademicManagerController(ClaimService claimService)
        {
            _claimService = claimService;
        }

        public IActionResult ManagerReview()
        {
            var pendingClaims = _claimService.GetClaimsByStatus("Approved by Coordinator");
            return View(pendingClaims);
        }

        public IActionResult Report()
        {
            var approvedClaims = _claimService.GetClaimsByStatus("Approved by Manager");
            var rejectedClaims = _claimService.GetClaimsByStatus("Rejected by Manager");

            return View();
        }

        [HttpPost]
        public IActionResult ApproveClaim(int claimId)
        {
            var claim = _claimService.GetAllClaims().FirstOrDefault(c => c.Id == claimId);
            if (claim != null)
            {
                claim.Status = "Approved by Manager";
            }

            return RedirectToAction("ManagerReview");
        }

        [HttpPost]
        public IActionResult RejectClaim(int claimId)
        {
            var claim = _claimService.GetAllClaims().FirstOrDefault(c => c.Id == claimId);
            if (claim != null)
            {
                claim.Status = "Rejected by Manager";
            }

            return RedirectToAction("ManagerReview");
        }
    }
}
