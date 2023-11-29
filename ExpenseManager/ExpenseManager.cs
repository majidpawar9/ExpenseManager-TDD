using System;

namespace Expense.Services
{
    public class ExpenseManager
    {
        public bool IsPrime(int candidate)
        {
            if (candidate == 1)
            {
                return false;
            }
            throw new NotImplementedException("Please create a test first.");
        }

        public bool DivideBy2(int x)
        {
            if ((x % 2) == 0)
            {
                return true;
            }
            return false;
        }
    }
}