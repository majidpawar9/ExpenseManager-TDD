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
    public class TransactionControllerIntegrationTests
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
                var category = new Category { CategoryID = 1, Title = "Test Category", Icon = "TestIcon", Type = "TestType" };
                context.Categories.Add(category);
                context.Transactions.Add(new Transaction { TransactionID = 1, CategoryID = 1, Amount = 100, Note = "Test Transaction", Date = DateTime.Now });
                context.SaveChanges();
            }
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new TransactionController(context);

                // Act
                var result = await controller.Index();

                // Assert
                Assert.IsInstanceOfType(result, typeof(ViewResult));
            }
        }

        [TestMethod]
        public void AddOrEdit_ReturnsViewResultWithTransactionModel()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new TransactionController(context);

                // Act
                var result = controller.AddOrEdit();

                // Assert
                Assert.IsInstanceOfType(result, typeof(ViewResult));
                Assert.IsInstanceOfType(((ViewResult)result).Model, typeof(Transaction));
            }
        }

        [TestMethod]
        public async Task AddOrEdit_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new TransactionController(context);
                var transaction = new Transaction { CategoryID = 1, Amount = 50, Note = "New Transaction", Date = DateTime.Now };

                // Act
                var result = await controller.AddOrEdit(transaction);

                // Assert
                Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
                Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);

                // Ensure the transaction is added to the database
                Assert.AreEqual(2, context.Transactions.Count()); // Assuming one transaction was seeded initially
                Assert.AreEqual("New Transaction", context.Transactions.Last().Note);
            }
        }

        [TestMethod]
        public async Task AddOrEdit_WithInvalidModel_ReturnsViewResult()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new TransactionController(context);
                var transaction = new Transaction(); // Invalid model

                // Act
                controller.ModelState.AddModelError("Amount", "Amount is required");
                var result = await controller.AddOrEdit(transaction);

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
                var controller = new TransactionController(context);

                // Act
                var result = await controller.DeleteConfirmed(1);

                // Assert
                Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
                Assert.AreEqual("Index", ((RedirectToActionResult)result).ActionName);

                // Ensure the transaction is removed from the database
                Assert.AreEqual(0, context.Transactions.Count());
            }
        }


     
    }
}
