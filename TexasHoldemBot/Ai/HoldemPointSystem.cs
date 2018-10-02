using System;
using TexasHoldemBot.Poker;

namespace TexasHoldemBot.Ai
{
    /**
        The ideas in the HoldemPointSystem class come from the book
        "TEXAS HOLD'EM MADE EASY: A Systematic Process for Steady Winnings 
           at No-limit Hold'em." by Walt Hazelton, 2015, Xlibris

        If you are interested in getting a copy of the book it is currently
        sold on Amazon.com at:
        https://www.amazon.com/Texas-HoldEm-Made-Easy-Systematic-ebook/dp/B0793P644C/ref=sr_1_1?ie=UTF8&qid=1538448408&sr=8-1&keywords=texas+holdem+made+easy
        The kindle edition is currently $3.       
     */
    public static class HoldemPointSystem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static int HoleCardPoints(Card[] cards)
        {
            if (cards.Length != 2)
                throw new ArgumentException("You can only have 2 hole cards.", nameof(cards));
            Card c1 = cards[0];
            Card c2 = cards[1];
            var value = CardValue(c1) + CardValue(c2);

            // pairs get +30
            if (c1.Value == c2.Value)
                value += 30;
            // suited cards get +10            
            if (c1.Suit == c2.Suit)
                value += 10;

            // connected cards get +10
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
