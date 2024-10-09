namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimService
    {
        // Shared list for storing claims
        private List<Claim> claims = new List<Claim>();

        // Method to add a new claim
        public void AddClaim(Claim claim)
        {
            claims.Add(claim);
        }

        // Method to get all claims
        public List<Claim> GetAllClaims()
        {
            return claims;
        }

        // Method to get claims by status
        public List<Claim> GetClaimsByStatus(string status)
        {
            return claims.Where(c => c.Status == status).ToList();
        }
    }
}
