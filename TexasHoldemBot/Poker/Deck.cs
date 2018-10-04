using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemBot.Poker
{
    public static class Deck
    {
        private static readonly Card[] _deck = Card.Cards.Values.ToArray();

        public static List<Card> FullDeck()
        {
            return new List<Card>(_deck);
        }

        public static List<Card> GetSubDeck(IEnumerable<Card> outs)
        {
            var l = new List<Card>(_deck);
            foreach (Card c in outs)
            {
                l.Remove(c);
            }
            return l;
        }

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
