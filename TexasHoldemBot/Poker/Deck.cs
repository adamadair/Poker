using System;
using System.Collections.Generic;
using System.Linq;

namespace TexasHoldemBot.Poker
{
    /// <summary>
    /// Convenience methods for dealing with an entire deck of cards.
    /// </summary>
    public static class Deck
    {
        /// <summary>
        /// An array of cards that makes up an entire playing deck for
        /// Texas Hold'em. 
        /// </summary>
        private static readonly Card[] _deck = Card.Cards.Values.ToArray();

        /// <summary>
        /// Get the full deck
        /// </summary>
        /// <returns></returns>
        public static List<Card> FullDeck()
        {
            return new List<Card>(_deck);
        }

        /// <summary>
        /// Get a deck, minus some cards.
        /// </summary>
        /// <param name="outs">list of cards NOT in the deck</param>
        /// <returns>The sub deck</returns>
        public static List<Card> GetSubDeck(IEnumerable<Card> outs)
        {
            var l = new List<Card>(_deck);
            foreach (Card c in outs)
            {
                l.Remove(c);
            }
            return l;
        }

        /// <summary>
        /// Gets the paired values in a collection of cards.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns>list of card values</returns>
        public static List<CardValue> Pairs(IEnumerable<Card> cards)
        {
            
            int[] varray = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            List<CardValue> l = new List<CardValue>();
            foreach (var c in cards)
            {
                varray[(int) c.Value]+=1;
            }

            foreach (var v in Enum.GetValues(typeof(CardValue)))
            {
                if (varray[(int) v] > 1)
                {
                    l.Add((CardValue)v);
                }
            }

            return l;
        }

        /// <summary>
        /// Counts the number of each suit in a collection of cards.
        /// </summary>
        /// <param name="cards">The card collection in question</param>
        /// <returns>Dictionary of suit counts</returns>
        public static Dictionary<CardSuit,int> Suited(IEnumerable<Card> cards)
        {
            var r = new Dictionary<CardSuit, int>
            {
                {CardSuit.Clubs, 0},
                {CardSuit.Diamonds, 0},
                {CardSuit.Hearts, 0},
                {CardSuit.Spades, 0}
            };
            foreach (var c in cards)
            {
                r[c.Suit] += 1;
            }
            return r;
        }        

        /// <summary>
        /// Get all two card combinations from a collection of cards
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<Card, Card>> GetTwoCardCombinations(IEnumerable<Card> cards)
        {
            var l = new List<Tuple<Card, Card>>();
            Card[] a = cards.ToArray();
            for (var i = 0; i < a.Length - 1; ++i)
            {
                for (var j = i + 1; j < a.Length; ++j)
                {
                    l.Add(new Tuple<Card, Card>(a[i], a[j]));
                }
            }
            return l;
        }
    }

}
