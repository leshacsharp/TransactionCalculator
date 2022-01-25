using System;
using System.Collections.Generic;
using TransactionCalculatorTest.BusinessLogic.Interfaces;
using TransactionCalculatorTest.Model.Models;

namespace TransactionCalculatorTest.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly ITransactionService _transactionService;
        public AccountService(ITransactionService transactionService)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        public List<AccountInfo> GetAccountsInfo(CalculationRequest request)
        {
            var accountsInfo = new List<AccountInfo>();

            foreach (var account in request.Accounts)
            {
                var dayBalances = _transactionService.CalculateDayBalances(account.Balances.Current.Amount, request.RequestDateTime, account.Transactions);
                var accountTransactionsInfo = _transactionService.GetTransactionsInfo(account.Transactions);

                var accountInfo = new AccountInfo()
                {
                    AccountId = account.AccountId,
                    EndOfDayBalances = dayBalances,
                    TotalDebits = accountTransactionsInfo.TotalDebits,
                    TotalCredits = accountTransactionsInfo.TotalCredits
                };
                accountsInfo.Add(accountInfo);
            }

            return accountsInfo;
        }
    }
}
