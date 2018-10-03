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
    }
}