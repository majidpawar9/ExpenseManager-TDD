using System;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManager_TDD.Controllers;
using ExpenseManager_TDD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpenseManager_TDD.Tests.IntegrationTests
{
    [TestClass]
    public class CategoryControllerIntegrationTests
    {
        private DbContextOptions<ApplicationDbContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            // Setup your DbContextOptions for testing (use an in-memory database for testing purposes).
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Seed the database with some test data.
            using (var context = new ApplicationDbContext(_options))
            {
                context.Categories.Add(new Category { CategoryID = 1, Title = "Test Category", Icon = "TestIcon", Type = "TestType" });
                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new CategoryController(context);

                // Act
                var result = await controller.Index();

                // Assert
                Assert.IsInstanceOfType(result, typeof(ViewResult));
            }
        }

        [TestMethod]
        public void AddOrEdit_ReturnsViewResultWithCategoryModel()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new CategoryController(context);

                // Act
                var result = controller.AddOrEdit();

                // Assert
                Assert.IsInstanceOfType(result, typeof(ViewResult));
                Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(Category));
            }
        }

        [TestMethod]
        public async Task AddOrEdit_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new CategoryController(context);
                var category = new Category { Title = "New Category", Icon = "NewIcon", Type = "NewType" };

                // Act
                var result = await controller.AddOrEdit(category);

                // Assert
                Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
                Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);

                // Ensure the category is added to the database
                Assert.AreEqual(2, context.Categories.Count()); 
                Assert.AreEqual("New Category", context.Categories.Last().Title);
            }
        }

        [TestMethod]
        public async Task AddOrEdit_WithInvalidModel_ReturnsViewResult()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new CategoryController(context);
                var category = new Category(); // Invalid model

                // Act
                controller.ModelState.AddModelError("Title", "Title is required");
                var result = await controller.AddOrEdit(category);

                // Assert
                Assert.IsInstanceOfType(result, typeof(ViewResult));
            }
        }

        [TestMethod]
        public async Task DeleteConfirmed_WithValidId_RedirectsToIndex()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new CategoryController(context);

                // Act
                var result = await controller.DeleteConfirmed(1);

                // Assert
                Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
                Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);

                // Ensure the category is removed from the database
                Assert.AreEqual(0, context.Categories.Count());
            }
        }

   
       

    
    }
}
