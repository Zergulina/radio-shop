using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.BLL.Dtos
{
    public class ImageDto
    {
        public string Id { get; set; }
        public Stream ImageData { get; set; }
        public string ImageType { get; set; }
    }
}
