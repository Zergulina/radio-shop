using System;

namespace AuthService.BLL.Dtos;

public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? MiddleName { get; set; } = null;
    public string Email { get; set; } = string.Empty;
}
