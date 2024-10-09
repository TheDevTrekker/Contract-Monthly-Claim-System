using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly ClaimService _claimService;

        public CoordinatorController(ClaimService claimService)
        {
            _claimService = claimService;
        }

        public IActionResult CoReview()
        {
            var pendingClaims = _claimService.GetClaimsByStatus("Pending");
            return View(pendingClaims);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int claimId)
        {
            var claim = _claimService.GetAllClaims().FirstOrDefault(c => c.Id == claimId);
            if (claim != null)
            {
                claim.Status = "Approved by Coordinator";
            }

            return RedirectToAction("PendingClaims");
        }

        [HttpPost]
        public IActionResult RejectClaim(int claimId)
        {
            var claim = _claimService.GetAllClaims().FirstOrDefault(c => c.Id == claimId);
            if (claim != null)
            {
                claim.Status = "Rejected by Coordinator";
            }

            return RedirectToAction("PendingClaims");
        }
    }
}
