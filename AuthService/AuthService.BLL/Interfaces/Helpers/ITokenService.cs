using System;
using AuthService.BLL.Dtos;

namespace AuthService.BLL.Interfaces.Helpers;

internal interface ITokenService
{
    string CreateToken(UserDto user, IList<string> roles);
}