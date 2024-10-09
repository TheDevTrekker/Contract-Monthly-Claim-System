using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {

        private static int _nextId = 1; // Static field to keep track of the next ID
        public int Id { get; private set; }

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

        public string LecturerName { get; set; } // New property
        public DateTime SubmissionDate { get; set; } // New property
        public string Status { get; set; } = "Pending"; // New property to track the status

        public Claim()
        {
            Id = _nextId++;
        }
    }
}
