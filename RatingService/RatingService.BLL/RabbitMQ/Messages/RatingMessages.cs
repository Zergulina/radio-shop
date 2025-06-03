using RabbitMQContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.BLL.RabbitMQ.Messages
{
    internal class AddRatingMessage : IAddRatingMessage
    {
        public int Id { get; set; }
        public byte Rating{ get; set; }
    }
    internal class RemoveRatingMessage : IRemoveRatingMessage
    {
        public int Id { get; set; }
        public byte Rating { get; set; }
    }
}
