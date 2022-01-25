using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransactionCalculatorTest.Model.Models
{
    public class Account
    {
        [Required]
        public string AccountId { get; set; }
        [Required]
        public Balance Balances { get; set; }
        [Required]
        public List<Transaction> Transactions { get; set; }
    }
}
