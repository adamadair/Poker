using System.Linq;

namespace TexasHoldemBot.Poker
{
    /// <summary>
    /// An enumeration of possible poker hands in rank order, so the higher the enumerated
    /// value the better the hand. 
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
    /// The interface for poker hand evaluator. By extracting the interface leaves open the
    /// possibility of introducing different and perhaps more efficient methods of evaluating
    /// poker hands. The idea is you pass the evaluator a proper <see cref="Hand">hand</see>, and it can tell you 
    /// what kind of hand it is. 
    /// </summary>
    public interface IPokerHandEvaluator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        PokerHand Evaluate(Hand h);
    }

    /// <summary>
    /// A simple implementation of PokerHandEvaluator that should be able to check for 
    /// poker hands from and array of cards that is at least five cards in length. This 
    /// evaluator assumes that the hand is valid, and there are not duplicate cards. 
    /// </summary>
    public class PokerHandEvaluator : IPokerHandEvaluator
    {
        public PokerHand Evaluate(Hand h)
        {
            PokerHand returnVal = PokerHand.HighCard;
            if (h == null || h.Cards.Length < 5)
                throw new PokerException("Invalid poker hand");
            if (IsPair(h, 1)) returnVal = PokerHand.OnePair;
            if (IsPair(h, 2)) returnVal = PokerHand.TwoPair; 
            if (OfKindCheck(h, 3)) returnVal = PokerHand.ThreeOfAKind;
            if (OfKindCheck(h, 4)) returnVal = PokerHand.FourOfAKind;
            if (h.Cards.GroupBy(c => c.Value).Count() == 2 && returnVal == PokerHand.ThreeOfAKind)
                returnVal = PokerHand.FullHouse; 
            bool isStraight = IsStraight(h);
            bool isFlush = IsFlush(h);
            if (isStraight) returnVal = PokerHand.Straight;
            if (isFlush) returnVal = PokerHand.Flush;
            if (isStraight && isFlush) returnVal = PokerHand.StraightFlush;
            if (returnVal == PokerHand.StraightFlush)
            {
                // Checking for the Royalflush. If it is a straight flush and contains both
                // the ten and ace cards.
                if (h.Cards.Where(c => c.Value == CardValue.Ten).Count() > 0 &&
                    h.Cards.Where(c => c.Value == CardValue.Ace).Count() > 0)
                    returnVal = PokerHand.RoyalFlush;
            }            
            return returnVal;
        }
        
        private bool IsPair(Hand hand, int number)
        {
            // Group the cards by value
            var groups = hand.Cards.GroupBy(c => c.Value);

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

            bool isNofKind = hand.Cards
                .GroupBy(c => c.Value)
                .Any(g => g.Count() == number);

            return isNofKind;
        }

        private bool IsStraight(Hand hand)
        {            
            Card[] sortedHand = hand.Cards.OrderBy(c => c.Value).ToArray();
            for (int i = 0; i < sortedHand.Length - 1; i++)
            {
                if (sortedHand[i + 1].Value != sortedHand[i].Value + 1)
                    return IsAceLowStraight(sortedHand);
            }
            return true;
        }

        // It is possible for the Ace to be on the bottom end of the straight,
        // as in Ace, 2, 3, 4, 5. This is a special case that would be caught
        // by the IsStraight function but must be checked for.
        private bool IsAceLowStraight(Card[] c)
        {
            return (c[0].Value == CardValue.Two &&
                c[1].Value == CardValue.Three &&
                c[2].Value == CardValue.Four &&
                c[3].Value == CardValue.Five && c.Where(v => v.Value == CardValue.Ace).Count() > 0);                
        }

        private bool IsFlush(Hand hand)
        {
            // Flush is 5 of the same suited cards. 
            return hand.Cards
                .GroupBy(c => c.Suit)
                .Any(g => g.Count() == 5);
        }
    }
}
