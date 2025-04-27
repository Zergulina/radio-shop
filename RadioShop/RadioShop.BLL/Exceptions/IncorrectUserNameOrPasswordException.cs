using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Exceptions
{
    public class IncorrectUserNameOrPasswordException : Exception
    {
        public IncorrectUserNameOrPasswordException() : base("Username is not found and/or password is incorrect") { }
    }
}
