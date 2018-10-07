using System;
using System.Linq;
using TexasHoldemBot.Poker;
using NUnit.Framework;

namespace PokerTests.TexasHoldemBot
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void TestEquals()
        {
            var ac1 = new Card(CardSuit.Clubs, CardValue.Ace);
            var ac2 = new Card(CardSuit.Clubs, CardValue.Ace);
            Assert.IsTrue(ac1.Equals(ac2));
            Assert.AreEqual(new Card(CardSuit.Clubs,CardValue.Ace), new Card(CardSuit.Clubs, CardValue.Ace));
            Assert.AreEqual(Cards.C_2C, new Card(CardSuit.Clubs, CardValue.Two));
            Assert.AreNotEqual(Cards.C_2C, Cards.C_2D);
            var carray = Card.Cards.Values.ToArray();
            for (int i = 0; i < carray.Length; ++i)
            {
                for (int j = 0; j < carray.Length; ++j)
                {
                    if (i == j)
                    {
                        Assert.AreEqual(carray[i], carray[j]);
                    }
                    else
                    {
                        Assert.AreNotEqual(carray[i], carray[j]);
                    }
                }
            }
        }

        /// <summary>
        /// TEST CASE: Get all two card combinations from a
        /// full deck of cards.
        /// </summary>
        [Test]
        public void DeckTwoCardComboTest()
        {
            var deck = Deck.FullDeck();
            Assert.AreEqual(deck.Count,52);
            var combos = Deck.GetTwoCardCombinations(deck);
            Assert.IsNotNull(combos);
            var c = 0;
            foreach (var cmbo in combos)
            {
                ++c;
                Console.WriteLine($"{cmbo.Item1}, {cmbo.Item2}");
            }
            Console.WriteLine($"count = {c}");
            
            //There are actually 1,326 combinations of hole cards in hold em ({52 × 51}/2). 
            Assert.AreEqual(1326, c);
        }

        /// <summary>
        /// TEST CASE: Create a sub deck by removing a set of cards.
        /// </summary>
        [Test]
        public void SubDeckTest()
        {
            var KnownCards = new Card[] {Card.CJ, Card.C4, Card.H6, Card.DK, Card.S7};
            var subDeck = Deck.GetSubDeck(KnownCards);
            Assert.AreEqual(47, subDeck.Count);
            Assert.IsFalse(subDeck.Contains(Card.CJ));
            Assert.IsFalse(subDeck.Contains(Card.C4));
            Assert.IsFalse(subDeck.Contains(Card.H6));
            Assert.IsFalse(subDeck.Contains(Card.DK));
            Assert.IsFalse(subDeck.Contains(Card.S7));

            var combos = Deck.GetTwoCardCombinations(subDeck);
            Assert.AreEqual(1081, combos.Count());
        }

        /// <summary>
        /// TEST CASE: Remove card from collection
        /// Need to be able to remove cards from a collection using separate
        /// but equal Card objects.
        /// </summary>
        [Test]
        public void RemoveCardTest()
        {
            var deck = Deck.FullDeck();       // Get a full deck
            Assert.AreEqual(52, deck.Count);        // Verify the deck is 52 cards.
            
            // Remove the Ace of clubs from the deck
            deck.Remove(new Card(CardSuit.Clubs, CardValue.Ace));
            
            Assert.AreEqual(51, deck.Count);        // Verify the deck is 51 cards.
            
            Assert.False(deck.Contains(Card.CA)); // Verify the ace of clubs is gone
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CountPairsTest()
        {
            var r1 = Deck.Pairs(new[] {Card.C2, Card.D2, Card.DA, Card.H7, Card.SQ});
            Assert.AreEqual(1,r1.Count);
            Assert.AreEqual(r1[0], CardValue.Two);

            var r2 = Deck.Pairs(new[] { Card.C2, Card.D2, Card.DA, Card.HA, Card.SQ });
            Assert.AreEqual(2, r2.Count);
            Assert.IsTrue(r2.Contains(CardValue.Two));
            Assert.IsTrue(r2.Contains(CardValue.Ace));

            var r3 = Deck.Pairs(new[] { Card.C2, Card.D3, Card.DA, Card.HK, Card.SQ });
            Assert.AreEqual(0, r3.Count);

            var r4 = Deck.Pairs(new[] { Card.C2, Card.D3, Card.DA});
            Assert.AreEqual(0, r4.Count);           

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void CountSuitsTest()
        {
            var r1 = Deck.Suited(new[] { Card.C2, Card.D2, Card.DA, Card.H7, Card.SQ });
            Assert.AreEqual(1, r1[CardSuit.Clubs]);
            Assert.AreEqual(2, r1[CardSuit.Diamonds]);
            Assert.AreEqual(1, r1[CardSuit.Hearts]);
            Assert.AreEqual(1, r1[CardSuit.Spades]);
            Assert.AreEqual(1, r1.Count(c => c.Value == 2));
            Assert.AreEqual(0, r1.Count(c => c.Value == 3));



            var r2 = Deck.Suited(new[] { Card.D3, Card.D2, Card.DA, Card.HA, Card.SQ });
            Assert.AreEqual(1, r2.Count(c => c.Value == 3));
            Assert.AreEqual(CardSuit.Diamonds, r2.First(c => c.Value == 3).Key);


        }

    }
}