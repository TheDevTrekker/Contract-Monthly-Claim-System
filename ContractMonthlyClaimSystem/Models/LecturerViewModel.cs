namespace ContractMonthlyClaimSystem.Models
{
    public class LecturerViewModel
    {
        // Claim for the form submission
        public Claim NewClaim { get; set; }
        public List<Claim> SubmittedClaims { get; set; }

        public LecturerViewModel()
        {
            NewClaim = new Claim(); // Initialize the new claim object
            SubmittedClaims = new List<Claim>(); // Initialize the list of submitted claims
        }
    }
}
