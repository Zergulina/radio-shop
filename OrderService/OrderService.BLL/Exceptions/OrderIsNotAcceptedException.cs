using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Exceptions
{
    public class OrderIsNotAcceptedException : Exception
    {
        public OrderIsNotAcceptedException() : base("Order is not accepted") { }
    }
}
