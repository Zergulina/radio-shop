using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string DeliveryLocation {  get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public List<ProductOrder> ProductOrders { get; set; }
    }
}
