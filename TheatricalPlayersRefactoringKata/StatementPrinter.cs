using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);
           

            foreach(var performance in invoice.Performances)
            {
                var play = plays[performance.PlayID];
                var performanceAmount = CalculatePerfomanceAmount(performance, play.Type);

                // add volume credits
                volumeCredits += Math.Max(performance.Audience - 30, 0);

                // add extra credit for every ten comedy attendees
                if (PlayType.Comedy == play.Type) volumeCredits += (int)Math.Floor((decimal)performance.Audience / 5);

                // print line for this order
                result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(performanceAmount / 100), performance.Audience);
                totalAmount += performanceAmount;
            }
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }

        private int CalculatePerfomanceAmount(Performance performance, PlayType playType)
        {
            switch (playType)
            {
                case PlayType.Tragedy:
                    return CalculateTragedyPerformanceAmount(performance);
                case PlayType.Comedy:
                    return CalculateComedyPerformanceAmount(performance);
                default:
                    throw new Exception("unknown type: " + playType);
            }
        }

        private int CalculateComedyPerformanceAmount(Performance performance)
        {
            int amount = 30000;
            if (performance.Audience > 20)
            {
                amount += 10000 + 500 * (performance.Audience - 20);
            }
            amount += 300 * performance.Audience;
            return amount;
        }

        private int CalculateTragedyPerformanceAmount(Performance performance)
        {
            int amount = 40000;
            if (performance.Audience > 30)
            {
                amount += 1000 * (performance.Audience - 30);
            }

            return amount;
        }
    }
}
