﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcommerceShop.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EcommerceShop.Application.Brand;
using Microsoft.Extensions.Logging;

namespace EcommerceShop.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<BrandVm>>> GetBrands()
        {
            var brands = await _brandService.GetBrands();
            return Ok(brands);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BrandVm>> GetBrand(int id)
        {
            var brand = await _brandService.GetBrandById(id);

            return Ok(brand);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PutBrand(int id, BrandUpdateRequest brandUpdateRequest)
        {
            var brand = await _brandService.PutBrand(id, brandUpdateRequest);
            if (brand > 0)
            {
                return Ok(brandUpdateRequest);
            }
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<BrandVm>> PostBrand(BrandCreateRequest brandCreateRequest)
        {
            var brand = await _brandService.PostBrand(brandCreateRequest);
            return CreatedAtAction("GetBrand", new { id = brand.BrandId }, new BrandVm { BrandId = brand.BrandId, Name = brand.Name });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            await _brandService.DeleteBrand(id);
            return NoContent();
        }
    }
}
