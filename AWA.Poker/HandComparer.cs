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
                    

                }
            }
                return 0;
        }

        private int highCardBreak(Hand x, Hand y)
        {
            Card[] xHand = x.Cards.OrderBy(c => c.Value).ToArray();
            Card[] yHand = x.Cards.OrderBy(c => c.Value).ToArray();
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
            List<Card> xHand = new List<Card>(x.Cards.OrderBy(c => c.Value));
            List<Card> yHand = new List<Card>(y.Cards.OrderBy(c => c.Value));
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

        private IEnumerable<IGrouping<CardValue, Card>> GroupByNominalValue(IEnumerable<Card> cards)
        {
            return cards.GroupBy(card => card.Value);
        }
        private IEnumerable<IGrouping<CardValue, Card>> Pairs(IEnumerable<Card> cards)
        {
            return GroupByNominalValue(cards).Where(group => group.Count() == 2).OrderByDescending(g => g.Key);
        }

        private int twoPairBreak(Hand x, Hand y)
        {
            return 0;
        }

        private int threeOfAKindBreak(Hand x, Hand y)
        {
            return 0;
        }

        private int fourOfAKindBreak(Hand x, Hand y)
        {
            return 0;
        }



    }
}
