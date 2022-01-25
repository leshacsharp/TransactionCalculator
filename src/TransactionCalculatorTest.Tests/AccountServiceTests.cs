using Moq;
using System.Collections.Generic;
using TransactionCalculatorTest.BusinessLogic.Services;
using TransactionCalculatorTest.BusinessLogic.Interfaces;
using TransactionCalculatorTest.Model.Models;
using Xunit;

namespace TransactionCalculatorTest.Tests
{
    public class AccountServiceTests
    {
        [Fact]
        public void GetAccountsInfo_ShouldReturnInfo_WhenRecieveValidRequest()
        {
            var serviceMock = new Mock<ITransactionService>();
            serviceMock.Setup(s => s.GetTransactionsInfo(It.IsAny<List<Transaction>>())).Returns(new TransactionsInfo());
            var accountService = new AccountService(serviceMock.Object);

            var request = new CalculationRequest()
            {
                Accounts = new List<Account>()
                {
                    new Account() { Balances = new Balance() { Current = new BalanceInfo() } },
                    new Account() { Balances = new Balance() { Current = new BalanceInfo() } }
                }
            };

            var accountsInfo = accountService.GetAccountsInfo(request);

            Assert.Equal(request.Accounts.Count, accountsInfo.Count);
        }
    }
}
