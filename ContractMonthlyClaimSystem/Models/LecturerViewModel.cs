namespace ContractMonthlyClaimSystem.Models
{
    public class LecturerViewModel
    {
        public List<Claim> PendingClaims { get; set; }
        public List<Claim> ApprovedClaims { get; set; }
        public List<Claim> RejectedClaims { get; set; }


        public Claim NewClaim { get; set; }
        public List<Claim> SubmittedClaims { get; set; }

        public LecturerViewModel()
        {
            NewClaim = new Claim(); // Initialize the new claim object
            SubmittedClaims = new List<Claim>(); // Initialize the list of submitted claims
        }
    }
}
