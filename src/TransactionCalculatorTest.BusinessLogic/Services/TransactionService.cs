using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionCalculatorTest.BusinessLogic.Interfaces;
using TransactionCalculatorTest.Model.Models;

namespace TransactionCalculatorTest.BusinessLogic.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IPreCalculationRuleFactory _preCalculationRuleFactory;
        public TransactionService(IPreCalculationRuleFactory preCalculationRuleFactory)
        {
            _preCalculationRuleFactory = preCalculationRuleFactory ?? throw new ArgumentNullException(nameof(preCalculationRuleFactory));
        }

        public List<DayBalance> CalculateDayBalances(decimal initBalance, DateTime requestDate, List<Transaction> transactions)
        {
            //Supposed that can be many rules for the pre-calculation process in the future.
            //By this assumption the pre-calculation rules process was made.
            //In the task specification there is only one specific rule - requestDate==today => yesterday's balance is calculated by today's + yesterday's transactions.

            var dayTransactions = transactions.GroupBy(t => t.BookingDate.Date).ToDictionary(t => t.Key, t => t.ToList());   
            var preCalculationCtx = new PreCalculationContext() { RequestDate = requestDate, DayTransactions = dayTransactions };
            var preCalculationRules = _preCalculationRuleFactory.CreateChainOfAllRules();
            preCalculationRules?.Handle(preCalculationCtx);

            var dayBalances = new List<DayBalance>();
            var currentBalance = initBalance;
            var readyDayTransactions = preCalculationCtx.DayTransactions.OrderByDescending(d => d.Key);

            foreach (var dayTransactionsGroup in readyDayTransactions)
            {
                foreach (var transaction in dayTransactionsGroup.Value)
                {
                    switch (transaction.CreditDebitIndicator)
                    {
                        case TransactionType.Debit:
                            currentBalance -= transaction.Amount; 
                            break;

                        case TransactionType.Credit: 
                            currentBalance += transaction.Amount;
                            break;

                        default:
                            throw new NotSupportedException(transaction.CreditDebitIndicator.ToString());
                    }
                }

                var dayBalance = new DayBalance()
                {
                    Date = dayTransactionsGroup.Key,
                    Amount = currentBalance
                };
                dayBalances.Add(dayBalance);
            }

            return dayBalances;
        }

        public TransactionsInfo GetTransactionsInfo(List<Transaction> transactions)
        {
            var info = new TransactionsInfo();

            foreach (var transaction in transactions)
            {
                switch (transaction.CreditDebitIndicator)
                {
                    case TransactionType.Debit: 
                        info.TotalDebits += transaction.Amount; 
                        break;

                    case TransactionType.Credit: 
                        info.TotalCredits += transaction.Amount;
                        break;

                    default:
                        throw new NotSupportedException(transaction.CreditDebitIndicator.ToString());
                }
            }

            return info;
        }
    }
}
