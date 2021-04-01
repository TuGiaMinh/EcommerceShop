﻿using EcommerceShop.Backend.Data;
using EcommerceShop.Backend.Models;
using EcommerceShop.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Brand
{
    class BrandService : IBrandService
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
                throw new Exception("Cannot find id");
            }
            _context.Brands.Remove(brand);
            return await _context.SaveChangesAsync();

        }

        public async Task<BrandVm> GetBrandById(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                throw new Exception("Cannot find id");
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

            var brand = new EcommerceShop.Backend.Models.Brand
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
                throw new Exception("Cannot find id");
            }

            brand.Name = request.Name;
            return await _context.SaveChangesAsync();
        }
    }
}