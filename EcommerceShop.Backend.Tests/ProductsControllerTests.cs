using EcommerceShop.Application.Models;
using EcommerceShop.Application.Service.Product;
using EcommerceShop.Application.Service.Storage;
using EcommerceShop.Backend.Controllers;
using EcommerceShop.Shared.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceShop.Backend.Tests
{
    public class ProductsControllerTests : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        public ProductsControllerTests(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task GetProducts_Success()
        {
            var dbContext = _fixture.Context;
            Category category = new Category { Name = "Test category" };
            await dbContext.Categories.AddAsync(category);
            Brand brand = new Brand { Name = "Test brand" };
            await dbContext.Brands.AddAsync(brand);
            await dbContext.SaveChangesAsync();
            Product product1 = new Product
            {
                Name = "product1",
                Description = "description 1",
                Price = 11,
                Amount = 50,
                BrandId = brand.BrandId,
                CategoryId = category.CategoryId
            };
            Product product2 = new Product
            {
                Name = "product2",
                Description = "description 2",
                Price = 12,
                Amount = 50,
                BrandId = brand.BrandId,
                CategoryId = category.CategoryId
            };
            int pageNumber = 1;
            int pageSize = 1;
            await dbContext.Products.AddRangeAsync(product1, product2);
            await dbContext.SaveChangesAsync();
            var fileStorageService = FileStorageServiceMock.FileStorageService();
            var productService = new ProductService(dbContext, fileStorageService);
            var productsController = new ProductsController(productService);
            var result = await productsController.GetProducts(pageNumber, pageSize);
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ProductPaginationVm>(actionResult.Value);
            Assert.NotEmpty(returnValue.items);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetProductById_Success(int id)
        {
            var dbContext = _fixture.Context;
            Category category = new Category { Name = "Test category" };
            await dbContext.Categories.AddAsync(category);
            Brand brand = new Brand { Name = "Test brand" };
            await dbContext.Brands.AddAsync(brand);
            await dbContext.SaveChangesAsync();
            Product product1 = new Product
            {
                Name = "product1",
                Description = "description 1",
                Price = 11,
                Amount = 50,
                BrandId = brand.BrandId,
                CategoryId = category.CategoryId
            };
            Product product2 = new Product
            {
                Name = "product2",
                Description = "description 2",
                Price = 12,
                Amount = 50,
                BrandId = brand.BrandId,
                CategoryId = category.CategoryId
            };
            await dbContext.Products.AddRangeAsync(product1, product2);
            await dbContext.SaveChangesAsync();
            var fileStorageService = FileStorageServiceMock.FileStorageService();
            var productService = new ProductService(dbContext, fileStorageService);
            var productsController = new ProductsController(productService);
            var result = await productsController.GetProductById(id);
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ProductVm>(actionResult.Value);
            Assert.NotNull(returnValue);
        }
        [Fact]
        public async Task PostProduct_Success()
        {
            var dbContext = _fixture.Context;
            Category category = new Category { Name = "Test category" };
            await dbContext.Categories.AddAsync(category);
            Brand brand = new Brand { Name = "Test brand" };
            await dbContext.Brands.AddAsync(brand);
            await dbContext.SaveChangesAsync();

            var fileStorageService = FileStorageServiceMock.FileStorageService();
            ProductCreateRequest product = new ProductCreateRequest
            {
                Name = "product1",
                Description = "description 1",
                Price = 11,
                Amount = 50,
                BrandId = brand.BrandId,
                CategoryId = category.CategoryId,
                Images = new List<IFormFile>()
                {
                     new FormFile(null,1,1,"picture1.jpg","picture1"),
                     new FormFile(null,2,3,"picture3.jpg","picture2"),
                     new FormFile(null,2,3,"picture2.jpg","picture3")
                }

            };
            var productService = new ProductService(dbContext, fileStorageService);
            var productsController = new ProductsController(productService);
            var result = await productsController.PostProduct(product);

            var createdAtActionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ProductVm>(createdAtActionResult.Value);
            Assert.NotNull(returnValue.Name);
        }
        [Fact]
        public async Task UpdateProduct_Success()
        {
            var dbContext = _fixture.Context;
            Category category = new Category { Name = "Test category" };
            await dbContext.Categories.AddAsync(category);
            Brand brand = new Brand { Name = "Test brand" };
            await dbContext.Brands.AddAsync(brand);
            await dbContext.SaveChangesAsync();
            var fileStorageService = FileStorageServiceMock.FileStorageService();
            Product product1 = new Product
            {
                Name = "product1",
                Description = "description 1",
                Price = 11,
                Amount = 50,
                BrandId = brand.BrandId,
                CategoryId = category.CategoryId
            };
            await dbContext.Products.AddAsync(product1);
            await dbContext.SaveChangesAsync();
            ProductUpdateRequest product2 = new ProductUpdateRequest
            {
                Name = "product2",
                Description = "description 2",
                Price = 12,
                Amount = 50,
                BrandId = brand.BrandId,
                CategoryId = category.CategoryId
            };
            var productService = new ProductService(dbContext, fileStorageService);
            var productsController = new ProductsController(productService);
            var result = await productsController.PutProduct(product1.ProductId,product2);
            var updatAtActionResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultValue = Assert.IsType<ProductVm>(updatAtActionResult.Value);
            Assert.Equal(product1.Name, resultValue.Name);
            Assert.Equal(product1.Name, product2.Name);
        }
        [Fact]
        public async Task DeleteProduct_Success()
        {

            var dbContext = _fixture.Context;

            Category category = new Category { Name = "Test category" };
            await dbContext.Categories.AddAsync(category);
            Brand brand = new Brand { Name = "Test brand" };
            await dbContext.Brands.AddAsync(brand);
            await dbContext.SaveChangesAsync();
            var fileStorageService = FileStorageServiceMock.FileStorageService();
            Product product = new Product
            {
                Name = "product1",
                Description = "description 1",
                Price = 11,
                Amount = 50,
                BrandId = brand.BrandId,
                CategoryId = category.CategoryId
            };
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();


            var productService = new ProductService(dbContext, fileStorageService);
            var productsController = new ProductsController(productService);
            var result = await productsController.DeleteProduct(product.ProductId);
            Assert.IsType<NoContentResult>(result.Result);
        }
    }
}
