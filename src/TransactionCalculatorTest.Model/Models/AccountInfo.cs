using System.Collections.Generic;

namespace TransactionCalculatorTest.Model.Models
{
    public class AccountInfo
    {
        public string AccountId { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal TotalDebits { get; set; }
        public List<DayBalance> EndOfDayBalances { get; set; }
    }
}
