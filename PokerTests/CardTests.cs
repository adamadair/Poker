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
            Card c = new Card(CardSuit.Clubs, CardRank.Ace);
            Assert.IsNotNull(c);
            Assert.AreEqual(c.Suit, CardSuit.Clubs);
            Assert.AreEqual(c.Rank, CardRank.Ace);
            Card c1 = new Card("KD");
            Assert.AreEqual(c1.Suit, CardSuit.Diamonds);
            Assert.AreEqual(c1.Rank, CardRank.King);
        }

        [ExpectedException("AWA.Poker.PokerException")]
        [Test()]
        public void CardConstructorInvalidParameterTest()
        {
            PokerException ex = null;
            try
            {
                var C = new Card(CardSuit.None, CardRank.King);
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
            testCard("2C", CardSuit.Clubs, CardRank.Two);
            testCard("3C", CardSuit.Clubs, CardRank.Three);
            testCard("4C", CardSuit.Clubs, CardRank.Four);
            testCard("5C", CardSuit.Clubs, CardRank.Five);
            testCard("6C", CardSuit.Clubs, CardRank.Six);
            testCard("7C", CardSuit.Clubs, CardRank.Seven);
            testCard("8C", CardSuit.Clubs, CardRank.Eight);
            testCard("9C", CardSuit.Clubs, CardRank.Nine);
            testCard("TC", CardSuit.Clubs, CardRank.Ten);
            testCard("JC", CardSuit.Clubs, CardRank.Jack);
            testCard("QC", CardSuit.Clubs, CardRank.Queen);
            testCard("KC", CardSuit.Clubs, CardRank.King);
            testCard("AC", CardSuit.Clubs, CardRank.Ace);
            testCard("2D", CardSuit.Diamonds, CardRank.Two);
            testCard("2H", CardSuit.Hearts, CardRank.Two);
            testCard("2S", CardSuit.Spades, CardRank.Two);
            //testCard("JJ", CardSuit.None, CardRank.Joker);
        }

        private void testCard(string init, CardSuit suit, CardRank rank)
        {
            var card = new Card(init);
            Assert.AreEqual(init, card.ToString());
            Assert.AreEqual(card.Suit, suit);
            Assert.AreEqual(card.Rank, rank);
            Assert.AreEqual(card.ToLongString(), string.Format("{0} of {1}", rank, suit));
        }

    }
}

