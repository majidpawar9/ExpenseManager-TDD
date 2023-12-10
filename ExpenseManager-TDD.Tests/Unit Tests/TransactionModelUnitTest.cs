using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseManager_TDD.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpenseManager_TDD.Tests.Unit_Tests
{
    [TestClass]
    public class TransactionModelTests
    {
        [TestMethod]
        public void TransactionID_ShouldHaveKeyAttribute()
        {
            // Arrange
            var property = typeof(Transaction).GetProperty("TransactionID");

            // Act
            var keyAttribute = Attribute.GetCustomAttribute(property, typeof(KeyAttribute)) as KeyAttribute;

            // Assert
            Assert.IsNotNull(keyAttribute, "TransactionID should have the KeyAttribute.");
        }

        [TestMethod]
        public void CategoryID_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(Transaction).GetProperty("CategoryID");

            // Act
            var rangeAttribute = Attribute.GetCustomAttribute(property, typeof(RangeAttribute)) as RangeAttribute;

            // Assert
            Assert.IsNotNull(rangeAttribute, "CategoryID should have the RangeAttribute.");
            Assert.AreEqual(1, rangeAttribute.Minimum, "CategoryID should have a minimum value of 1.");
            Assert.AreEqual(int.MaxValue, rangeAttribute.Maximum, "CategoryID should have a maximum value of int.MaxValue.");
        }

        [TestMethod]
        public void Amount_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(Transaction).GetProperty("Amount");

            // Act
            var rangeAttribute = Attribute.GetCustomAttribute(property, typeof(RangeAttribute)) as RangeAttribute;

            // Assert
            Assert.IsNotNull(rangeAttribute, "Amount should have the RangeAttribute.");
            Assert.AreEqual(1, rangeAttribute.Minimum, "Amount should have a minimum value of 1.");
            Assert.AreEqual(int.MaxValue, rangeAttribute.Maximum, "Amount should have a maximum value of int.MaxValue.");
        }

        [TestMethod]
        public void Note_ShouldHaveColumnAttributeWithTypeNVarChar()
        {
            // Arrange
            var property = typeof(Transaction).GetProperty("Note");

            // Act
            var columnAttribute = Attribute.GetCustomAttribute(property, typeof(ColumnAttribute)) as ColumnAttribute;

            // Assert
            Assert.IsNotNull(columnAttribute, "Note should have the ColumnAttribute.");
            Assert.AreEqual("nvarchar(75)", columnAttribute.TypeName, "Note should have a TypeName of nvarchar(75).");
        }

        [TestMethod]
        public void Date_ShouldHaveDefaultValueOfDateTimeNow()
        {
            // Arrange
            var property = typeof(Transaction).GetProperty("Date");

            // Act
            var defaultValue = property.GetValue(new Transaction());

            // Assert
            Assert.IsNotNull(defaultValue, "Date should have a default value.");
            Assert.AreEqual(DateTime.Now.Date, ((DateTime)defaultValue).Date, "Date should have a default value of DateTime.Now.");
        }

        [TestMethod]
        public void CategoryTitleWithIcon_ShouldReturnCorrectValue()
        {
            // Arrange
            var transaction = new Transaction { Category = new Category { Icon = "TestIcon", Title = "TestCategory" } };

            // Act
            var result = transaction.CategoryTitleWithIcon;

            // Assert
            Assert.AreEqual("TestIcon TestCategory", result, "CategoryTitleWithIcon should return the correct value.");
        }

        [TestMethod]
        public void FormattedAmount_ShouldReturnCorrectValueForExpense()
        {
            // Arrange
            var transaction = new Transaction { Amount = 100, Category = new Category { Type = "Expense" } };

            // Act
            var result = transaction.FormattedAmount;

            // Assert
            Assert.AreEqual("- $100", result, "FormattedAmount should return the correct value for Expense.");
        }

        [TestMethod]
        public void FormattedAmount_ShouldReturnCorrectValueForIncome()
        {
            // Arrange
            var transaction = new Transaction { Amount = 100, Category = new Category { Type = "Income" } };

            // Act
            var result = transaction.FormattedAmount;

            // Assert
            Assert.AreEqual("+ $100", result, "FormattedAmount should return the correct value for Income.");
        }
    }
}
