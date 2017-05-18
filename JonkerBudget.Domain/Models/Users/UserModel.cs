namespace JonkerBudget.Domain.Models.Users
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public bool IsAdUser { get; set; }       
    }
}
