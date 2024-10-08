using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class LecturerController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public LecturerController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // Display the Claim Submission Form
        [HttpGet]
        public IActionResult SubmitClaims()
        {
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

                    model.SupportingDocumentPath = uniqueFileName;
                }

                // Save the claim to database (placeholder, actual DB saving logic will vary)
                // _context.Claims.Add(model);
                // await _context.SaveChangesAsync();

                // Redirect to confirmation page or status tracking page
                return RedirectToAction("ClaimSubmitted");
            }

            return View("SubmitClaim", model);
        }

        // Claim submitted confirmation
        public IActionResult ClaimSubmitted()
        {
            return View();
        }
    }
}

