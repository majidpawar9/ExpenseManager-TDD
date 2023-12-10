using ExpenseManager_TDD.Controllers;
using ExpenseManager_TDD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager_TDD.Tests.Unit_Tests
{
    [TestClass]
    public class DashboardControllerTests
    {
        private readonly ApplicationDbContext _dbContext;

        public DashboardControllerTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            _dbContext = new ApplicationDbContext(options);
        }

        [TestMethod]
        public async Task Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new DashboardController(_dbContext);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Index_PopulatesViewData()
        {
            // Arrange
            var controller = new DashboardController(_dbContext);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData["TotalIncome"]);
            Assert.IsNotNull(result.ViewData["TotalExpense"]);
            Assert.IsNotNull(result.ViewData["Balance"]);
            Assert.IsNotNull(result.ViewData["RecentTransactions"]);
        }

        [TestMethod]
        public async Task Index_CalculatesBalance()
        {
            // Arrange
            var controller = new DashboardController(_dbContext);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData["Balance"]);

            // Balance should be zero in this test scenario as we don't have specific transactions
            Assert.AreEqual(0, Convert.ToDecimal(result.ViewData["Balance"]));
        }

        [TestMethod]
        public async Task Index_RetrievesRecentTransactions()
        {
            // Arrange
            // Add some sample transactions to the in-memory database
            _dbContext.Transactions.AddRange(
                new Transaction { Date = DateTime.Now.AddDays(-1), Amount = 100, CategoryID = 1 },
                new Transaction { Date = DateTime.Now.AddDays(-2), Amount = 200, CategoryID = 2 },
                new Transaction { Date = DateTime.Now.AddDays(-3), Amount = 300, CategoryID = 1 }
            );
            await _dbContext.SaveChangesAsync();

            var controller = new DashboardController(_dbContext);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData["RecentTransactions"]);

            var recentTransactions = result.ViewData["RecentTransactions"] as List<Transaction>;
            Assert.IsNotNull(recentTransactions);
            Assert.AreEqual(3, recentTransactions.Count); // Assuming you added 3 transactions
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Dispose();
        }
    }
}
