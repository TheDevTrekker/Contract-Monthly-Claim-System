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

        public IActionResult ManagerReport()
        {
            var submittedClaims = _claimService.GetAllClaims();

            // Categorize claims by status
            var pendingClaims = submittedClaims
                .Where(c => c.Status == "Pending" || c.Status == "Approved by Coordinator")
                .ToList();

            var approvedClaims = submittedClaims
                .Where(c => c.Status == "Approved by Manager")
                .ToList();

            var rejectedClaims = submittedClaims
                .Where(c => c.Status == "Rejected by Coordinator" || c.Status == "Rejected by Manager")
                .ToList();

            // Prepare the view model with categorized claims
            var viewModel = new ManagerReportViewModel
            {
                PendingClaims = pendingClaims,
                ApprovedClaims = approvedClaims,
                RejectedClaims = rejectedClaims
            };

            // Pass the view model to the view
            return View(viewModel);
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
