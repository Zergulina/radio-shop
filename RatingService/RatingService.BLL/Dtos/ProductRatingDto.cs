using RatingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingService.BLL.Dtos
{
    public class ProductRatingDto
    {
        public string UserId { get; set; }
        public UserDto? User { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public byte Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
