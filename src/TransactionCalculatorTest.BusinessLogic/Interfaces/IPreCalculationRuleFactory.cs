namespace TransactionCalculatorTest.BusinessLogic.Interfaces
{
    public interface IPreCalculationRuleFactory
    {
        IPreCalculationRuleHandler CreateChainOfAllRules();
    }
}
