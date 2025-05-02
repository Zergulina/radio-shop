using System;

namespace AuthService.BLL.Exceptions;

public class IncorrectUserNameOrPasswordException : Exception
{
    public IncorrectUserNameOrPasswordException() : base("Username is not found and/or password is incorrect") { }
}