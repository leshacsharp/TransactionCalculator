using System;
using System.Collections.Generic;
using TransactionCalculatorTest.Model.Models;

namespace TransactionCalculatorTest.BusinessLogic.Interfaces
{
    public interface ITransactionService
    {
        List<DayBalance> CalculateDayBalances(decimal initBalance, DateTime requestDate, List<Transaction> transactions);
        TransactionsInfo GetTransactionsInfo(List<Transaction> transactions);
    }
}
