using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using TexasHoldemBot;
using TexasHoldemBot.Ai;
using TexasHoldemBot.Poker;

namespace PokerTests.TexasHoldemBot
{
    [TestFixture]
    public class BotMoveTest
    {
        [Test]
        public void MoveTest1()
        {
            StringBuilder sOutput = new StringBuilder();
            StringBuilder sError = new StringBuilder();
            StringWriter wOutput = new StringWriter(sOutput);
            StringWriter wError = new StringWriter(sError);

            BotIo.SetIn(TestScripts.GetReader(TestScripts.SCRIPT1_S));
            BotIo.SetOut(wOutput);
            BotIo.SetLog(wError);

            HoldemBot bot = new HoldemBot(new PointCountBrain(new PokerHandEvaluator()));
            bot.Run();
            Console.WriteLine($"OUTPUT:\n{sOutput}");
            Console.WriteLine($"\n\nLOG:\n{sError}");
        }
        
        /// <summary>
        /// This test immortalizes on of the best hands my bot ever played,
        /// and yet it generated a stack trace error in the middle of the
        /// log file for it. So it went all in and a straight flush and won,
        /// but I also got the replay so I could fix the error.
        /// </summary>
        [Test]
        public void MoveTest2()
        {
            RunTest(TestScripts.SCRIPT2_S);
        }


        public void RunTest(string script)
        {
            StringBuilder sOutput = new StringBuilder();
            StringBuilder sError = new StringBuilder();
            StringWriter wOutput = new StringWriter(sOutput);
            StringWriter wError = new StringWriter(sError);

            BotIo.SetIn(TestScripts.GetReader(script));
            BotIo.SetOut(wOutput);
            BotIo.SetLog(wError);

            HoldemBot bot = new HoldemBot(new PointCountBrain(new PokerHandEvaluator()));
            bot.Run();
            Console.WriteLine($"OUTPUT:\n{sOutput}");
            Console.WriteLine($"\n\nLOG:\n{sError}");
        }
    }
}