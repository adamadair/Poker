using System;
using NUnit.Framework;
using TexasHoldemBot.Poker;

namespace PokerTests.TexasHoldemBot
{
    [TestFixture]
    public class BrecherHandEvaluatorTests
    {
        static Card C_2S = Card.Parse("2S");
        static Card C_3S = Card.Parse("3S");
        static Card C_4S = Card.Parse("4S");
        static Card C_5S = Card.Parse("5S");
        static Card C_6S = Card.Parse("6S");
        static Card C_7S = Card.Parse("7S");
        static Card C_8S = Card.Parse("8S");
        static Card C_9S = Card.Parse("9S");
        static Card C_TS = Card.Parse("TS");
        static Card C_JS = Card.Parse("JS");
        static Card C_QS = Card.Parse("QS");
        static Card C_KS = Card.Parse("KS");
        static Card C_AS = Card.Parse("AS");
        static Card C_2H = Card.Parse("2H");
        static Card C_3H = Card.Parse("3H");
        static Card C_4H = Card.Parse("4H");
        static Card C_5H = Card.Parse("5H");
        static Card C_6H = Card.Parse("6H");
        static Card C_7H = Card.Parse("7H");
        static Card C_8H = Card.Parse("8H");
        static Card C_9H = Card.Parse("9H");
        static Card C_TH = Card.Parse("TH");
        static Card C_JH = Card.Parse("JH");
        static Card C_QH = Card.Parse("QH");
        static Card C_KH = Card.Parse("KH");
        static Card C_AH = Card.Parse("AH");
        static Card C_2C = Card.Parse("2C");
        static Card C_3C = Card.Parse("3C");
        static Card C_4C = Card.Parse("4C");
        static Card C_5C = Card.Parse("5C");
        static Card C_6C = Card.Parse("6C");
        static Card C_7C = Card.Parse("7C");
        static Card C_8C = Card.Parse("8C");
        static Card C_9C = Card.Parse("9C");
        static Card C_TC = Card.Parse("TC");
        static Card C_JC = Card.Parse("JC");
        static Card C_QC = Card.Parse("QC");
        static Card C_KC = Card.Parse("KC");
        static Card C_AC = Card.Parse("AC");
        static Card C_2D = Card.Parse("2D");
        static Card C_3D = Card.Parse("3D");
        static Card C_4D = Card.Parse("4D");
        static Card C_5D = Card.Parse("5D");
        static Card C_6D = Card.Parse("6D");
        static Card C_7D = Card.Parse("7D");
        static Card C_8D = Card.Parse("8D");
        static Card C_9D = Card.Parse("9D");
        static Card C_TD = Card.Parse("TD");
        static Card C_JD = Card.Parse("JD");
        static Card C_QD = Card.Parse("QD");
        static Card C_KD = Card.Parse("KD");
        static Card C_AD = Card.Parse("AD");

        private const string HIGH_CARD_HAND = "2H 4D 7C 9S TH";
        private const string ONE_PAIR_HAND = "2H 2D 7C 9S TH";
        private const string TWO_PAIR_HAND = "2H 2D 7C 7S TH";
        private const string THREE_OF_KIND_HAND = "2H 2D 2C 9S TH";
        private const string FOUR_OF_KIND_HAND = "2H 2D 2C 2S TH";
        private const string STRAIGHT_HAND = "3H 4D 5H 6C 7S";
        private const string ACE_LOW_STRAIGHT_HAND = "AH 2D 3H 4C 5S";
        private const string FLUSH_HAND = "2C 4C 6C JC KC";
        private const string FULL_HOUSE_HAND = "4C 4H QS QD QC";
        private const string STRAIGHT_FLUSH_HAND = "3S 4S 5S 6S 7S";
        private const string ROYAL_FLUSH_HAND = "TS JS QS KS AS";

        [Test]
        public void HandEvaluatorTest()
        {
            testHand(HIGH_CARD_HAND, PokerHand.HighCard);
            testHand(ONE_PAIR_HAND, PokerHand.OnePair);
            testHand(TWO_PAIR_HAND, PokerHand.TwoPair);
            testHand(THREE_OF_KIND_HAND, PokerHand.ThreeOfAKind);
            testHand(FOUR_OF_KIND_HAND, PokerHand.FourOfAKind);
            testHand(STRAIGHT_HAND, PokerHand.Straight);
            testHand(ACE_LOW_STRAIGHT_HAND, PokerHand.Straight);
            testHand(FLUSH_HAND, PokerHand.Flush);
            testHand(FULL_HOUSE_HAND, PokerHand.FullHouse);
            testHand(STRAIGHT_FLUSH_HAND, PokerHand.StraightFlush);
            testHand(ROYAL_FLUSH_HAND, PokerHand.RoyalFlush);
        }

