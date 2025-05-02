using System;

namespace AuthService.BLL.Exceptions;

public class UserNameIsOccupiedException : Exception
{
    public UserNameIsOccupiedException() : base("The username is already occupied") { }
}