namespace JonkerBudget.Application.Users.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public bool IsAdUser { get; set; }
    }
}
