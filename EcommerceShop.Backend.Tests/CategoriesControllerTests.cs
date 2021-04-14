using EcommerceShop.Application.Models;
using EcommerceShop.Application.Service.Category;
using EcommerceShop.Backend.Controllers;
using EcommerceShop.Shared.Category;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceShop.Backend.Tests
{
    public class CategoriesControllerTests : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        public CategoriesControllerTests(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
        }
        [Fact]
        public async Task PostCategory_Success()
        {
            var dbContext = _fixture.Context;
            CategoryCreateRequest category = new CategoryCreateRequest { Name = "Test category" };
            var categoryService = new CategoryService(dbContext);
            var categoriesController = new CategoriesController(categoryService);
            var result = await categoriesController.PostCategory(category);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<CategoryVm>(createdAtActionResult.Value);
            Assert.Equal("Test category", returnValue.Name);
        }
        [Fact]
        public async Task UpdateCategory_Success()
        {
            var dbContext = _fixture.Context;
            Category oldCategory = new Category { Name = "Test category" };
            await dbContext.Categories.AddAsync(oldCategory);
            await dbContext.SaveChangesAsync();
            CategoryUpdateRequest newCategory = new CategoryUpdateRequest { Name = "Test1 category" };
            var categoryService = new CategoryService(dbContext);
            var categoriesController = new CategoriesController(categoryService);
            var result = await categoriesController.PutCategory(1, newCategory);
            var updatAtActionResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultValue = Assert.IsType<CategoryUpdateRequest>(updatAtActionResult.Value);
            Assert.Equal(oldCategory.Name, resultValue.Name);
            Assert.Equal(oldCategory.Name, newCategory.Name);
        }
        [Fact]
        public async Task GetCategory_Success()
        {
            var dbContext = _fixture.Context;
            await dbContext.Categories.AddRangeAsync(new Category { Name = "Test1 category" }, new Category { Name = "Test2 category" }, new Category { Name = "Test3 category" });
            await dbContext.SaveChangesAsync();
            var categoryService = new CategoryService(dbContext);
            var categoriesController = new CategoriesController(categoryService);
            var result = await categoriesController.GetCategories();
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotEmpty(actionResult.Value as IEnumerable<CategoryVm>);
        }
        [Fact]
        public async Task DeleteCategory_Success()
        {

            var dbContext = _fixture.Context;

            Category category = new Category { Name = "Test brand" };
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            var categoryService = new CategoryService(dbContext);
            var categoriesController = new CategoriesController(categoryService);
            // Act
            var result = await categoriesController.DeleteCategory(category.CategoryId);
            // Assert
            var deleteAtActionResult = Assert.IsType<NoContentResult>(result.Result);

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await categoriesController.GetCategoryById(category.CategoryId);
            });
        }
    }
}
