using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TransactionCalculatorTest.Model.Models
{
    public class CalculationRequest
    {
        [Required]
        public List<Account> Accounts { get; set; }
        public DateTime RequestDateTime { get; set; }
    }
}
