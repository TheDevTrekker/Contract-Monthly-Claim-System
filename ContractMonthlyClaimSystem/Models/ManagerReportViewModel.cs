namespace ContractMonthlyClaimSystem.Models
{
    public class ManagerReportViewModel
    {
        public List<Claim> PendingClaims { get; set; } = new List<Claim>();
        public List<Claim> ApprovedClaims { get; set; } = new List<Claim>();
        public List<Claim> RejectedClaims { get; set; } = new List<Claim>();
    }
}
