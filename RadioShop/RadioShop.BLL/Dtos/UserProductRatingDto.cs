using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Dtos
{
    public class UserProductRatingDto
    {
        public string UserId {  get; set; }
        public byte Rating { get; set; }
        public string? Description { get; set; }
        public DateTime DateTime { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
