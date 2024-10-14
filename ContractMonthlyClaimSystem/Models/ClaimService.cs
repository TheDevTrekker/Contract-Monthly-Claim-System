namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimService
    {
        // Shared list for storing claims
        private List<Claim> claims = new List<Claim>();

        // Method to add a new claim
        public virtual void AddClaim(Claim claim)
        {
            claim.Id = claims.Count + 1;
            claims.Add(claim);
        }

        // Method to get all claims
        public virtual List<Claim> GetAllClaims()
        {
            return claims;
        }

        // Method to get claims by status
        public virtual List<Claim> GetClaimsByStatus(string status)
        {
            return claims.Where(c => c.Status == status).ToList();
        }
    }
}
