using System;
using NUnit.Framework;
using TexasHoldemBot.Poker;

namespace PokerTests.TexasHoldemBot
{
    [TestFixture]
    public class BrecherHandEvaluatorTests
    {
        static Card C_2S = new Card("2S");
        static Card C_3S = new Card("3S");
        static Card C_4S = new Card("4S");
        static Card C_5S = new Card("5S");
        static Card C_6S = new Card("6S");
        static Card C_7S = new Card("7S");
        static Card C_8S = new Card("8S");
        static Card C_9S = new Card("9S");
        static Card C_TS = new Card("TS");
        static Card C_JS = new Card("JS");
        static Card C_QS = new Card("QS");
        static Card C_KS = new Card("KS");
        static Card C_AS = new Card("AS");
        static Card C_2H = new Card("2H");
        static Card C_3H = new Card("3H");
        static Card C_4H = new Card("4H");
        static Card C_5H = new Card("5H");
        static Card C_6H = new Card("6H");
        static Card C_7H = new Card("7H");
        static Card C_8H = new Card("8H");
        static Card C_9H = new Card("9H");
        static Card C_TH = new Card("TH");
        static Card C_JH = new Card("JH");
        static Card C_QH = new Card("QH");
        static Card C_KH = new Card("KH");
        static Card C_AH = new Card("AH");
        static Card C_2C = new Card("2C");
        static Card C_3C = new Card("3C");
        static Card C_4C = new Card("4C");
        static Card C_5C = new Card("5C");
        static Card C_6C = new Card("6C");
        static Card C_7C = new Card("7C");
        static Card C_8C = new Card("8C");
        static Card C_9C = new Card("9C");
        static Card C_TC = new Card("TC");
        static Card C_JC = new Card("JC");
        static Card C_QC = new Card("QC");
        static Card C_KC = new Card("KC");
        static Card C_AC = new Card("AC");
        static Card C_2D = new Card("2D");
        static Card C_3D = new Card("3D");
        static Card C_4D = new Card("4D");
        static Card C_5D = new Card("5D");
        static Card C_6D = new Card("6D");
        static Card C_7D = new Card("7D");
        static Card C_8D = new Card("8D");
        static Card C_9D = new Card("9D");
        static Card C_TD = new Card("TD");
        static Card C_JD = new Card("JD");
        static Card C_QD = new Card("QD");
        static Card C_KD = new Card("KD");
        static Card C_AD = new Card("AD");

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
