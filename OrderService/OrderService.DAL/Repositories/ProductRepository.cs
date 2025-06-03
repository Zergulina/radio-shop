using Microsoft.EntityFrameworkCore;
using OrderService.DAL.Data;
using OrderService.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DAL.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(x => x.Id == id);
        }

        public async Task<List<int>> ExistsListAsync(List<int> ids)
        {
            var existIds = await _context.Products.Where(x => ids.Contains(x.Id)).Select(x => x.Id).ToListAsync();
            return ids.Except(existIds).ToList();
        }
    }
}
