using System;
using System.Collections.Generic;
using TransactionCalculatorTest.BusinessLogic.Services;
using TransactionCalculatorTest.Model.Models;
using Xunit;
using System.Linq;
using TransactionCalculatorTest.BusinessLogic.Handlers;

namespace TransactionCalculatorTest.Tests
{
    public class TodayRequestDateHandlerTests
    {
        private readonly List<Transaction> _transactions;
        public TodayRequestDateHandlerTests()
        {
            _transactions = new List<Transaction>()
            {
                new Transaction()
                {
                    Amount = 1,
                    CreditDebitIndicator = TransactionType.Debit,
                    BookingDate = DateTime.Today.AddDays(-1)
                },
                new Transaction()
                {
                    Amount = 2,
                    CreditDebitIndicator = TransactionType.Debit,
                    BookingDate = DateTime.Today
                },
                new Transaction()
                {
                    Amount = 3,
                    CreditDebitIndicator = TransactionType.Debit,
                    BookingDate = DateTime.Today
                }
            };
        }

        [Fact]
        public void Handle_ShouldAddTodayTransactionsToYesterday_WhenRequestDateIsToday()
        {
            var dayTransactions = _transactions.GroupBy(t => t.BookingDate.Date).ToDictionary(t => t.Key, t => t.ToList());
            var ctx = new PreCalculationContext() { RequestDate = DateTime.Today, DayTransactions = dayTransactions };

            var handler = new TodayRequestDateHandler();
            handler.Handle(ctx);
            ctx.DayTransactions.TryGetValue(DateTime.Today.AddDays(-1), out var yesterdayTransactions);

            Assert.Equal(_transactions.Count, yesterdayTransactions.Count);
        }

        [Fact]
        public void Handle_ShouldNotDoAnyting_WhenRequestDateIsNotToday()
        {
            var dayTransactions = _transactions.GroupBy(t => t.BookingDate.Date).ToDictionary(t => t.Key, t => t.ToList());
            var ctx = new PreCalculationContext() { RequestDate = DateTime.Today.AddDays(-1), DayTransactions = dayTransactions };

            var handler = new TodayRequestDateHandler();
            handler.Handle(ctx);
            ctx.DayTransactions.TryGetValue(DateTime.Today.AddDays(-1), out var yesterdayTransactions);

            Assert.NotEqual(_transactions.Count, yesterdayTransactions.Count);
        }
    }
}
