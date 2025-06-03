using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQContracts
{
    public interface IAddRatingMessage
    {
        public int Id { get; set; }
        public byte Rating { get; set; }
    }
    public interface IRemoveRatingMessage
    {
        public int Id { get; set; }
        public byte Rating { get; set; }
    }
}
