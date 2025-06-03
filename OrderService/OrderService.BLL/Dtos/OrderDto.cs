using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BLL.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderedAt { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public string DeliveryAddress { get; set; }
        public bool IsAccepted { get; set; }
        public List<OrderUnitDto> Units { get; set; }
        public UserDto? User { get; set; }
    }
}
