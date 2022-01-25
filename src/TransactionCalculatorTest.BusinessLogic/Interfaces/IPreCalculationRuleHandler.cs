using TransactionCalculatorTest.Model.Models;

namespace TransactionCalculatorTest.BusinessLogic.Interfaces
{
    public interface IPreCalculationRuleHandler
    {
        void SetNext(IPreCalculationRuleHandler handler);
        void Handle(PreCalculationContext context);
    }
}
