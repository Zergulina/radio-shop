using System;

namespace AuthService.BLL.Exceptions;

public class UserCreationException : Exception
{
    public UserCreationException() : base("User creation error") { }
}
