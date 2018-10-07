using System;
using System.Collections.Generic;
using System.Linq;
using TexasHoldemBot.Poker;

namespace TexasHoldemBot.Ai
{
    /**
          _____
         |A .  | _____
         | /.\ ||A ^  | _____
         |(_._)|| / \ ||A _  | _____
         |  |  || \ / || ( ) ||A_ _ |
         |____V||  .  ||(_'_)||( v )|
                |____V||  |  || \ / |
                       |____V||  .  |
                              |____V|

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
        /// Hole card points are the sum of the point values with
        /// the following additional points:
        ///   +30 for a pocket pair
        ///   +10 if both cards are same suit
        ///   +10 if the cards are connected 
        /// </summary>
        /// <param name="cards">An array of 2 cards.</param>
        /// <returns>The point value of the cards</returns>
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

        /// <summary>
        /// Once cards start turning over on the table the value of the
        /// hole cards has to be re-evaluated.
        /// 
        /// </summary>
        /// <param name="holeCards"></param>
        /// <param name="tableCards"></param>
        /// <returns></returns>
        public static int PostFlopStrength(Card[] holeCards, Card[] tableCards)
        {
            int holeCard1Value = CardValue(holeCards[0]);
            int holeCard2Value = CardValue(holeCards[1]);
            int value = HoleCardPoints(holeCards);
            var pairs = Deck.Pairs(tableCards);
            var suits = Deck.Suited(tableCards);

            // First we handle the case where there are no pairs or suited cards on the table,
            // We can return a value directly.
            if (pairs.Count == 0 && !suits.Any(c => c.Value > 1))
            {
                // if we have a pocket pair, and there is a match return double points.
                if (holeCards[0].Value == holeCards[1].Value)
                {
                    if (tableCards.Any(c => c.Value == holeCards[0].Value))
                    {
                        return value * 2;
                    }
                }

                // If we have pocket suited cards, and there is one of the same on the table +10
                if (holeCards[0].Suit == holeCards[1].Suit)
                {
                    if (tableCards.Any(c => c.Suit == holeCards[0].Suit))
                    {
                        value += 10;
                    }
                }

                /*
                 * If there are no pairs or suited cards on the flop, add some points
                 * for any pairs we may have. Don't worry about 3 of a kind, because
                 * that  will be handled.
                 */
                value += tableCards.Count(c => c.Value == holeCards[0].Value) * ((holeCard1Value > 0)
                    ? holeCard1Value
                    : 5);
                value += tableCards.Count(c => c.Value == holeCards[1].Value) * ((holeCard2Value > 0)
                             ? holeCard2Value
                             : 5);
                // If we drew an open ended straight add 10. You have roughly %16 to get the straight.
                // add another 10. 
                if (IsOpenEndedStraightDraw(holeCards, tableCards))
                {
                    value += 10;
                }

                return value;
            }

            if (pairs.Count > 0)
            {
                // if there is a hole card that matches the flopped pair +30
                // if not -10 (the other guy might have a match)
                foreach (var p in pairs)
                {
                    if (holeCards.Any(c => c.Value == p))
                    {
                        value += 30;
                    }
                    else
                    {
                        value -= 10;
                    }
                }
            }

            if (suits.Count(c => c.Value == 3) > 0)
            {
                // There are three suited cards on the table!
                // if we have a match
                CardSuit s = suits.First(c => c.Value == 3).Key;
                var suitCount = holeCards.Count(c => c.Suit == s);
                if (suitCount == 0)
                {
                    value -= 20;
                }

                else if (suitCount == 1)
                {
                    // if the single match is an ace add 20, else 10
                    if (holeCards.First(c => c.Suit == s).Value == Poker.CardValue.Ace)
                        value += 20;
                    else
                    {
                        value += 10;
                    }
                }

                else if (suitCount == 2)
                {
                    value += 30; // silly, as we will recognize this as a flush, but this is what the book says.
                }

            }
            else if (suits.Count(c => c.Value == 2) > 0)
            {
                // there is at least one pair of suited cards on the table.
                // If we have two matching cards then add 20 else subtract 10
                if (holeCards[0].Suit == holeCards[1].Suit)
                {
                    CardSuit s = holeCards[0].Suit;
                    foreach (CardSuit cs in suits.Where(c=>c.Value==2).Select(t=>t.Key))
                    {
                        if (cs == s)
                        {
                            value += 20;
                        }
                        else
                        {
                            value -= 10;
                        }
                    }
                }
                else
                {
                    value -= 10;
                }
            }
            return value;
        }

        /// <summary>
        /// determine if you have an open ended straight draw.
        /// </summary>
        /// <param name="holeCards"></param>
        /// <param name="tableCards"></param>
        /// <returns></returns>
        public static bool IsOpenEndedStraightDraw(Card[] holeCards, Card[] tableCards)
        {
            // add any cards to consideration that are not aces. In a straight
            // and ace is terminal, so it would not be open ended.
            var cards = new List<Card>(holeCards.Where(c => c.Value != Poker.CardValue.Ace));
            cards.AddRange(tableCards.Where(c => c.Value != Poker.CardValue.Ace));
            
            cards.Sort((card, card1) => card.Value.CompareTo(card1.Value));
            var run = 1;
            for (var i = 1; i < cards.Count; ++i)
            {
                if ((int) cards[i].Value - (int) cards[i - 1].Value == 1)
                {
                    ++run;
                    if (run == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    run = 1;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Card values are as follows:
        /// A = 15
        /// K = 10
        /// Q = 10
        /// J = 5
        /// T = 5        
        /// All other cards are 0
        /// </summary>
        /// <param name="c">The card</param>
        /// <returns>The value of the card</returns>
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
