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
            var result = GetStatementHeader(invoice);
           
            foreach(var performance in invoice.Performances)
            {
                var play = plays[performance.PlayID];
                var performanceAmount = CalculatePerfomanceAmount(performance, play.Type);
                volumeCredits = GetVolumeCredits(volumeCredits, performance, play);

                result = GetLineItemDetail(result, performance, play, performanceAmount);

                totalAmount += performanceAmount;
            }
            result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }

        private int GetVolumeCredits(int volumeCredits, Performance performance, Play play)
        {
            volumeCredits += GetAdditionalVolumeCredits(performance);

            if (PlayType.Comedy == play.Type)
            {
                volumeCredits += GetAdditionalComedyVolumeCredits(performance);
            }

            return volumeCredits;
        }

        private string GetLineItemDetail(string result, Performance performance, Play play, int performanceAmount)
        {
            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(performanceAmount / 100), performance.Audience);
            return result;
        }

        private string GetStatementHeader(Invoice invoice)
        {
            return string.Format("Statement for {0}\n", invoice.Customer);
        }
        private int GetAdditionalComedyVolumeCredits(Performance performance)
        {
            return (int)Math.Floor((decimal)performance.Audience / 5);
        }
        private int GetAdditionalVolumeCredits(Performance performance)
        {
            return Math.Max(performance.Audience - 30, 0);
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
                    throw new Exception("Unsurported Type: " + playType);
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
