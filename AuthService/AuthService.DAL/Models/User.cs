using System;
using Microsoft.AspNetCore.Identity;

namespace AuthService.DAL.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set;} = string.Empty;
    public string? MiddleName { get; set; } = null;
}
