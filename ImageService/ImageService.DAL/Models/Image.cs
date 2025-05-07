using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.DAL.Models
{
    public class Image
    {
        public string Id { get; set; }
        public Stream ImageData { get; set; }
        public string ImageType { get; set; }
    }
}
