using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemBot;
using TexasHoldemBot.Poker;

namespace PokerTests
{
    [TestFixture]
    public class BrecherHandEvaluatorTests
    {
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

        [Test()]
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
            Assert.AreEqual(pe.Evaluate(h), expectedHand);
        }
    }
}
