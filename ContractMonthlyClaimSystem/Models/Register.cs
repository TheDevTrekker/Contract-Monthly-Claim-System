using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
    public class Register
    {
        [Required]
        public string? FullName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, MinLength(6)]
        public string? Password { get; set; }

        [Required, Compare("Password")]
        public string? ConfirmPassword { get; set; }

        [Required]
        public string? Role { get; set; }
    }
}
