using System;
using System.Collections.Generic;

namespace TransactionCalculatorTest.Model.Models
{
    public class PreCalculationContext
    {
        public DateTime RequestDate { get; set; }
        public Dictionary<DateTime, List<Transaction>> DayTransactions { get; set; }
    }
}
