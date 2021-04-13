using EcommerceShop.Application.Data;
using EcommerceShop.Application.Models;
using EcommerceShop.Application.Service.Storage;
using EcommerceShop.Shared.Image;
using EcommerceShop.Shared.Product;
using EcommerceShop.Shared.Rating;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var images = await _context.Images.Where(i => i.ProductId == ProductId).ToListAsync();
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImageUrl);
            }
            var rates = await _context.Ratings.Where(i => i.ProductId == ProductId).ToListAsync();
            _context.Ratings.RemoveRange(rates);
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ProductVm>> GetProductByCategoryId(int CategoryId)
        {
            var products = await _context.Products.Select(x => new ProductVm
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Amount = x.Amount,
                BrandId = x.BrandId,
                CategoryId = x.CategoryId
            }).Where(x => x.CategoryId == CategoryId).ToListAsync();

            foreach (ProductVm product in products)
            {
                var listImageVm = await _context.Images.Select(x => new ImageVm
                {
                    ImageId = x.ImageId,
                    ImageUrl = x.ImageUrl,
                    Caption = x.Caption,
                    IsDefault = x.IsDefault,
                    ProductId = x.ProductId
                }).Where(x => x.ProductId == product.ProductId).ToListAsync();
                product.Images = listImageVm;
                product.Ratings = new List<RatingVm>();
                var listRate = await _context.Ratings.Select(x => new RatingVm
                {
                    RatingId = x.RatingId,
                    RateValue = x.RateValue,
                    FeedBack = x.FeedBack,
                    UserId = x.UserId,
                    ProductId = x.ProductId
                }).Where(x => x.ProductId == product.ProductId).ToListAsync();
                product.Ratings = listRate;
            }
            return products;
        }
        public async Task<IEnumerable<ProductVm>> GetProductByBrandId(int BrandId)
        {
            var products = await _context.Products.Select(x => new ProductVm
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Amount = x.Amount,
                BrandId = x.BrandId,
                CategoryId = x.CategoryId
            }).Where(x => x.BrandId == BrandId).ToListAsync();

            foreach (ProductVm product in products)
            {
                var listImageVm = await _context.Images.Select(x => new ImageVm
                {
                    ImageId = x.ImageId,
                    ImageUrl = x.ImageUrl,
                    Caption = x.Caption,
                    IsDefault = x.IsDefault,
                    ProductId = x.ProductId
                }).Where(x => x.ProductId == product.ProductId).ToListAsync();
                product.Images = listImageVm;
                product.Ratings = new List<RatingVm>();
                var listRate = await _context.Ratings.Select(x => new RatingVm
                {
                    RatingId = x.RatingId,
                    RateValue = x.RateValue,
                    FeedBack = x.FeedBack,
                    UserId = x.UserId,
                    ProductId = x.ProductId
                }).Where(x => x.ProductId == product.ProductId).ToListAsync();
                product.Ratings = listRate;
            }
            return products;
        }
        public async Task<ProductVm> GetProductById(int ProductId)
        {
            var product = await _context.Products.Select(x => new ProductVm
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Amount = x.Amount,
                BrandId = x.BrandId,
                CategoryId = x.CategoryId
            }).FirstOrDefaultAsync(x => x.ProductId == ProductId);
            product.Images = new List<ImageVm>();
            var listImageVm = await _context.Images.Select(x => new ImageVm
            {
                ImageId = x.ImageId,
                ImageUrl = x.ImageUrl,
                Caption = x.Caption,
                IsDefault = x.IsDefault,
                ProductId = x.ProductId
            }).Where(x => x.ProductId == ProductId).ToListAsync();
            product.Images = listImageVm;
            product.Ratings = new List<RatingVm>();
            var listRate = await _context.Ratings.Select(x => new RatingVm
            {
                RatingId = x.RatingId,
                RateValue = x.RateValue,
                FeedBack = x.FeedBack,
                UserId = x.UserId,
                ProductId = x.ProductId
            }).Where(x => x.ProductId == ProductId).ToListAsync();
            product.Ratings = listRate;
            return product;
        }

        public async Task<IEnumerable<ProductVm>> GetRelatedProducts(int ProductId,int CategoryId)
        {
            var products = await _context.Products.Include(c=>c.Category).Select(x => new ProductVm
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Amount = x.Amount,
                BrandId = x.BrandId,
                CategoryId = x.CategoryId
            }).Where(x=>x.ProductId!=ProductId && x.CategoryId== CategoryId).Take(3).ToListAsync();

            foreach (ProductVm product in products)
            {
                var listImageVm = await _context.Images.Select(x => new ImageVm
                {
                    ImageId = x.ImageId,
                    ImageUrl = x.ImageUrl,
                    Caption = x.Caption,
                    IsDefault = x.IsDefault,
                    ProductId = x.ProductId
                }).Where(x => x.ProductId == product.ProductId).ToListAsync();
                product.Images = listImageVm;
                product.Ratings = new List<RatingVm>();
                var listRate = await _context.Ratings.Select(x => new RatingVm
                {
                    RatingId = x.RatingId,
                    RateValue = x.RateValue,
                    FeedBack = x.FeedBack,
                    UserId = x.UserId,
                    ProductId = x.ProductId
                }).Where(x => x.ProductId == ProductId).ToListAsync();
                product.Ratings = listRate;
            }
            return products;
        }
        public async Task<ProductPaginationVm> GetProducts(PagingRequestVm pagingRequestVm)
        {
            var products =  _context.Products.Select(x => new ProductVm
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Amount = x.Amount,
                BrandId = x.BrandId,
                CategoryId = x.CategoryId
            }).AsQueryable();
            
            int count = products.Count();

            int CurrentPage = pagingRequestVm.pageNumber;

            int? PageSize = pagingRequestVm.pageSize;

            int TotalCount = count;

            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            var items = await products.Skip((CurrentPage - 1) * PageSize.Value).Take(PageSize.Value).ToListAsync();
            foreach (ProductVm product in items)
            {
                var listImageVm = await _context.Images.Select(x => new ImageVm
                {
                    ImageId = x.ImageId,
                    ImageUrl = x.ImageUrl,
                    Caption = x.Caption,
                    IsDefault = x.IsDefault,
                    ProductId = x.ProductId
                }).Where(x => x.ProductId == product.ProductId).ToListAsync();
                product.Images = listImageVm;
                product.Ratings = new List<RatingVm>();
                var listRate = await _context.Ratings.Select(x => new RatingVm
                {
                    RatingId = x.RatingId,
                    RateValue = x.RateValue,
                    FeedBack = x.FeedBack,
                    UserId = x.UserId,
                    ProductId = x.ProductId
                }).Where(x => x.ProductId == product.ProductId).ToListAsync();
                product.Ratings = listRate;
            }
            var previousPage = CurrentPage > 1;

            var nextPage = CurrentPage < TotalPages;
            ProductPaginationVm result = new ProductPaginationVm
            {
                items = items,
                totalCount = TotalCount,
                pageSize = PageSize.Value,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage = previousPage,
                nextPage = nextPage
            };
            return result;
        }
        public async Task<ProductPaginationVm> SearchProducts(string? keyword, int? categoryId, int? brandId, PagingRequestVm pagingRequestVm)
        {
            var products = _context.Products.Select(x => new ProductVm
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Amount = x.Amount,
                BrandId = x.BrandId,
                CategoryId = x.CategoryId
            }).AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword) && categoryId==null && brandId==null)
            {
                products = _context.Products.Select(x => new ProductVm
                {
                    ProductId = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Amount = x.Amount,
                    BrandId = x.BrandId,
                    CategoryId = x.CategoryId
                }).Where(x=>x.Name.Contains(keyword)).AsQueryable();
            }
            else if(!string.IsNullOrWhiteSpace(keyword) && categoryId != null && brandId == null)
            {
                products = _context.Products.Select(x => new ProductVm
                {
                    ProductId = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Amount = x.Amount,
                    BrandId = x.BrandId,
                    CategoryId = x.CategoryId
                }).Where(x => x.Name.Contains(keyword)&&x.CategoryId==categoryId).AsQueryable();
            }
            else if (!string.IsNullOrWhiteSpace(keyword) && categoryId == null && brandId != null)
            {
                products = _context.Products.Select(x => new ProductVm
                {
                    ProductId = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Amount = x.Amount,
                    BrandId = x.BrandId,
                    CategoryId = x.CategoryId
                }).Where(x => x.Name.Contains(keyword) && x.BrandId == brandId).AsQueryable();
            }
            else if (!string.IsNullOrWhiteSpace(keyword) && categoryId != null && brandId != null)
            {
                products = _context.Products.Select(x => new ProductVm
                {
                    ProductId = x.ProductId,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Amount = x.Amount,
                    BrandId = x.BrandId,
                    CategoryId = x.CategoryId
                }).Where(x => x.Name.Contains(keyword) && categoryId != null && x.BrandId == brandId).AsQueryable();
            }
            int count = products.Count();

            int CurrentPage = pagingRequestVm.pageNumber;

            int? PageSize = pagingRequestVm.pageSize;

            int TotalCount = count;

            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            var items = await products.Skip((CurrentPage - 1) * PageSize.Value).Take(PageSize.Value).ToListAsync();
            foreach (ProductVm product in items)
            {
                var listImageVm = await _context.Images.Select(x => new ImageVm
                {
                    ImageId = x.ImageId,
                    ImageUrl = x.ImageUrl,
                    Caption = x.Caption,
                    IsDefault = x.IsDefault,
                    ProductId = x.ProductId
                }).Where(x => x.ProductId == product.ProductId).ToListAsync();
                product.Images = listImageVm;
                product.Ratings = new List<RatingVm>();
                var listRate = await _context.Ratings.Select(x => new RatingVm
                {
                    RatingId = x.RatingId,
                    RateValue = x.RateValue,
                    FeedBack = x.FeedBack,
                    UserId = x.UserId,
                    ProductId = x.ProductId
                }).Where(x => x.ProductId == product.ProductId).ToListAsync();
                product.Ratings = listRate;
            }
            var previousPage = CurrentPage > 1;

            var nextPage = CurrentPage < TotalPages;
            ProductPaginationVm result = new ProductPaginationVm
            {
                items = items,
                totalCount = TotalCount,
                pageSize = PageSize.Value,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage = previousPage,
                nextPage = nextPage
            };
            return result;
        }

        public async Task<ProductVm> PostProduct(ProductCreateRequest request)
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
            var listImageVm = await _context.Images.Select(x => new ImageVm
            {
                ImageId = x.ImageId,
                ImageUrl = x.ImageUrl,
                Caption = x.Caption,
                IsDefault = x.IsDefault,
                ProductId = x.ProductId
            }).Where(x => x.ProductId == product.ProductId).ToListAsync();
            var productVm = new ProductVm()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Amount = product.Amount,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Images = listImageVm
            };
            return productVm;
        }

        public async Task<ProductVm> PutProduct(int ProductId, ProductUpdateRequest request)
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
            var listImageVm = await _context.Images.Select(x => new ImageVm
            {
                ImageId = x.ImageId,
                ImageUrl = x.ImageUrl,
                Caption = x.Caption,
                IsDefault = x.IsDefault,
                ProductId = x.ProductId
            }).Where(x => x.ProductId == product.ProductId).ToListAsync();
            var listRate = await _context.Ratings.Select(x => new RatingVm
            {
                RatingId = x.RatingId,
                RateValue = x.RateValue,
                FeedBack = x.FeedBack,
                UserId = x.UserId,
                ProductId = x.ProductId
            }).Where(x => x.ProductId == product.ProductId).ToListAsync();
            var productVm = new ProductVm()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Amount = product.Amount,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Images = listImageVm,
                Ratings = listRate
            };
            return productVm;
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
