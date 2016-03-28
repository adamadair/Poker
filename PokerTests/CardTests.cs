using NUnit.Framework;
using System;
using AWA.Poker;

namespace PokerTests
{
    [TestFixture()]
    public class CardTests
    {
        [Test()]
        public void CardConstructorTest()
        {
            Card c = new Card(CardSuit.Clubs, CardValue.Ace);
            Assert.IsNotNull(c);
            Assert.AreEqual(c.Suit, CardSuit.Clubs);
            Assert.AreEqual(c.Value, CardValue.Ace);
            Card c1 = new Card("KD");
            Assert.AreEqual(c1.Suit, CardSuit.Diamonds);
            Assert.AreEqual(c1.Value, CardValue.King);
        }

        [ExpectedException("AWA.Poker.PokerException")]
        [Test()]
        public void CardConstructorInvalidParameterTest()
        {
            PokerException ex = null;
            try
            {
                var C = new Card("XC");
                Assert.IsNull(C);
            }
            catch (PokerException e)
            {
                ex = e;
            }
            Assert.IsNotNull(ex);
            ex = null;
            try
            {
                var C = new Card("2X");
                Assert.IsNull(C);
            }
            catch (PokerException e)
            {
                ex = e;
            }
            Assert.IsNotNull(ex);
            throw ex;
        }

        [Test()]
        public void CardToStringTest()
        {
            var C = new Card("KD");
            Assert.AreEqual(C.ToString(), "KD");
            Assert.AreEqual(C.ToLongString(), "King of Diamonds");
        }

        [Test()]
        public void AllCardTest()
        {
            testCard("2C", CardSuit.Clubs, CardValue.Two);
            testCard("3C", CardSuit.Clubs, CardValue.Three);
            testCard("4C", CardSuit.Clubs, CardValue.Four);
            testCard("5C", CardSuit.Clubs, CardValue.Five);
            testCard("6C", CardSuit.Clubs, CardValue.Six);
            testCard("7C", CardSuit.Clubs, CardValue.Seven);
            testCard("8C", CardSuit.Clubs, CardValue.Eight);
            testCard("9C", CardSuit.Clubs, CardValue.Nine);
            testCard("TC", CardSuit.Clubs, CardValue.Ten);
            testCard("JC", CardSuit.Clubs, CardValue.Jack);
            testCard("QC", CardSuit.Clubs, CardValue.Queen);
            testCard("KC", CardSuit.Clubs, CardValue.King);
            testCard("AC", CardSuit.Clubs, CardValue.Ace);
            testCard("2D", CardSuit.Diamonds, CardValue.Two);
            testCard("2H", CardSuit.Hearts, CardValue.Two);
            testCard("2S", CardSuit.Spades, CardValue.Two);
            //testCard("JJ", CardSuit.None, CardValue.Joker);
        }

        private void testCard(string init, CardSuit suit, CardValue rank)
        {
            var card = new Card(init);
            Assert.AreEqual(init, card.ToString());
            Assert.AreEqual(card.Suit, suit);
            Assert.AreEqual(card.Value, rank);
            Assert.AreEqual(card.ToLongString(), string.Format("{0} of {1}", rank, suit));
        }

    }
}

