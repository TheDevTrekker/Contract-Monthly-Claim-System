namespace ContractMonthlyClaimSystem.Models
{
    public class userExamples
    {
        public List<Register> manager()
        {

            return new List<Register>
            {
                new Register
                {
                    FullName = "John Doe",
                    Email = "manager@example.com",
                    Password = "password123",
                    ConfirmPassword = "password123",
                    Role = "Manager"
                }
            };
        }
    }
}
