using System;

namespace TransactionCalculatorTest.Model.Models
{
    public class Transaction
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public TransactionType CreditDebitIndicator { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
