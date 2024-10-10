namespace ContractMonthlyClaimSystem.Models
{
    public class CoReportViewModel
    {
        public List<Claim> ApprovedClaims { get; set; } = new List<Claim>();
        public List<Claim> RejectedClaims { get; set; } = new List<Claim>();
    }
}
