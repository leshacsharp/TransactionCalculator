using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionCalculatorTest.Model.Models;

namespace TransactionCalculatorTest.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        List<AccountInfo> GetAccountsInfo(CalculationRequest request);
    }
}
