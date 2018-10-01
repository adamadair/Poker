using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemBot.Poker;

namespace TexasHoldemBot.Ai
{

    public static class HoldemPointSystem
    {
        public static int HoleCardPoints(Card[] cards)
        {
            if (cards.Length != 2)
                throw new ArgumentException("You can only have 2 hole cards.", nameof(cards));
            Card c1 = cards[0];
            Card c2 = cards[1];
            var value = CardValue(c1) + CardValue(c2);
            if (c1.Value == c2.Value)
                value += 30;
            if (c1.Suit == c2.Suit)
                value += 10;
            if (c1.IsConnected(c2))
            {
                value += 10;
            }

            return value;
        }
                

        public static int CardValue(Card c)
        {
            switch (c.Value)
            {
                case Poker.CardValue.Ten:
                    return 5;
                case Poker.CardValue.Jack:
                    return 5;
                case Poker.CardValue.Queen:
                    return 10;
                case Poker.CardValue.King:
                    return 10;
                case Poker.CardValue.Ace:
                    return 15;
                default:
                    return 0;
            }
        }
        
    }
}
