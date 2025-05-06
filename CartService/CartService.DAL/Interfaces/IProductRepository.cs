using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.DAL.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> ExistsAsync(int id);
    }
}
