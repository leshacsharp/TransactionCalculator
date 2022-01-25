using System;
using System.Collections.Generic;
using TransactionCalculatorTest.BusinessLogic.Services;
using TransactionCalculatorTest.Model.Models;
using Xunit;
using System.Linq;
using Moq;
using TransactionCalculatorTest.BusinessLogic.Interfaces;

namespace TransactionCalculatorTest.Tests
{
    public class TransactionServiceTests
    {
        private readonly List<Transaction> _transactions;
        public TransactionServiceTests()
        {
            _transactions = new List<Transaction>()
            {
                new Transaction()
                { 
                    Amount = 1,
                    CreditDebitIndicator = TransactionType.Credit,
                    BookingDate = new DateTime(2022, 1, 1)
                },
                new Transaction()
                {
                    Amount = 1,
                    CreditDebitIndicator = TransactionType.Credit,
                    BookingDate = new DateTime(2022, 1, 2)
                },
                new Transaction()
                {
                    Amount = 1,
                    CreditDebitIndicator = TransactionType.Debit,
                    BookingDate = new DateTime(2022, 1, 2)
                }
            };
        }

        [Fact]
        public void GetTransactionsInfo_ShouldReturnInfo_WhenRecieveTransactions()
        {
            var ruleFactoryMock = new Mock<IPreCalculationRuleFactory>();
            var transactionService = new TransactionService(ruleFactoryMock.Object);

            var transactionsInfo = transactionService.GetTransactionsInfo(_transactions);

            const int expectedTotalDebits = 1;
            const int expectedTotalCredits = 2;
            Assert.Equal(expectedTotalDebits, transactionsInfo.TotalDebits);
            Assert.Equal(expectedTotalCredits, transactionsInfo.TotalCredits);
        }

        [Fact]
        public void CalculateDayBalances_ShouldReturnAllDaysBalances()
        {
            var ruleFactoryMock = new Mock<IPreCalculationRuleFactory>();
            var transactionService = new TransactionService(ruleFactoryMock.Object);

            var dayBalances = transactionService.CalculateDayBalances(0, DateTime.Today, _transactions);
            var firstDayBalance = dayBalances.Single(b => b.Date == new DateTime(2022, 1, 1));     

            const int expectedDayBalancesCount = 2;
            const int expectedFirstDayAmount = 1;
            Assert.Equal(expectedDayBalancesCount, dayBalances.Count);
            Assert.Equal(expectedFirstDayAmount, firstDayBalance.Amount);
        }
    }
}
