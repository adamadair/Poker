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
