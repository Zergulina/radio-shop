using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQContracts;

namespace OrderService.BLL.RabbitMQ.Messages
{
    internal class AddOrderMessage : IAddOrderMessage
    {
        public int Id { get; set; }
        public ulong OrderAmount { get; set; }
    }

    internal class RemoveOrderMessage : IRemoveOrderMessage
    {
        public int Id { get; set; }
        public ulong OrderAmount { get; set; }
    }
}
