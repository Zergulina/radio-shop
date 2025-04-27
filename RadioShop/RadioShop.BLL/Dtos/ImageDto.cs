using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.BLL.Dtos
{
    public class ImageDto
    {
        public int Id { get; set; }
        public string ImageData { get; set; } = string.Empty;
        public string ImageExtention {  get; set; } = string.Empty;
    }
}
