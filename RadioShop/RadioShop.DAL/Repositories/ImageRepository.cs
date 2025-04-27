using Microsoft.EntityFrameworkCore;
using RadioShop.DAL.Data;
using RadioShop.DAL.Interfaces;
using RadioShop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioShop.DAL.Repositories
{
    internal class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<Image> CreateAsync(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<Image?> DeleteAsync(int id)
        {
            var existingImage = await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
            if (existingImage == null)
            {
                return null;
            }

            _context.Images.Remove(existingImage);
            await _context.SaveChangesAsync();
            return existingImage;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Images.AnyAsync(x => x.Id == id);
        }

        public async Task<Image?> GetByIdAsync(int id)
        {
            return await _context.Images.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
