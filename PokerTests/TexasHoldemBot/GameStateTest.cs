using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemBot;
using TexasHoldemBot.Ai;
using TexasHoldemBot.Poker;

namespace PokerTests.TexasHoldemBot
{
    [TestFixture]
    public class GameStateTest
    {
        [Test]
        public void TestInitialGameState()
        {
            HoldemBot bot = new HoldemBot(new DumbBrain(new PokerHandEvaluator()));            
        }
    }
}
