namespace ContractMonthlyClaimSystem.Models
{
    public class userExamples
    {
        public List<Register> Manager()
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

        public List<Register> Coordinator()
        {
            return new List<Register>
            {
                new Register
                {
                    FullName = "Piet Smith",
                    Email = "coordinator@example.com",
                    Password = "password123",
                    ConfirmPassword = "password123",
                    Role = "Coordinator"
                     }
            };
        }

        public List<Register> Lecturer()
        {
            return new List<Register>
            {
                new Register
                {
                     FullName = "Johannes Korf",
                    Email = "lecturer@example.com",
                    Password = "password123",
                    ConfirmPassword = "password123",
                    Role = "Lecturer"
                     }
            };
        }

    }
}
