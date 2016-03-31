using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWA.Poker
{
    /// <summary>
    /// A comparer class that compares two hands of poker.
    /// </summary>
    public class HandComparer : IComparer<Hand>
    {
        IPokerHandEvaluator _evaluator;

        public HandComparer(IPokerHandEvaluator evaluator)
        {
            if (evaluator == null) throw new PokerException("Evaluator can not be null.");
            _evaluator = evaluator;
        }
        /// <summary>
        /// Compares to hands of poker.
        /// </summary>
        /// <param name="x">one hand</param>
        /// <param name="y">the other hand</param>
        /// <returns>
        /// Less than zero - x is less than y.
        /// Zero - x equals y.
        /// Greater than zero - x is greater than y.
        /// </returns>
        public int Compare(Hand x, Hand y)
        {
            PokerHand xHand = _evaluator.Evaluate(x);
            PokerHand yHand = _evaluator.Evaluate(y);
            if (xHand < yHand)
                return -1;
            else if (xHand > yHand)
                return 1;
            else
            {
                switch (xHand)
                {
                    case PokerHand.HighCard:
                        return highCardBreak(x, y);
                    case PokerHand.OnePair:
                        return pairBreak(x, y);
                    case PokerHand.TwoPair:
                        return twoPairBreak(x, y);
                    case PokerHand.ThreeOfAKind:
                        return threeOfAKindBreak(x, y);
                    case PokerHand.FourOfAKind:
                        return fourOfAKindBreak(x, y);
                    case PokerHand.FullHouse:
                        return threeOfAKindBreak(x, y);
                    case PokerHand.Straight:
                        return straightBreak(x, y);
                    case PokerHand.Flush:
                        return flushBreak(x, y);
                    case PokerHand.StraightFlush:
                        return straightBreak(x, y);
                }
            }
            return 0;
        }

        private int highCardBreak(Hand x, Hand y)
        {
            Card[] xHand = x.Cards.OrderByDescending(c => c.Value).ToArray();
            Card[] yHand = y.Cards.OrderByDescending(c => c.Value).ToArray();
            return compareHighCards(xHand, yHand);
        }

        private int compareHighCards(Card[] xHand, Card[] yHand)
        {
            for (int i = 0; i < xHand.Length; ++i)
            {
                var xCard = xHand[i];
                if (i < yHand.Length)
                {
                    var yCard = yHand[i];
                    if (xCard.Value < yCard.Value) return -1;
                    if (xCard.Value > yCard.Value) return 1;
                }
            }
            return 0;
        }

        private int pairBreak(Hand x, Hand y)
        {
            List<Card> xHand = new List<Card>(x.Cards.OrderByDescending(c => c.Value));
            List<Card> yHand = new List<Card>(y.Cards.OrderByDescending(c => c.Value));
            var xPair = Pairs(xHand);
            var yPair = Pairs(yHand);
            if (xPair.First().Key == yPair.First().Key)
            {
                //uggg - Remove the pairs, and check high cards.
                foreach(var xp in xPair.First())
                {
                    xHand.Remove(xp);
                }
                foreach(var yp in yPair.First())
                {
                    yHand.Remove(yp);
                }
                return compareHighCards(GetCardArray(xHand, 3), GetCardArray(yHand, 3));
            }
            else
            {
                if (xPair.First().Key > yPair.First().Key) return 1;
                else return -1;
            }                        
        }

        /// <summary>
        /// This is going to create an 
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private Card[] GetCardArray(IEnumerable<Card> cards, int n)
        {
            if (n > cards.Count())
            {
                return cards.ToArray();
            }
            return cards.Take(n).ToArray();
        }

        private IEnumerable<IGrouping<CardValue, Card>> GroupByValue(IEnumerable<Card> cards)
        {
            return cards.GroupBy(card => card.Value);
        }
        private IEnumerable<IGrouping<CardValue, Card>> Pairs(IEnumerable<Card> cards)
        {
            return GroupByValue(cards).Where(group => group.Count() == 2).OrderByDescending(g => g.Key);
        }
    
        private IEnumerable<IGrouping<CardValue, Card>> Trips(IEnumerable<Card> cards)
        {
            return GroupByValue(cards).Where(group => group.Count() == 3).OrderByDescending(g => g.Key);
        }

        private IEnumerable<IGrouping<CardValue, Card>> Quad(IEnumerable<Card> cards)
        {
            return GroupByValue(cards).Where(group => group.Count() == 4).OrderByDescending(g => g.Key);
        }

        private int twoPairBreak(Hand x, Hand y)
        {
            List<Card> xHand = new List<Card>(x.Cards.OrderBy(c => c.Value));
            List<Card> yHand = new List<Card>(y.Cards.OrderBy(c => c.Value));
            var xPair = Pairs(xHand).ToArray();
            var yPair = Pairs(yHand).ToArray();
            for(int i = 0; i < 2; ++i)
            {
                if (xPair[0].Key != yPair[0].Key)
                {
                    if (xPair[0].Key < yPair[0].Key)
                        return -1;
                    return 1;                
                }
            }
            if (xHand[4].Value < yHand[4].Value)
                return -1;
            if (xHand[4].Value > yHand[4].Value)
                return 1;
            return 0;
        }

        private int threeOfAKindBreak(Hand x, Hand y)
        {
            var xTripVal = Trips(x.Cards).First().Key;
            var yTripVal = Trips(y.Cards).First().Key;
            if (xTripVal < yTripVal) return -1;
            return 1;
        }

        private int fourOfAKindBreak(Hand x, Hand y)
        {
            var xQuadVal = Quad(x.Cards).First().Key;
            var yQuadVal = Quad(y.Cards).First().Key;
            if (xQuadVal < yQuadVal) return -1;
            return 1;
        }

        private int straightBreak(Hand x, Hand y)
        {
            Card[] xHand = x.Cards.OrderByDescending(c => c.Value).ToArray();
            xHand = trimLowAce(xHand);
            Card[] yHand = y.Cards.OrderByDescending(c => c.Value).ToArray();
            yHand = trimLowAce(yHand);
            return compareHighCards(xHand, yHand);
        }

        private Card[] trimLowAce(Card[] ca)
        {
            if(ca[0].Value==CardValue.Ace&&ca[4].Value == CardValue.Two)
            {
                return ca.Skip(1).ToArray();
            }
            return ca;
        }

        private int flushBreak(Hand x, Hand y)
        {
            return highCardBreak(x, y);
        }        

    }
}
