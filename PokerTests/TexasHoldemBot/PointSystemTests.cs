using System;
using NUnit.Framework;
using TexasHoldemBot.Ai;
using TexasHoldemBot.Poker;

namespace PokerTests.TexasHoldemBot
{
    [TestFixture]
    public class PointSystemTests
    {
        [Test]
        public void TestConnected()
        {
            Assert.IsTrue(Cards.C_2C.IsConnected(Cards.C_AC));
            Assert.IsTrue(Cards.C_2C.IsConnected(Cards.C_3D));
            Assert.IsTrue(Cards.C_3H.IsConnected(Cards.C_2C));
            Assert.IsTrue(Cards.C_3H.IsConnected(Cards.C_4D));
            Assert.IsTrue(Cards.C_4D.IsConnected(Cards.C_3S));
            Assert.IsTrue(Cards.C_4D.IsConnected(Cards.C_5H));
            Assert.IsTrue(Cards.C_5S.IsConnected(Cards.C_4S));
            Assert.IsTrue(Cards.C_5S.IsConnected(Cards.C_6H));
            Assert.IsTrue(Cards.C_6C.IsConnected(Cards.C_5S));
            Assert.IsTrue(Cards.C_6C.IsConnected(Cards.C_7H));
            Assert.IsTrue(Cards.C_7C.IsConnected(Cards.C_6S));
            Assert.IsTrue(Cards.C_7C.IsConnected(Cards.C_8H));
            Assert.IsTrue(Cards.C_8C.IsConnected(Cards.C_7S));
            Assert.IsTrue(Cards.C_8C.IsConnected(Cards.C_9H));
            Assert.IsTrue(Cards.C_9C.IsConnected(Cards.C_8S));
            Assert.IsTrue(Cards.C_9C.IsConnected(Cards.C_TH));
            Assert.IsTrue(Cards.C_TC.IsConnected(Cards.C_9S));
            Assert.IsTrue(Cards.C_TC.IsConnected(Cards.C_JH));
            Assert.IsTrue(Cards.C_JC.IsConnected(Cards.C_TS));
            Assert.IsTrue(Cards.C_JC.IsConnected(Cards.C_QH));
            Assert.IsTrue(Cards.C_QC.IsConnected(Cards.C_JS));
            Assert.IsTrue(Cards.C_QC.IsConnected(Cards.C_KH));
            Assert.IsTrue(Cards.C_KC.IsConnected(Cards.C_QS));
            Assert.IsTrue(Cards.C_KC.IsConnected(Cards.C_AH));
            Assert.IsTrue(Cards.C_AC.IsConnected(Cards.C_KS));
            Assert.IsTrue(Cards.C_AC.IsConnected(Cards.C_2H));
        }

        [Test]
        public void HolePointsTest()
        {
            TestHolePoints(Cards.C_AC, Cards.C_AS, 60);
            TestHolePoints(Cards.C_AD, Cards.C_AH, 60);
            TestHolePoints(Cards.C_AD, Cards.C_KD, 45);
            TestHolePoints(Cards.C_AD, Cards.C_KC, 35);
            TestHolePoints(Cards.C_AD, Cards.C_QD, 35);
            TestHolePoints(Cards.C_AD, Cards.C_QC, 25);
            TestHolePoints(Cards.C_AH, Cards.C_JH, 30);
            TestHolePoints(Cards.C_AH, Cards.C_JS, 20);
            TestHolePoints(Cards.C_AH, Cards.C_TH, 30);
            TestHolePoints(Cards.C_AH, Cards.C_TS, 20);
            TestHolePoints(Cards.C_AH, Cards.C_9H, 25);
            TestHolePoints(Cards.C_AH, Cards.C_9S, 15);
            TestHolePoints(Cards.C_AH, Cards.C_8H, 25);
            TestHolePoints(Cards.C_AH, Cards.C_8S, 15);
            TestHolePoints(Cards.C_AH, Cards.C_7H, 25);
            TestHolePoints(Cards.C_AH, Cards.C_7S, 15);
            TestHolePoints(Cards.C_AH, Cards.C_6H, 25);
            TestHolePoints(Cards.C_AH, Cards.C_6S, 15);
            TestHolePoints(Cards.C_AH, Cards.C_5H, 25);
            TestHolePoints(Cards.C_AH, Cards.C_5S, 15);
            TestHolePoints(Cards.C_AH, Cards.C_4H, 25);
            TestHolePoints(Cards.C_AH, Cards.C_4S, 15);
            TestHolePoints(Cards.C_AH, Cards.C_3H, 25);
            TestHolePoints(Cards.C_AH, Cards.C_3S, 15);
            TestHolePoints(Cards.C_AH, Cards.C_2H, 35);
            TestHolePoints(Cards.C_AH, Cards.C_2S, 25);

            TestHolePoints(Cards.C_2D, Cards.C_4C, 0);
            TestHolePoints(Cards.C_3D, Cards.C_5C, 0);
            TestHolePoints(Cards.C_4D, Cards.C_9H, 0);
            TestHolePoints(Cards.C_5D, Cards.C_9H, 0);
            TestHolePoints(Cards.C_6D, Cards.C_9H, 0);
            TestHolePoints(Cards.C_7D, Cards.C_9H, 0);
            TestHolePoints(Cards.C_8D, Cards.C_2H, 0);
            TestHolePoints(Cards.C_9D, Cards.C_3H, 0);

            TestHolePoints(Cards.C_2C, Cards.C_2D, 30);
            TestHolePoints(Cards.C_3C, Cards.C_3D, 30);
            TestHolePoints(Cards.C_2C, Cards.C_3D, 10);
            TestHolePoints(Cards.C_JD, Cards.C_JC, 40);
            TestHolePoints(Cards.C_TD, Cards.C_TC, 40);

            TestHolePoints(Cards.C_JD, Cards.C_TD, 30);
            TestHolePoints(Cards.C_JD, Cards.C_TC, 20);
        }

        private void TestHolePoints(Card c1, Card c2, int expectedScore)
        {
            int actualScore = HoldemPointSystem.HoleCardPoints(new[] {c1, c2});
            Console.WriteLine($"[{c1}, {c2}] = {actualScore}");
            Assert.AreEqual(expectedScore, actualScore);
        }

    }
}