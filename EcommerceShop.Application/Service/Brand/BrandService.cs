using EcommerceShop.Application.Data;
using EcommerceShop.Application.Models;
using EcommerceShop.Shared;
using EcommerceShop.Shared.Brand;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Brand
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;
       
        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> DeleteBrand(int BrandId)
        {
            var brand = await _context.Brands.FindAsync(BrandId);
            if (brand == null)
            {
                return -1;
            }
            _context.Brands.Remove(brand);
            return await _context.SaveChangesAsync();

        }

        public async Task<BrandVm> GetBrandById(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return new BrandVm();
            }

            var brandVm = new BrandVm
            {
                BrandId = brand.BrandId,
                Name = brand.Name
            };

            return brandVm;
        }

        public async Task<IEnumerable<BrandVm>> GetBrands()
        {
            return await _context.Brands
                  .Select(x => new BrandVm { BrandId = x.BrandId, Name = x.Name })
                  .ToListAsync();
        }

        public async Task<BrandVm> PostBrand(BrandCreateRequest request)
        {

            var brand = new EcommerceShop.Application.Models.Brand
            {
                Name = request.Name
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            var brandVm = new BrandVm
            {
                BrandId = brand.BrandId,
                Name = brand.Name
            };

            return brandVm;
        }

        public async Task<int> PutBrand(int id,BrandUpdateRequest request)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return -1;
            }

            brand.Name = request.Name;
            return await _context.SaveChangesAsync();
        }
    }
}
