using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class LecturerController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        

        private readonly ClaimService _claimService;
        public LecturerController(IWebHostEnvironment environment, ClaimService claimservice)
        {
            _environment = environment;
            _claimService = claimservice;
        }

        // Display the Claim Submission Form
        [HttpGet]
        public IActionResult SubmitClaims()
        {
            var lecturerName = HttpContext.Session.GetString("Name");
            // Show the submitted claims by the logged-in lecturer
            var submittedClaims = _claimService.GetAllClaims().Where(c => c.LecturerName == lecturerName).ToList();

            var viewModel = new LecturerViewModel
            {
                SubmittedClaims = submittedClaims
            };

            // Pass the view model to the view
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult LecturerTrack()
        {
            return View();
        }

        // Handle form submission
        [HttpPost]
        public async Task<IActionResult> SubmitClaimPost(LecturerViewModel model)
        {
            var lecturerName = HttpContext.Session.GetString("Name");
            model.NewClaim.LecturerName = lecturerName;
            model.NewClaim.SubmissionDate = DateTime.Now;
            model.NewClaim.Status = "Pending"; // Set default status

            if (ModelState.IsValid)
            {
                // Save file if there is one
                if (model.NewClaim.SupportingDocument != null)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Path.GetFileNameWithoutExtension(model.NewClaim.SupportingDocument.FileName)
                                         + "_" + Path.GetRandomFileName()
                                         + Path.GetExtension(model.NewClaim.SupportingDocument.FileName);

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    try
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.NewClaim.SupportingDocument.CopyToAsync(fileStream);
                        }

                        model.NewClaim.SupportingDocumentPath = "/uploads/" + uniqueFileName; // Save the path for display
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "File upload failed: " + ex.Message);
                        model.SubmittedClaims = _claimService.GetAllClaims().Where(c => c.LecturerName == lecturerName).ToList();
                        return View("SubmitClaims", model); // Return to the view with the error
                    }
                }

                // Add the claim to the service
                _claimService.AddClaim(model.NewClaim);

                // Redirect back to the claims page
                return RedirectToAction("SubmitClaims");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // Log or inspect errors
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            // If model state is invalid, reload the list of submitted claims
            model.SubmittedClaims = _claimService.GetAllClaims().Where(c => c.LecturerName == lecturerName).ToList();

            return View("SubmitClaims", model); // Return to the view with the current model
        }





    }
}

