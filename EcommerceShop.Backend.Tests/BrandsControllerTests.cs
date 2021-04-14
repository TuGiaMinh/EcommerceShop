using EcommerceShop.Application.Brand;
using EcommerceShop.Application.Models;
using EcommerceShop.Backend.Controllers;
using EcommerceShop.Shared.Brand;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceShop.Backend.Tests
{
    public class BrandsControllerTests: IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        public BrandsControllerTests(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task PostBrand_Success()
        {
            var dbContext = _fixture.Context;
            BrandCreateRequest brand = new BrandCreateRequest { Name = "Test brand" };
            var brandService = new BrandService(dbContext);
            var controller = new BrandsController(brandService);
            var result = await controller.PostBrand(brand);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<BrandVm>(createdAtActionResult.Value);
            Assert.Equal("Test brand", returnValue.Name);
        }
        [Fact]
        public async Task UpdateBrand_Success()
        {
            var dbContext = _fixture.Context;
            Brand oldBrand = new Brand { Name = "Test brand" };
            await dbContext.Brands.AddAsync(oldBrand);
            await dbContext.SaveChangesAsync();
            BrandUpdateRequest newBrand = new BrandUpdateRequest { Name = "Test1 brand" };
            var brandService = new BrandService(dbContext);
            var controller = new BrandsController(brandService);
            var result = await controller.PutBrand(1, newBrand);
            var updatAtActionResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultValue = Assert.IsType<BrandUpdateRequest>(updatAtActionResult.Value);
            Assert.Equal(oldBrand.Name, resultValue.Name);
            Assert.Equal(oldBrand.Name, newBrand.Name);
        }
        [Fact]
        public async Task GetBrand_Success()
        {
            var dbContext = _fixture.Context;
            await dbContext.Brands.AddRangeAsync(new Brand { Name = "Test1 brand" }, new Brand { Name = "Test2 brand" }, new Brand { Name = "Test3 brand" });
            await dbContext.SaveChangesAsync();
            var brandService = new BrandService(dbContext);
            var brandsController = new BrandsController(brandService);
            var result = await brandsController.GetBrands();
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotEmpty(actionResult.Value as IEnumerable<BrandVm>);
        }
        [Fact]
        public async Task DeleteBrand_Success()
        {

            var dbContext = _fixture.Context;

            Brand brand = new Brand { Name = "Test brand" };
            await dbContext.Brands.AddAsync(brand);
            await dbContext.SaveChangesAsync();

            var brandService = new BrandService(dbContext);
            var brandsController = new BrandsController(brandService);
            // Act
            var result = await brandsController.DeleteBrand(brand.BrandId);
            // Assert
            var deleteAtActionResult = Assert.IsType<NoContentResult>(result.Result);

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await brandsController.GetBrand(brand.BrandId);
            });
        }
    }
}
