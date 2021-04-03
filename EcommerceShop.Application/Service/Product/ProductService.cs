using EcommerceShop.Application.Data;
using EcommerceShop.Application.Models;
using EcommerceShop.Application.Service.Storage;
using EcommerceShop.Shared.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Service.Product
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public ProductService(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<int> DeleteProduct(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null)
            {
                throw new Exception("Cannot find id");
            }
            var images = _context.Images.Where(i => i.ProductId == ProductId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImageUrl);
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Models.Product>> GetProductByCategoryId(int CategoryId)
        {
            return await _context.Products.Include(p => p.Images).Include(p => p.Category).Include(p => p.Brand).Where(x => x.CategoryId == CategoryId).ToListAsync();
        }
        public async Task<IEnumerable<EcommerceShop.Application.Models.Product>> GetProductByBrandId(int BrandId)
        {
            return await _context.Products.Include(p => p.Images).Include(p => p.Category).Include(p => p.Brand).Where(x=>x.BrandId==BrandId).ToListAsync();
        }

        public async Task<IEnumerable<EcommerceShop.Application.Models.Product>> GetProducts()
        {
            return await _context.Products.Include(p=>p.Images).Include(p => p.Category).Include(p=>p.Brand).ToListAsync();
        }

        public async Task<EcommerceShop.Application.Models.Product> PostProduct(ProductCreateRequest request)
        {
            var product = new EcommerceShop.Application.Models.Product()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Amount = request.Amount,
                BrandId = request.BrandId,
                CategoryId = request.CategoryId
            };
            product.Images = new List<Image>();
            if ((request.Images != null) && (request.Images.Count > 0))
            {
                foreach (IFormFile file in request.Images)
                {
                    product.Images.Add(new Image()
                    {
                        Caption = "Thumbnail image",
                        ImageUrl = await this.SaveFile(file),
                        IsDefault = false,
                    });
                }
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<EcommerceShop.Application.Models.Product> PutProduct(int ProductId,ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(ProductId);

            if (product == null)
            {
                throw new Exception("Cannot find id");
            }
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Amount = request.Amount;
            product.BrandId = request.BrandId;
            product.CategoryId = request.CategoryId;
            var images = _context.Images.Where(i => i.ProductId == ProductId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImageUrl);
            }
            product.Images = new List<Image>();
            if ((request.Images != null) && (request.Images.Count > 0))
            {
                foreach (IFormFile file in request.Images)
                {
                    product.Images.Add(new Image()
                    {
                        Caption = "Thumbnail image",
                        ImageUrl = await this.SaveFile(file),
                        IsDefault = false,
                    });
                }
            }
            await _context.SaveChangesAsync();
            return product;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        
    }
}
