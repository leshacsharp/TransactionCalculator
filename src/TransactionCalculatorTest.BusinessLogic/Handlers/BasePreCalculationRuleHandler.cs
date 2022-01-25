using System;
using TransactionCalculatorTest.BusinessLogic.Interfaces;
using TransactionCalculatorTest.Model.Models;

namespace TransactionCalculatorTest.BusinessLogic.Handlers
{
    public abstract class BasePreCalculationRuleHandler : IPreCalculationRuleHandler
    {
        protected IPreCalculationRuleHandler Next { get; set; }

        public void SetNext(IPreCalculationRuleHandler handler)
        {
            Next = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public abstract void Handle(PreCalculationContext context);
    }
}
