﻿using EcommerceShop.Backend.Data;
using EcommerceShop.Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcommerceShop.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EcommerceShop.Backend.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        [Authorize("Bearer")]
        public class BrandsController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public BrandsController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            [AllowAnonymous]
            public async Task<ActionResult<IEnumerable<BrandVm>>> GetBrands()
            {
                return await _context.Brands
                    .Select(x => new BrandVm { BrandId = x.BrandId, Name = x.Name })
                    .ToListAsync();
            }

            [HttpGet("{id}")]
            [AllowAnonymous]
            public async Task<ActionResult<BrandVm>> GetBrand(int id)
            {
                var brand = await _context.Brands.FindAsync(id);

                if (brand == null)
                {
                    return NotFound();
                }

                var brandVm = new BrandVm
                {
                    BrandId = brand.BrandId,
                    Name = brand.Name
                };

                return brandVm;
            }

            [HttpPut("{id}")]
            [Authorize(Roles = "admin")]
            public async Task<IActionResult> PutBrand(int id, BrandCreateRequest brandCreateRequest)
            {
                var brand = await _context.Brands.FindAsync(id);

                if (brand == null)
                {
                    return NotFound();
                }

                brand.Name = brandCreateRequest.Name;
                await _context.SaveChangesAsync();

                return NoContent();
            }

            [HttpPost]
            [Authorize(Roles = "admin")]
            public async Task<ActionResult<BrandVm>> PostBrand(BrandCreateRequest brandCreateRequest)
            {
                var brand = new Brand
                {
                    Name = brandCreateRequest.Name
                };

                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBrand", new { id = brand.BrandId }, new BrandVm { BrandId = brand.BrandId, Name = brand.Name });
            }

            [HttpDelete("{id}")]
            [Authorize(Roles = "admin")]
            public async Task<IActionResult> DeleteBrand(int id)
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null)
                {
                    return NotFound();
                }

                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
}