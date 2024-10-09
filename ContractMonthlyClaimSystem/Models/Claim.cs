using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        [Required]
        [Range(1, 100, ErrorMessage = "Please enter a valid number of hours.")]
        public int HoursWorked { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Please enter a valid hourly rate.")]
        public decimal HourlyRate { get; set; }

        public string? Notes { get; set; }

        // This property is for uploading a file (supporting document)
        [Display(Name = "Supporting Document")]
        public IFormFile? SupportingDocument { get; set; }

        // This property will store the path of the uploaded file
        public string? SupportingDocumentPath { get; set; }
    }
}
