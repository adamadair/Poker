using NUnit.Framework;
using TexasHoldemBot.Poker;

namespace PokerTests.TexasHoldemBot
{
    [TestFixture]
    public class TexasHoldemHands
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
        public void FlopHandEvaluatorTest()
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

        private const string TURN_HIGH_CARD_HAND = "2H 4D 7C 9S TH AC";
        private const string TURN_ONE_PAIR_HAND = "2H 4D 7C 9S TH 4C";
        private const string TURN_TWO_PAIR_HAND = "2H 2D 7C 6S TH 7H";
        private const string TURN_THREE_OF_KIND_HAND = "2H 2D 7C 9S TH 2C";
        private const string TURN_FOUR_OF_KIND_HAND = "2H 2D 2C 3S TH 2S";
        private const string TURN_STRAIGHT_HAND = "3H 4D 5H 6C AS 7S";
        private const string TURN_ACE_LOW_STRAIGHT_HAND = "AH 2D 3H 4C JS 5S";
        private const string TURN_FLUSH_HAND = "2C 4C 6C JC 2S KC";
        private const string TURN_FULL_HOUSE_HAND = "4C 4H QS QD KS QC";
        private const string TURN_STRAIGHT_FLUSH_HAND = "3S 4S 5S 6S KC 7S";
        private const string TURN_ROYAL_FLUSH_HAND = "TS JS QD KS AS KC QS";

        [Test()]
        public void TurnHandEvaluatorTest()
        {
            //testHand(TURN_HIGH_CARD_HAND, PokerHand.HighCard);
            //testHand(TURN_ONE_PAIR_HAND, PokerHand.OnePair);
            //testHand(TURN_TWO_PAIR_HAND, PokerHand.TwoPair);
            //testHand(TURN_THREE_OF_KIND_HAND, PokerHand.ThreeOfAKind);
            //testHand(TURN_FOUR_OF_KIND_HAND, PokerHand.FourOfAKind);
            //testHand(TURN_STRAIGHT_HAND, PokerHand.Straight);
            //testHand(TURN_ACE_LOW_STRAIGHT_HAND, PokerHand.Straight);
            //testHand(TURN_FLUSH_HAND, PokerHand.Flush);
            //testHand(TURN_FULL_HOUSE_HAND, PokerHand.FullHouse);
            //testHand(TURN_STRAIGHT_FLUSH_HAND, PokerHand.StraightFlush);
            //testHand(TURN_ROYAL_FLUSH_HAND, PokerHand.RoyalFlush);
        }

        private void testHand(string hand, PokerHand expectedHand)
        {
            PokerHandEvaluator pe = new PokerHandEvaluator();
            var h = new Hand(hand);
            var actualHand = pe.Evaluate(h);
            Assert.AreEqual(expectedHand, actualHand);
        }
    }
}