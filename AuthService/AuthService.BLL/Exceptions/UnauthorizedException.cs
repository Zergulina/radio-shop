using System;

namespace AuthService.BLL.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("Not enough permissions") { }
}