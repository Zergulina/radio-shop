using RadioShop.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Interfaces.Helpers
{
    internal interface ITokenService
    {
        string CreateToken(UserDto user, IList<string> roles);
    }
}