        private void testHand(string hand, PokerHand expectedHand)
        {
            BrecherHandEvaluator pe = new BrecherHandEvaluator();
            var h = new Hand(hand);
            var actualHand = pe.Evaluate(h);
            Assert.AreEqual(expectedHand, actualHand);            
        }

        [Test]
        public void TestPair()
        {
            Hand h = new Hand("Qs Qd Qh Qc 5s");
            BrecherHandEvaluator pe = new BrecherHandEvaluator();            
        }

        [Test]
        public void CardCodeTest()
        {
            TestCard(C_2S, 1);
            TestCard(C_3S, 2);
            TestCard(C_4S, 4);
            TestCard(C_5S, 8);
            TestCard(C_6S, 16);
            TestCard(C_7S, 32);
            TestCard(C_8S, 64);
            TestCard(C_9S, 128);
            TestCard(C_TS, 256);
            TestCard(C_JS, 512);
            TestCard(C_QS, 1024);
            TestCard(C_KS, 2048);
            TestCard(C_AS, 4096);
            TestCard(C_2H, 65536);
            TestCard(C_3H, 131072L);
            TestCard(C_4H, 262144L);
            TestCard(C_5H, 524288L);
            TestCard(C_6H, 1048576L);
            TestCard(C_7H, 2097152L);
            TestCard(C_8H, 4194304L);
            TestCard(C_9H, 8388608L);
            TestCard(C_TH, 16777216L);
            TestCard(C_JH, 33554432L);
            TestCard(C_QH, 67108864L);
            TestCard(C_KH, 134217728L);
            TestCard(C_AH, 268435456L);
            TestCard(C_2C, 4294967296L);
            TestCard(C_3C, 8589934592L);
            TestCard(C_4C, 17179869184L);
            TestCard(C_5C, 34359738368L);
            TestCard(C_6C, 68719476736L);
            TestCard(C_7C, 137438953472L);
            TestCard(C_8C, 274877906944L);
            TestCard(C_9C, 549755813888L);
            TestCard(C_TC, 1099511627776L);
            TestCard(C_JC, 2199023255552L);
            TestCard(C_QC, 4398046511104L);
            TestCard(C_KC, 8796093022208L);
            TestCard(C_AC, 17592186044416L);
            TestCard(C_2D, 281474976710656L);
            TestCard(C_3D, 562949953421312L);
            TestCard(C_4D, 1125899906842624L);
            TestCard(C_5D, 2251799813685248L);
            TestCard(C_6D, 4503599627370496L);
            TestCard(C_7D, 9007199254740992L);
            TestCard(C_8D, 18014398509481984L);
            TestCard(C_9D, 36028797018963968L);
            TestCard(C_TD, 72057594037927936L);
            TestCard(C_JD, 144115188075855872L);
            TestCard(C_QD, 288230376151711744L);
            TestCard(C_KD, 576460752303423488L);
            TestCard(C_AD, 1152921504606846976L);
        }

        private void TestCard(Card c, long code)
        {
            var bcode = BrecherHandEvaluator.CardToCode(c);
            Console.WriteLine($"{c} code = {bcode}");
            Assert.AreEqual(code, bcode);
        }

        [Test]
        public void HandCodeTest()
        {
            TestHand("6s 9h 5c 5h 3h", 34368782352L);
            TestHand("Qs Qd Qh Qc 5s", 288234774265332744L);
            TestHand("2H 2D 2C 9S TH", 281479288520832L);
            TestHand("AH 2D 3H 4C JS 5S", 281492425146888L);
            TestHand("AH 2D 3H 4C JS 5S 7C", 281629864100360L);
        }

        private void TestHand(string hand, long code)
        {
            Hand h = new Hand(hand);
            var hcode = BrecherHandEvaluator.HandToCode(h);
            Console.WriteLine($"{h} code = {hcode}");
            Assert.AreEqual(code,hcode);
        }

        [Test]
        public void HandStrengthTest()
        {
            TestStrength("6s 9h 5c 5h 3h", 17003536);
            TestStrength("Qs Qd Qh Qc 5s", 118108160);
        }

        private void TestStrength(string hand, int strength)
        {
            Hand h = new Hand(hand);
            var s = BrecherHandEvaluator.GetHandStrength(h);
            Console.WriteLine($"{h} strength = {s}");
            Assert.AreEqual(strength, s);
        }

    }
}
