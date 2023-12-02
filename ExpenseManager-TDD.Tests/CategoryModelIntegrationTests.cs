using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpenseManager_TDD.Models;

namespace ExpenseManager_TDD.Tests
{
    [TestClass]
    public class CategoryModelIntegrationTests
    {
        [TestMethod]
        public void Category_Model_Should_Pass_DataAnnotations_Validation()
        {
          
            var category = new Category
            {
                CategoryID = 1,
                Title = "Test Category",
                Icon = "Icon",
                Type = "Expense"
            };

           
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(category);
            var validationResults = new System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(category, validationContext, validationResults, true);

           
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void Category_Model_Should_Be_Created_In_Database()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var category = new Category
                {
                    CategoryID = 1,
                    Title = "Test Category",
                    Icon = "Icon",
                    Type = "Expense"
                };

                
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
            }

            
            using (var dbContext = new ApplicationDbContext(options))
            {
                var retrievedCategory = dbContext.Categories.Find(1);

                Assert.IsNotNull(retrievedCategory);
                Assert.AreEqual("Test Category", retrievedCategory.Title);
                
            }
        }

        [TestMethod]
        public void Category_Model_Should_Fail_If_Title_Is_Missing()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var category = new Category
                {
                    CategoryID = 1,
                    // We are not setting the title here purposely
                    Icon = "Icon",
                    Type = "Expense"
                };

                Assert.ThrowsException<DbUpdateException>(() => {
                    dbContext.Categories.Add(category);
                    dbContext.SaveChanges();
                });
            }
        }
    }
}
