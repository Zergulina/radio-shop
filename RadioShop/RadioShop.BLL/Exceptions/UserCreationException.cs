using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Exceptions
{
    public class UserCreationException : Exception
    {
        public UserCreationException() : base("User creation error") { }
    }
}
