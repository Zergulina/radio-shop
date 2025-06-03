namespace OrderService.Dtos.User
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = null;
        public string Email { get; set; } = string.Empty;
    }
}
