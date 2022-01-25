using System.ComponentModel.DataAnnotations;

namespace TransactionCalculatorTest.Model.Models
{
    public class Balance
    {
        [Required]
        public BalanceInfo Current { get; set; }
        [Required]
        public BalanceInfo Available { get; set; }
    }
}
