using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementPrinterTests
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void test_statement_example()
        {
            var plays = new Dictionary<string, Play>();
            plays.Add("hamlet", new Play("Hamlet", PlayType.Tragedy));
            plays.Add("as-like", new Play("As You Like It", PlayType.Comedy));
            plays.Add("othello", new Play("Othello", PlayType.Tragedy));

            Invoice invoice = new Invoice("BigCo", new List<Performance>{new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40)});
            
            StatementPrinter statementPrinter = new StatementPrinter(invoice, plays);
            var result = statementPrinter.Print();

            Approvals.Verify(result);
        }
        [Fact]
        public void test_statement_with_new_play_types()
        {
            var plays = new Dictionary<string, Play>();
            plays.Add("henry-v", new Play("Henry V", PlayType.History));
            plays.Add("as-like", new Play("As You Like It", PlayType.Pastoral));

            Invoice invoice = new Invoice("BigCoII", new List<Performance>{new Performance("henry-v", 53),
                new Performance("as-like", 55)});
            
            StatementPrinter statementPrinter = new StatementPrinter(invoice, plays);

            Assert.Throws<Exception>(() => statementPrinter.Print());
        }
    }
}
