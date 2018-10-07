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

            /*** Suited connected cards < 10 should be 20 ***/
            TestHolePoints(Card.C9, Card.C8, 20 );
        }

        private void TestHolePoints(Card c1, Card c2, int expectedScore)
        {
            int actualScore = HoldemPointSystem.HoleCardPoints(new[] {c1, c2});
            Console.WriteLine($"[{c1}, {c2}] = {actualScore}");
            Assert.AreEqual(expectedScore, actualScore);
        }

        [Test]
        public void IsOpenEndedStraightTest()
        {
            Assert.IsTrue(HoldemPointSystem.IsOpenEndedStraightDraw(new []{Card.C3, Card.C4},new []{Card.D5, Card.H6, Card.HK}));
            Assert.IsTrue(HoldemPointSystem.IsOpenEndedStraightDraw(new[] { Card.C3, Card.C6 }, new[] { Card.D5, Card.H4, Card.HK }));
            Assert.IsTrue(HoldemPointSystem.IsOpenEndedStraightDraw(new[] { Card.C8, Card.D6 }, new[] { Card.D7, Card.H5, Card.HK }));
            Assert.IsFalse(HoldemPointSystem.IsOpenEndedStraightDraw(new[] { Card.C8, Card.D6 }, new[] { Card.D7, Card.H4, Card.HK }));
            Assert.IsFalse(HoldemPointSystem.IsOpenEndedStraightDraw(new[] { Card.CA, Card.D2 }, new[] { Card.D3, Card.H4, Card.HK }));
            Assert.IsFalse(HoldemPointSystem.IsOpenEndedStraightDraw(new[] { Card.CA, Card.DK }, new[] { Card.D3, Card.HJ, Card.HQ }));
        }

        [Test]
        public void FlopPointsTest()
        {
            /*** POCKET PAIRS TESTS ***/
            TestFlopPoints(Card.CA, Card.HA, new[] { Card.DA, Card.H7, Card.C2 }, 120);
            TestFlopPoints(Card.CK, Card.HK, new[] { Card.DK, Card.H7, Card.C2 }, 100);
            TestFlopPoints(Card.CQ, Card.HQ, new[] { Card.DQ, Card.H7, Card.C2 }, 100);
            TestFlopPoints(Card.CJ, Card.HJ, new[] { Card.DJ, Card.H7, Card.C2 }, 80);
            TestFlopPoints(Card.CT, Card.HT, new[] { Card.DT, Card.H7, Card.C2 }, 80);
            TestFlopPoints(Card.C9, Card.H9, new[] { Card.D9, Card.H7, Card.C2 }, 60);
            TestFlopPoints(Card.C8, Card.H8, new[] { Card.D8, Card.H7, Card.C2 }, 60);

            /*** One community card matches hole card ***/
            TestFlopPoints(Card.DA, Cards.C_KD, new[] { Card.HA, Card.S7, Card.C2 }, 60);
            TestFlopPoints(Card.DA, Cards.C_KD, new[] { Card.HK, Card.S7, Card.C2 }, 55);
            TestFlopPoints(Card.HA, Cards.C_KD, new[] { Card.HA, Card.S7, Card.C2 }, 50);
            TestFlopPoints(Card.HA, Cards.C_KD, new[] { Card.HK, Card.S7, Card.C2 }, 45);

            TestFlopPoints(Card.DA, Cards.C_QD, new[] { Card.HA, Card.S7, Card.C2 }, 50);
            TestFlopPoints(Card.DA, Cards.C_QD, new[] { Card.HQ, Card.S7, Card.C2 }, 45);
            TestFlopPoints(Card.HA, Cards.C_QD, new[] { Card.HA, Card.S7, Card.C2 }, 40);
            TestFlopPoints(Card.HA, Cards.C_QD, new[] { Card.HQ, Card.S7, Card.C2 }, 35);

            /*** Other hole suited/connected combinations (9 or lower) ***/
            TestFlopPoints(Card.D9, Card.D8, new[] { Card.H9, Card.S7, Card.C2 }, 25);
            TestFlopPoints(Card.D9, Card.D8, new[] { Card.H8, Card.S7, Card.C2 }, 25);

            /*** One of the community cards is same suit as hole suited cards **/
            TestFlopPoints(Card.DA, Card.DK, new[] { Card.D8, Card.S7, Card.C2 }, 55);
            TestFlopPoints(Card.DA, Card.DQ, new[] { Card.D8, Card.S7, Card.C2 }, 45);
            TestFlopPoints(Card.DA, Card.DJ, new[] { Card.D8, Card.S7, Card.C2 }, 40);
            TestFlopPoints(Card.DA, Card.DT, new[] { Card.D8, Card.S7, Card.C2 }, 40);
            
            TestFlopPoints(Card.DK, Card.DQ, new[] { Card.D8, Card.S7, Card.C2 }, 50);
            TestFlopPoints(Card.DQ, Card.DJ, new[] { Card.D8, Card.S7, Card.C2 }, 45);
            TestFlopPoints(Card.DJ, Card.DT, new[] { Card.D8, Card.S7, Card.C2 }, 40);

            TestFlopPoints(Card.D8, Card.D9, new[] { Card.D2, Card.S7, Card.C3 }, 30);

            /*** Flop includes pair ***/
            TestFlopPoints(Card.D8, Card.D9, new[] { Card.C8, Card.S8, Card.H3 }, 50);
            TestFlopPoints(Card.D8, Card.D9, new[] { Card.C7, Card.S7, Card.H3 }, 10);

            /*** Flop included Three suited cards ***/
            TestFlopPoints(Card.D8, Card.D9, new[] { Card.DA, Card.DK, Card.D2 }, 50);
            TestFlopPoints(Card.DT, Card.DJ, new[] { Card.DA, Card.DK, Card.D2 }, 60);
            TestFlopPoints(Card.DT, Card.HJ, new[] { Card.DA, Card.DK, Card.D2 }, 30);
            TestFlopPoints(Card.DA, Card.HJ, new[] { Card.DT, Card.DK, Card.D2 }, 40);
            TestFlopPoints(Card.D8, Card.D9, new[] { Card.CA, Card.CK, Card.C2 }, 0);

            /*** Flop includes two suited cards ***/
            TestFlopPoints(Card.D8, Card.D9, new[] { Card.DA, Card.DK, Card.C2 }, 40);
            TestFlopPoints(Card.DT, Card.H9, new[] { Card.DA, Card.DK, Card.C2 }, 5);
            TestFlopPoints(Card.D8, Card.H9, new[] { Card.DA, Card.DK, Card.C2 }, 0);
            TestFlopPoints(Card.H8, Card.H9, new[] { Card.DA, Card.DK, Card.C2 }, 10);
        }

        private void TestFlopPoints(Card c1, Card c2, Card[] flop, int expectedScore)
        {
            int actualScore = HoldemPointSystem.PostFlopStrength(new[] {c1, c2}, flop);
            Console.WriteLine($"Hole = [{c1}, {c2}]  Table = {new Hand(flop)}; Adjusted Score = {actualScore}");
            Assert.AreEqual(expectedScore, actualScore);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void AfterFlopAdjustmentTest()
        {
            // two pairs
            Console.WriteLine("Pocket Pairs:");
            PrintPoints(Card.CA, Card.DA, new [] {Card.CK, Card.SK, Card.D2});
            PrintPoints(Card.CA, Card.DA, new[] { Card.CQ, Card.SQ, Card.D2 });
            PrintPoints(Card.C5, Card.D5, new[] { Card.CK, Card.SK, Card.D2 });
            PrintPoints(Card.C5, Card.D5, new[] { Card.CK, Card.SK, Card.D2, Card.D4});

            Console.WriteLine("\nTwo pairs");
            PrintPoints(Card.C5, Card.C6, new[] { Card.D5, Card.S6, Card.D2 });
            PrintPoints(Card.CA, Card.CK, new[] { Card.DA, Card.SK, Card.D2 });

        }
        private void PrintPoints(Card c1, Card c2, Card[] flop)
        {
            int actualScore = HoldemPointSystem.PostFlopStrength(new[] { c1, c2 }, flop);
            Console.WriteLine($"Hole = [{c1}, {c2}]  Table = {new Hand(flop)}; Adjusted Score = {actualScore}");            
        }

    }
}