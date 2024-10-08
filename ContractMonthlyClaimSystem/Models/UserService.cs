namespace ContractMonthlyClaimSystem.Models
{
    public class UserService
    {
        private List<User> users = new List<User>
    {
        new User { Email = "lecturer@example.com", Password = "password123", FullName = "John Doe", Role = "Lecturer" },
        new User { Email = "coordinator@example.com", Password = "password123", FullName = "Jane Smith", Role = "Coordinator" },
        new User { Email = "manager@example.com", Password = "password123", FullName = "Alice Brown", Role = "Manager" }
    };

        public User? ValidateUser(string email, string password)
        {
            return users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
