using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerRank
{
    class Program
    {
        private static void Main(string[] args)
        {
            var line1 = Console.ReadLine()?.Trim();
            var n = int.Parse(line1 ?? throw new InvalidOperationException());
            for (var i = 0; i < n; ++i)
            {
                var line = Console.ReadLine();
                string p1Hand = new string(line.Take(14).ToArray());
                string p2Hand = new string(line.Skip(15).ToArray());
                var h1 = new Hand(p1Hand);
                var h2 = new Hand(p2Hand);
                int val = h1.CompareTo(h2);
                string result = "Player 2";
                if (val > 0) result = "Player 1";
                if (val == 0)
                {
                    result = "TIE";
                }
                Console.WriteLine(result);
            }
        }
    }

    public class Card
    {
        public Card(CardSuit cs, CardValue cr)
        {
            Suit = cs;
            Value = cr;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AWA.Poker.Card" /> class. Takes
        ///     an input string in the format [rank][suite] where: <br />
        ///     rank is a character in the set [2,3,4,5,6,7,8,9,T,J,Q,K,A], <br />
        ///     and suit is a character in the set [C,D,H,S] <br />
        ///     Example: <br />
        ///     2S => 2 of Spades <br />
        ///     JC => Jack of Clubs <br />
        ///     A joker is a special case which has no rank or suit and can be initialized with
        ///     the string "JJ"
        /// </summary>
        /// <param name="cardString">Card string.</param>
        public Card(string cardString)
        {
            SetCardValues(cardString);
        }

        public CardSuit Suit { get; private set; }
        public CardValue Value { get; private set; }

        /// <summary>
        ///     Sets the card values from an input string.
        /// </summary>
        /// <param name="val">Value.</param>
        private void SetCardValues(string val)
        {
            if (val.Length != 2)
                throw new PokerException("Incorrect length for card string. Must be 2 characters.");
            var cr = val.Substring(0, 1).ToUpper();
            var cs = val.Substring(1, 1).ToUpper();
            switch (cr)
            {
                case "2":
                    Value = CardValue.Two;
                    break;
                case "3":
                    Value = CardValue.Three;
                    break;
                case "4":
                    Value = CardValue.Four;
                    break;
                case "5":
                    Value = CardValue.Five;
                    break;
                case "6":
                    Value = CardValue.Six;
                    break;
                case "7":
                    Value = CardValue.Seven;
                    break;
                case "8":
                    Value = CardValue.Eight;
                    break;
                case "9":
                    Value = CardValue.Nine;
                    break;
                case "T":
                    Value = CardValue.Ten;
                    break;
                case "J":
                    Value = CardValue.Jack;
                    break;
                case "Q":
                    Value = CardValue.Queen;
                    break;
                case "K":
                    Value = CardValue.King;
                    break;
                case "A":
                    Value = CardValue.Ace;
                    break;
                default:
                    throw new PokerException(cr + " is an invalid card rank.");
            }

            switch (cs)
            {
                case "C":
                    Suit = CardSuit.Clubs;
                    break;
                case "D":
                    Suit = CardSuit.Diamonds;
                    break;
                case "H":
                    Suit = CardSuit.Hearts;
                    break;
                case "S":
                    Suit = CardSuit.Spades;
                    break;
                default:
                    throw new PokerException(cs + " is an invalid card suite.");
            }
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents the current <see cref="AWA.Poker.Card" />.
        ///     The string returned is in the format [rank][suite] where: <br />
        ///     rank is a character in the set [2,3,4,5,6,7,8,9,T,J,Q,K,A], <br />
        ///     and suit is a character in the set [C,D,H,S] <br />
        ///     Example: <br />
        ///     2S => 2 of Spades <br />
        ///     JC => Jack of Clubs <br />
        ///     A joker is a special case string "JJ"
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents the current <see cref="AWA.Poker.Card" />.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            switch (Value)
            {
                case CardValue.Two:
                    sb.Append("2");
                    break;
                case CardValue.Three:
                    sb.Append("3");
                    break;
                case CardValue.Four:
                    sb.Append("4");
                    break;
                case CardValue.Five:
                    sb.Append("5");
                    break;
                case CardValue.Six:
                    sb.Append("6");
                    break;
                case CardValue.Seven:
                    sb.Append("7");
                    break;
                case CardValue.Eight:
                    sb.Append("8");
                    break;
                case CardValue.Nine:
                    sb.Append("9");
                    break;
                case CardValue.Ten:
                    sb.Append("T");
                    break;
                case CardValue.Jack:
                    sb.Append("J");
                    break;
                case CardValue.Queen:
                    sb.Append("Q");
                    break;
                case CardValue.King:
                    sb.Append("K");
                    break;
                case CardValue.Ace:
                    sb.Append("A");
                    break;
                default:
                    throw new PokerException("Card to String error, unknown rank");
            }

            switch (Suit)
            {
                case CardSuit.Clubs:
                    sb.Append("C");
                    break;
                case CardSuit.Diamonds:
                    sb.Append("D");
                    break;
                case CardSuit.Hearts:
                    sb.Append("H");
                    break;
                case CardSuit.Spades:
                    sb.Append("S");
                    break;
                default:
                    throw new PokerException("Card to string error, unknown suit");
            }

            return sb.ToString();
        }
    }

    /// <summary>
    ///     Card suit.
    /// </summary>
    public enum CardSuit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    /// <summary>
    ///     Card rank.
    /// </summary>
    public enum CardValue
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    /// <summary>
    ///     A poker hand. A valid poker hand must have at least 5 cards.
    ///     More cards are allowed to be in the hand to allow implementations
    ///     of game sof poker than involve cards.
    /// </summary>
    public class Hand : IComparable<Hand>
    {
        private static HandComparer _comparer;
        private readonly List<Card> cards;

        public Hand()
        {
            cards = new List<Card>();
        }

        public Hand(string handCards)
        {
            cards = new List<Card>();
            foreach (var c in handCards.Split(" ".ToCharArray())) Add(new Card(c));
        }

        private static HandComparer Comparer
        {
            get
            {
                if (_comparer == null)
                    _comparer = new HandComparer(new PokerHandEvaluator());
                return _comparer;
            }
        }

        public Card[] Cards => cards.ToArray();

        public int CompareTo(Hand other)
        {
            return Comparer.Compare(this, other);
        }

        public void Discard(Card card)
        {
            cards.Remove(card);
        }

        public void Add(Card card)
        {
            cards.Add(card);
        }

        public void Clear()
        {
            cards.Clear();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (Card c in cards)
            {
                sb.Append(" ");
                sb.Append(c);
            }

            return "[" + sb.ToString().TrimStart() + "]";
        }
    }

    /// <summary>
    ///     A comparer class that compares two hands of poker.
    /// </summary>
    public class HandComparer : IComparer<Hand>
    {
        private readonly IPokerHandEvaluator _evaluator;

        public HandComparer(IPokerHandEvaluator evaluator)
        {
            if (evaluator == null) throw new PokerException("Evaluator can not be null.");
            _evaluator = evaluator;
        }

        /// <summary>
        ///     Compares to hands of poker.
        /// </summary>
        /// <param name="x">one hand</param>
        /// <param name="y">the other hand</param>
        /// <returns>
        ///     Less than zero - x is less than y.
        ///     Zero - x equals y.
        ///     Greater than zero - x is greater than y.
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
            for (var i = 0; i < xHand.Length; ++i)
            {
                Card xCard = xHand[i];
                if (i < yHand.Length)
                {
                    Card yCard = yHand[i];
                    if (xCard.Value < yCard.Value) return -1;
                    if (xCard.Value > yCard.Value) return 1;
                }
            }

            return 0;
        }

        private int pairBreak(Hand x, Hand y)
        {
            var xHand = new List<Card>(x.Cards.OrderByDescending(c => c.Value));
            var yHand = new List<Card>(y.Cards.OrderByDescending(c => c.Value));
            IEnumerable<IGrouping<CardValue, Card>> xPair = Pairs(xHand);
            IEnumerable<IGrouping<CardValue, Card>> yPair = Pairs(yHand);
            if (xPair.First().Key == yPair.First().Key)
            {
                //uggg - Remove the pairs, and check high cards.
                foreach (Card xp in xPair.First()) xHand.Remove(xp);
                foreach (Card yp in yPair.First()) yHand.Remove(yp);
                return compareHighCards(GetCardArray(xHand, 3), GetCardArray(yHand, 3));
            }

            if (xPair.First().Key > yPair.First().Key) return 1;
            return -1;
        }

        /// <summary>
        ///     This is going to create an
        /// </summary>
        /// <param name="cards"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private Card[] GetCardArray(IEnumerable<Card> cards, int n)
        {
            if (n > cards.Count()) return cards.ToArray();
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
            var xHand = new List<Card>(x.Cards.OrderBy(c => c.Value));
            var yHand = new List<Card>(y.Cards.OrderBy(c => c.Value));
            IGrouping<CardValue, Card>[] xPair = Pairs(xHand).ToArray();
            IGrouping<CardValue, Card>[] yPair = Pairs(yHand).ToArray();
            for (var i = 0; i < 2; ++i)
                if (xPair[0].Key != yPair[0].Key)
                {
                    if (xPair[0].Key < yPair[0].Key)
                        return -1;
                    return 1;
                }

            if (xHand[4].Value < yHand[4].Value)
                return -1;
            if (xHand[4].Value > yHand[4].Value)
                return 1;
            return 0;
        }

        private int threeOfAKindBreak(Hand x, Hand y)
        {
            CardValue xTripVal = Trips(x.Cards).First().Key;
            CardValue yTripVal = Trips(y.Cards).First().Key;
            if (xTripVal < yTripVal) return -1;
            return 1;
        }

        private int fourOfAKindBreak(Hand x, Hand y)
        {
            CardValue xQuadVal = Quad(x.Cards).First().Key;
            CardValue yQuadVal = Quad(y.Cards).First().Key;
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
            if (ca[0].Value == CardValue.Ace && ca[4].Value == CardValue.Two) return ca.Skip(1).ToArray();
            return ca;
        }

        private int flushBreak(Hand x, Hand y)
        {
            return highCardBreak(x, y);
        }
    }

    /// <summary>
    ///     An enumeration of possible poker hands in rank order, so the higher the enumerated
    ///     value the better the hand.
    /// </summary>
    public enum PokerHand
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    /// <summary>
    ///     The interface for poker hand evaluator. By extracting the interface leaves open the
    ///     possibility of introducing different and perhaps more efficient methods of evaluating
    ///     poker hands. The idea is you pass the evaluator a proper <see cref="AWA.Poker.Hand">hand</see>, and it can tell you
    ///     what kind of hand it is.
    /// </summary>
    public interface IPokerHandEvaluator
    {
        /// <summary>
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        PokerHand Evaluate(Hand h);
    }

    /// <summary>
    ///     A simple implementation of PokerHandEvaluator that should be able to check for
    ///     poker hands from and array of cards that is at least five cards in lenght. This
    ///     evaluator assumes that the hand is valid, and there are not duplicate cards.
    /// </summary>
    public class PokerHandEvaluator : IPokerHandEvaluator
    {
        public PokerHand Evaluate(Hand h)
        {
            var returnVal = PokerHand.HighCard;
            if (h == null || h.Cards.Length < 5)
                throw new PokerException("Invalid poker hand");
            if (IsPair(h, 1)) returnVal = PokerHand.OnePair;
            if (IsPair(h, 2)) returnVal = PokerHand.TwoPair;
            if (OfKindCheck(h, 3)) returnVal = PokerHand.ThreeOfAKind;
            if (OfKindCheck(h, 4)) returnVal = PokerHand.FourOfAKind;
            if (h.Cards.GroupBy(c => c.Value).Count() == 2 && returnVal == PokerHand.ThreeOfAKind)
                returnVal = PokerHand.FullHouse;
            var isStraight = IsStraight(h);
            var isFlush = IsFlush(h);
            if (isStraight) returnVal = PokerHand.Straight;
            if (isFlush) returnVal = PokerHand.Flush;
            if (isStraight && isFlush) returnVal = PokerHand.StraightFlush;
            if (returnVal == PokerHand.StraightFlush)
                if (h.Cards.Where(c => c.Value == CardValue.Ten).Count() > 0 &&
                    h.Cards.Where(c => c.Value == CardValue.Ace).Count() > 0)
                    returnVal = PokerHand.RoyalFlush;
            return returnVal;
        }

        private bool IsPair(Hand hand, int number)
        {
            // Group the cards by value
            IEnumerable<IGrouping<CardValue, Card>> groups = hand.Cards.GroupBy(c => c.Value);

            // If there is more than two cards that match then the hand is not a pair
            if (groups.Count(c => c.Count() > 2) > 0)
                return false;

            // Returns true if the number of groups specified in the parameters matches the number of groups with a pair
            return groups.Count(c => c.Count() == 2) == number;
        }

        private bool OfKindCheck(Hand hand, int number)
        {
            if (hand == null || !hand.Cards.Any())
                return false;

            var isNofKind = hand.Cards
                .GroupBy(c => c.Value)
                .Any(g => g.Count() == number);

            return isNofKind;
        }

        private bool IsStraight(Hand hand)
        {
            Card[] sortedHand = hand.Cards.OrderBy(c => c.Value).ToArray();
            for (var i = 0; i < sortedHand.Length - 1; i++)
                if (sortedHand[i + 1].Value != sortedHand[i].Value + 1)
                    return IsAceLowStraight(sortedHand);
            return true;
        }

        // It is possible for the Ace to be on the bottom end of the straight,
        // as in Ace, 2, 3, 4, 5. This is a special case that would be caught
        // by the IsStraight function but must be checked for.
        private bool IsAceLowStraight(Card[] c)
        {
            return c[0].Value == CardValue.Two &&
                   c[1].Value == CardValue.Three &&
                   c[2].Value == CardValue.Four &&
                   c[3].Value == CardValue.Five && c.Where(v => v.Value == CardValue.Ace).Count() > 0;
        }

        private bool IsFlush(Hand hand)
        {
            // Flush is 5 of the same suited cards. 
            return hand.Cards
                .GroupBy(c => c.Suit)
                .Any(g => g.Count() == 5);
        }
    }

    public class PokerException : Exception
    {
        public PokerException(string msg) : base(msg)
        {
        }
    }
}