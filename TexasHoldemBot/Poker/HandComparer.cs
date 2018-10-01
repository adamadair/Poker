using System.Collections.Generic;
using System.Linq;

namespace TexasHoldemBot.Poker
{
    /// <inheritdoc />
    /// <summary>
    /// A comparer class that compares two hands of poker.
    /// </summary>
    public class HandComparer : IComparer<Hand>
    {
        readonly IPokerHandEvaluator _evaluator;

        public HandComparer(IPokerHandEvaluator evaluator)
        {
            if (evaluator == null) throw new PokerException("Evaluator can not be null.");
            _evaluator = evaluator;
        }

        /// <inheritdoc />
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
            if (xHand > yHand)
                return 1;
            switch (xHand)
            {
                case PokerHand.HighCard:
                    return HighCardBreak(x, y);
                case PokerHand.OnePair:
                    return PairBreak(x, y);
                case PokerHand.TwoPair:
                    return TwoPairBreak(x, y);
                case PokerHand.ThreeOfAKind:
                    return ThreeOfAKindBreak(x, y);
                case PokerHand.FourOfAKind:
                    return FourOfAKindBreak(x, y);
                case PokerHand.FullHouse:
                    return ThreeOfAKindBreak(x, y);
                case PokerHand.Straight:
                    return StraightBreak(x, y);
                case PokerHand.Flush:
                    return FlushBreak(x, y);
                case PokerHand.StraightFlush:
                    return StraightBreak(x, y);
            }
            return 0;
        }

        private int HighCardBreak(Hand x, Hand y)
        {
            Card[] xHand = x.Cards.OrderByDescending(c => c.Value).ToArray();
            Card[] yHand = y.Cards.OrderByDescending(c => c.Value).ToArray();
            return CompareHighCards(xHand, yHand);
        }

        private static int CompareHighCards(IReadOnlyList<Card> xHand, IReadOnlyList<Card> yHand)
        {
            for (var i = 0; i < xHand.Count; ++i)
            {
                Card xCard = xHand[i];
                if (i >= yHand.Count) continue;
                Card yCard = yHand[i];
                if (xCard.Value < yCard.Value) return -1;
                if (xCard.Value > yCard.Value) return 1;
            }
            return 0;
        }

        private int PairBreak(Hand x, Hand y)
        {
            var xHand = new List<Card>(x.Cards.OrderByDescending(c => c.Value));
            var yHand = new List<Card>(y.Cards.OrderByDescending(c => c.Value));
            IGrouping<CardValue, Card>[] xPair = Pairs(xHand).ToArray();
            IGrouping<CardValue, Card>[] yPair = Pairs(yHand).ToArray();
            if (xPair.First().Key == yPair.First().Key)
            {                
                foreach(Card xp in xPair.First())
                {
                    xHand.Remove(xp);
                }
                foreach(Card yp in yPair.First())
                {
                    yHand.Remove(yp);
                }
                return CompareHighCards(GetCardArray(xHand, 3), GetCardArray(yHand, 3));
            }
            if (xPair.First().Key > yPair.First().Key) return 1;
            return -1;
        }

        /// <summary>
        /// This is going to create an 
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static Card[] GetCardArray(IEnumerable<Card> cards, int n)
        {
            Card[] enumerable = cards as Card[] ?? cards.ToArray();
            return n > enumerable.Count() ? enumerable.ToArray() : enumerable.Take(n).ToArray();
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

        private int TwoPairBreak(Hand x, Hand y)
        {
            var xHand = new List<Card>(x.Cards.OrderBy(c => c.Value));
            var yHand = new List<Card>(y.Cards.OrderBy(c => c.Value));
            IGrouping<CardValue, Card>[] xPair = Pairs(xHand).ToArray();
            IGrouping<CardValue, Card>[] yPair = Pairs(yHand).ToArray();
            for(var i = 0; i < 2; ++i)
            {
                if (xPair[0].Key == yPair[0].Key) continue;
                if (xPair[0].Key < yPair[0].Key)
                    return -1;
                return 1;
            }
            if (xHand[4].Value < yHand[4].Value)
                return -1;
            return xHand[4].Value > yHand[4].Value ? 1 : 0;
        }

        private int ThreeOfAKindBreak(Hand x, Hand y)
        {
            CardValue xTripVal = Trips(x.Cards).First().Key;
            CardValue yTripVal = Trips(y.Cards).First().Key;
            if (xTripVal < yTripVal) return -1;
            return 1;
        }

        private int FourOfAKindBreak(Hand x, Hand y)
        {
            CardValue xQuadVal = Quad(x.Cards).First().Key;
            CardValue yQuadVal = Quad(y.Cards).First().Key;
            if (xQuadVal < yQuadVal) return -1;
            return 1;
        }

        private static int StraightBreak(Hand x, Hand y)
        {
            Card[] xHand = x.Cards.OrderByDescending(c => c.Value).ToArray();
            xHand = TrimLowAce(xHand);
            Card[] yHand = y.Cards.OrderByDescending(c => c.Value).ToArray();
            yHand = TrimLowAce(yHand);
            return CompareHighCards(xHand, yHand);
        }

        private static Card[] TrimLowAce(Card[] ca)
        {
            if(ca[0].Value==CardValue.Ace&&ca[4].Value == CardValue.Two)
            {
                return ca.Skip(1).ToArray();
            }
            return ca;
        }

        private int FlushBreak(Hand x, Hand y)
        {
            return HighCardBreak(x, y);
        }        
    }
}
