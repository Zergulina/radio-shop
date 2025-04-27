using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long PriceRuble { get; set; }
        public long PriceKopek { get; set; }
        public byte Rating { get; set; }
        public List<TagDto> Tags { get; set; } = new List<TagDto>();
        public int? ImageId { get; set; }
        public ImageDto? Image { get; set; } = null;
    }
}
