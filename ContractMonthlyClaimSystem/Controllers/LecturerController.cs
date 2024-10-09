using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class LecturerController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private static List<ContractMonthlyClaimSystem.Models.Claim> _submittedClaims = new List<ContractMonthlyClaimSystem.Models.Claim>();

        public LecturerController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // Display the Claim Submission Form
        [HttpGet]
        public IActionResult SubmitClaims()
        {
            ViewBag.Claims = _submittedClaims;
            return View();
        }

        // Handle form submission
        [HttpPost]
        public async Task<IActionResult> SubmitClaimPost(ContractMonthlyClaimSystem.Models.Claim model)
        {
            if (ModelState.IsValid)
            {
                // Save file if there is one
                if (model.SupportingDocument != null)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Path.GetFileNameWithoutExtension(model.SupportingDocument.FileName)
                                         + "_" + Path.GetRandomFileName()
                                         + Path.GetExtension(model.SupportingDocument.FileName);

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.SupportingDocument.CopyToAsync(fileStream);
                    }

                    model.SupportingDocumentPath = uniqueFileName; // Save path for display
                }

                _submittedClaims.Add(model); // Add the model to the list for runtime display

                return RedirectToAction("SubmitClaims");
            }

            ViewBag.Claims = _submittedClaims;
            return View("SubmitClaims", model);
        }


        // Claim submitted confirmation
        public IActionResult ClaimSubmitted()
        {
            return View();
        }
    }
}

