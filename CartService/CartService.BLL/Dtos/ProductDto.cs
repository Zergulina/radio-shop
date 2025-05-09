using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.BLL.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public long PriceRuble { get; set; }
        public byte PriceKopek { get; set; }
        public byte Rating { get; set; } = 0;
        public long OrderAmount { get; set; } = 0;
        public List<TagDto> Tags { get; set; } = new List<TagDto>();
        public string? ImageId { get; set; } = null;
    }
}
