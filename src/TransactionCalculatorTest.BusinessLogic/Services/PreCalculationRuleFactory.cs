using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionCalculatorTest.BusinessLogic.Interfaces;

namespace TransactionCalculatorTest.BusinessLogic.Services
{
    public class PreCalculationRuleFactory : IPreCalculationRuleFactory
    {
        private readonly IEnumerable<IPreCalculationRuleHandler> _ruleHandlers;
        public PreCalculationRuleFactory(IEnumerable<IPreCalculationRuleHandler> ruleHandlers)
        {
            _ruleHandlers = ruleHandlers;
        }

        public IPreCalculationRuleHandler CreateChainOfAllRules()
        {
            var handlers = _ruleHandlers.ToList();

            for (int i = 0; i < handlers.Count - 1; i++)
            {
                handlers[i].SetNext(handlers[i + 1]);
            }

            return handlers.FirstOrDefault();
        }
    }
}
