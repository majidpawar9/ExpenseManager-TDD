using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expense.Services;

namespace Expense.Tests.Services
{
    [TestClass]
    public class ExpenseManager_IsPrimeShould
    {
        private readonly ExpenseManager _expenseservice;

        public ExpenseManager_IsPrimeShould()
        {
            _expenseservice = new ExpenseManager();
        }

        [TestMethod]
        public void IsPrime_InputIs1_ReturnFalse()
        {
            bool result = _expenseservice.IsPrime(1);

            Assert.IsFalse(result, "1 should not be prime");
        }
    }
}