using System;
using System.Linq;
using TransactionCalculatorTest.Model.Models;

namespace TransactionCalculatorTest.BusinessLogic.Handlers
{
    public class TodayRequestDateHandler : BasePreCalculationRuleHandler
    {
        public override void Handle(PreCalculationContext context)
        {
            if (context.RequestDate.Date == DateTime.Today)
            {
                var yesterdayDate = context.RequestDate.AddDays(-1).Date;
                var dayTransactions = context.DayTransactions;

                if (context.DayTransactions.ContainsKey(context.RequestDate.Date) && context.DayTransactions.ContainsKey(yesterdayDate))
                {
                    var todayTransactions = dayTransactions[context.RequestDate.Date];
                    var yesterdayTransactions = dayTransactions[yesterdayDate];
                    yesterdayTransactions.AddRange(todayTransactions);
                    dayTransactions.Remove(context.RequestDate.Date);
                }
            }

            Next?.Handle(context);
        }
    }
}
