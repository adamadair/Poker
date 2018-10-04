using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemBot.Poker
{
    /// <summary>
    /// 
    /// </summary>
    public class BrecherHandEvaluator : IPokerHandEvaluator
    {
        public PokerHand Evaluate(Hand h)
        {            
            int strength = GetHandStrength(h);
            if (strength < BrecherHandEval.PAIR)
                return PokerHand.HighCard;
            if (strength < BrecherHandEval.TWO_PAIR)
                return PokerHand.OnePair;
            if (strength < BrecherHandEval.THREE_OF_A_KIND)
                return PokerHand.TwoPair;
            if (strength < BrecherHandEval.STRAIGHT)
                return PokerHand.ThreeOfAKind;
            if (strength < BrecherHandEval.FLUSH)
                return PokerHand.Straight;
            if (strength < BrecherHandEval.FULL_HOUSE)
                return PokerHand.Flush;
            if (strength < BrecherHandEval.FOUR_OF_A_KIND)
                return PokerHand.FullHouse;
            if (strength < BrecherHandEval.STRAIGHT_FLUSH)
                return PokerHand.FourOfAKind;

            if (h.Cards.Any(c => c.Value == CardValue.Ten) &&
                h.Cards.Any(c => c.Value == CardValue.Jack) &&
                h.Cards.Any(c => c.Value == CardValue.Queen) &&
                h.Cards.Any(c => c.Value == CardValue.King) &&
                h.Cards.Any(c => c.Value == CardValue.Ace))
                return PokerHand.RoyalFlush;

            return PokerHand.StraightFlush;
        }

        public static float WinningHandPercent(Card[] playerCards, Card[] tableCards)
        {
            
            return 0.0f;
        }

        public static int GetHandStrength(Hand h)
        {
            return GetHandStrength(HandToCode(h), h.Cards.Length);
        }

        private static int GetHandStrength(long handCode, int handSize)
        {
            switch (handSize)
            {
                case 5:
                    return BrecherHandEval.Hand5Eval(handCode);
                case 6:
                    return BrecherHandEval.Hand6Eval(handCode);
                case 7:
                    return BrecherHandEval.Hand7Eval(handCode);
                default:
                    return 0;
            }
        }

        public static long HandToCode(Hand h)
        {
            long handCode = h.Cards.Sum(card => CardToCode(card));
            return handCode;
        }

        public static long CardToCode(Card c)
        {
            return 1L << (16 * SuitToNumber(c.Suit) + ValueToNumber(c.Value));
        }

        private static int SuitToNumber(CardSuit s)
        {
            switch (s)
            {
                case CardSuit.Spades:
                    return 0;
                case CardSuit.Hearts:
                    return 1;
                case CardSuit.Clubs:
                    return 2;
                case CardSuit.Diamonds:
                    return 3;
            }

            return 0;
        }

        private static int ValueToNumber(CardValue v)
        {
            switch (v)
            {
                case CardValue.Two: return 0;
                case CardValue.Three: return 1;
                case CardValue.Four: return 2;
                case CardValue.Five: return 3;
                case CardValue.Six: return 4;
                case CardValue.Seven: return 5;
                case CardValue.Eight: return 6;
                case CardValue.Nine: return 7;
                case CardValue.Ten: return 8;
                case CardValue.Jack: return 9;
                case CardValue.Queen: return 10;
                case CardValue.King: return 11;
                case CardValue.Ace: return 12;
            }
            return 0;
        }
    }


    /**
     * BrecherHandEval is a conversion of the HandEval.java code to C#. The Java code
     * was a conversion of a C library written by Steve Brecher.
     *
     * You can find the original Java code here:
     * https://github.com/theaigames/texasholdem-engine/blob/master/com/stevebrecher/HandEval.java
     *
     * Non-instantiable class containing a variety of static poker hand evaluation and related utility methods.
     * <p>
     * All of the methods are thread-safe.
     * <p>
     * Each evaluation method takes a single parameter representing a hand of five to
     * seven cards represented within a long (64 bits).  The long is considered as
     * composed of four 16-bit fields, one for each suit.  The ordering of these
     * 16-bit fields within the long, i.e., the correspondence of each to a specific
     * suit, is immaterial.  Within each suit's 16-bit field, the least-significant
     * 13 bits (masked by 0x1FFF) are flags representing the presence of ranks in
     * that suit, where bit 0 set (0x0001) for a deuce, ..., bit 12 set (0x1000) for
     * an ace.  The values of the unused most-significant three bits within each
     * 16-bit suit field are immaterial.
     * <p>
     * A hand parameter can be built by encoding a {@link CardSet} or by bitwise
     * OR-ing, or adding, the encoded values of individual {@link Card}s.  These
     * encodings are returned by an {@link #encode encode} method.
     * <p>
     * Different methods are used for high and for lowball evaluation.
     * <p>
     * For high evaluation if results R1 > R2, hand 1 beats hand 2;
     * for lowball evaluation if results R1 > R2, hand 2 beats hand 1.
     * <p>
     * Evaluation result in 32 bits = 0x0V0RRRRR where V, R are
     * hex digits or "nybbles" (half-bytes).
     * <p>
     * V nybble = category code ranging from {@link HandCategory#NO_PAIR}<code>.ordinal()</code>
     *                                    to {@link HandCategory#STRAIGHT_FLUSH}<code>.ordinal()</code>
     * <p>
     * The R nybbles are the significant ranks (0..12), where 0 is the deuce
     * in a high result (Ace is 12, 0xC), and for lowball 0 is the Ace
     * (King is 0xC).  The Rs may be considered to consist of Ps for ranks
     * which determine the primary value of the hand, and Ks for kickers
     * where applicable.  Ordering is left-to-right:  first the Ps, then
     * any Ks, then padding with 0s.  Because 0 is a valid rank, to
     * interpret a result you must know how many ranks are significant,
     * which is a function of the hand category and whether high or lowball.
     * Examples: for a one-pair hand there are four significant ranks,
     * that of the pair and of the three kickers; for a straight, there is
     * one significant rank, that of the highest in the hand.
     * <p>
     * Common-card (board) games are assumed in determining the number of
     * significant ranks.  For example, a kicker value is returned for quads even
     * though it wouldn't be significant in a draw game.
     * <p><pre>
     * Examples of ...Eval method results (high where not indicated):
     *  Royal flush: 0x080C0000
     *  Four of a kind, Queens, with a 5 kicker:  0x070A3000
     *  Threes full of eights:  0x06016000
     *  Straight to the five (wheel): 0x04030000 (high)
     *  Straight to the five (wheel): 0x04040000 (lowball)
     *  One pair, deuces (0x0), with A65: 0x0100C430 (high)
     *  One pair, deuces (0x1), with 65A: 0x01015400 (lowball)
     *  No pair, KJT85: 0x000B9863
     *  Razz, wheel:  0x00043210</pre>
     * For the eight-or-better lowball ..._Eval functions, the result is
     * either as above or the constant {@link #NO_8_LOW}.  NO_8_LOW > any other
     * ..._Eval function result.
     * <p>
     * @version 2010Jun25.1
     * @author Steve Brecher
     *
     */
    internal static class BrecherHandEval
    {
        private const int _RANK_SHIFT_1 = 4;
        private const int _RANK_SHIFT_2 = _RANK_SHIFT_1 + 4;
        private const int _RANK_SHIFT_3 = _RANK_SHIFT_2 + 4;
        private const int _RANK_SHIFT_4 = _RANK_SHIFT_3 + 4;
        public const int VALUE_SHIFT = _RANK_SHIFT_4 + 8;

        public const int NO_PAIR = 0;
        public const int PAIR = NO_PAIR + (1 << VALUE_SHIFT);
        public const int TWO_PAIR = PAIR + (1 << VALUE_SHIFT);
        public const int THREE_OF_A_KIND = TWO_PAIR + (1 << VALUE_SHIFT);
        public const int STRAIGHT = THREE_OF_A_KIND + (1 << VALUE_SHIFT);
        public const int FLUSH = STRAIGHT + (1 << VALUE_SHIFT);
        public const int FULL_HOUSE = FLUSH + (1 << VALUE_SHIFT);
        public const int FOUR_OF_A_KIND = FULL_HOUSE + (1 << VALUE_SHIFT);
        public const int STRAIGHT_FLUSH = FOUR_OF_A_KIND + (1 << VALUE_SHIFT);
        public const int NO_8_LOW = STRAIGHT_FLUSH + (1 << VALUE_SHIFT);

        private const int _ARRAY_SIZE = 0x1FC0 + 1;           // all combos of up to 7 of LS 13 bits on
        /* Arrays for which index is bit mask of card ranks in hand: */
        private static readonly int[] StraightValue = new int[_ARRAY_SIZE]; // Value(STRAIGHT) | (straight's high card rank-2 (3..12) << RANK_SHIFT_4); 0 if no straight
        private static readonly int[] NbrOfRanks = new int[_ARRAY_SIZE];    // count of bits set
        private static readonly int[] HiRank = new int[_ARRAY_SIZE];    // 4-bit card rank of highest bit set, right justified
        private static readonly int[] HiUpTo5Ranks = new int[_ARRAY_SIZE];  // 4-bit card ranks of highest (up to) 5 bits set, right-justified
        private static readonly int[] LoMaskOrNo8Low = new int[_ARRAY_SIZE];    // low-order 5 of the low-order 8 bits set, or NO_8_LOW; Ace is LS bit.
        private static readonly int[] Lo3_8ObRanksMask = new int[_ARRAY_SIZE];   // bits other than lowest 3 8-or-better reset; Ace is LS bit.

        private static int FlushAndOrStraight7(int ranks, int c, int d, int h, int s)
        {

            int i, j;

            if ((j = NbrOfRanks[c]) > 7 - 5)
            {
                // there's either a club flush or no flush
                if (j >= 5)
                    if ((i = StraightValue[c]) == 0)
                        return FLUSH | HiUpTo5Ranks[c];
                    else
                        return (STRAIGHT_FLUSH - STRAIGHT) + i;
            }
            else if ((j += (i = NbrOfRanks[d])) > 7 - 5)
            {
                if (i >= 5)
                    if ((i = StraightValue[d]) == 0)
                        return FLUSH | HiUpTo5Ranks[d];
                    else
                        return (STRAIGHT_FLUSH - STRAIGHT) + i;
            }
            else if ((j += (i = NbrOfRanks[h])) > 7 - 5)
            {
                if (i >= 5)
                    if ((i = StraightValue[h]) == 0)
                        return FLUSH | HiUpTo5Ranks[h];
                    else
                        return (STRAIGHT_FLUSH - STRAIGHT) + i;
            }
            else
                /* total cards in other suits <= 7-5: spade flush: */
            if ((i = StraightValue[s]) == 0)
                return FLUSH | HiUpTo5Ranks[s];
            else
                return (STRAIGHT_FLUSH - STRAIGHT) + i;
            return StraightValue[ranks];
        }

        public static int Hand7Eval(long hand)
        {
            int i, j, ranks;

            /*
             * The parameter contains four 16-bit fields; in each, the low-order
             * 13 bits are significant.  Get the respective fields into variables.
             * We don't care which suit is which; we arbitrarily call them c,d,h,s.
             */
            var c = (int)hand & 0x1FFF;
            var d = (int)((ulong)hand >> 16) & 0x1FFF;
            var h = (int)((ulong)hand >> 32) & 0x1FFF;
            var s = (int)((ulong)hand >> 48) & 0x1FFF;

            switch (NbrOfRanks[ranks = c | d | h | s])
            {

                case 2:
                    /*
                     * quads with trips kicker
                     */
                    i = c & d & h & s; /* bit for quads */
                    return FOUR_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[i ^ ranks] << _RANK_SHIFT_3);

                case 3:
                    /*
                     * trips and pair (full house) with non-playing pair,
                     * or two trips (full house) with non-playing singleton,
                     * or quads with pair and singleton
                     */
                    /* bits for singleton, if any, and trips, if any: */
                    if (NbrOfRanks[i = c ^ d ^ h ^ s] == 3)
                    {
                        /* two trips (full house) with non-playing singleton */
                        if (NbrOfRanks[i = c & d] != 2)
                            if (NbrOfRanks[i = c & h] != 2)
                                if (NbrOfRanks[i = c & s] != 2)
                                    if (NbrOfRanks[i = d & h] != 2)
                                        if (NbrOfRanks[i = d & s] != 2)
                                            i = h & s; /* bits for the trips */
                        return FULL_HOUSE | (HiUpTo5Ranks[i] << _RANK_SHIFT_3);
                    }
                    if ((j = c & d & h & s) != 0) /* bit for quads */
                                                  /* quads with pair and singleton */
                        return FOUR_OF_A_KIND | (HiRank[j] << _RANK_SHIFT_4) | (HiRank[ranks ^ j] << _RANK_SHIFT_3);
                    /* trips and pair (full house) with non-playing pair */
                    return FULL_HOUSE | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[ranks ^ i] << _RANK_SHIFT_3);

                case 4:
                    /*
                     * three pair and singleton,
                     * or trips and pair (full house) and two non-playing singletons,
                     * or quads with singleton kicker and two non-playing singletons
                     */
                    i = c ^ d ^ h ^ s; // the bit(s) of the trips, if any, and singleton(s)
                    if (NbrOfRanks[i] == 1)
                    {
                        /* three pair and singleton */
                        j = HiUpTo5Ranks[ranks ^ i];    /* ranks of the three pairs */
                        return TWO_PAIR | ((j & 0x0FF0) << _RANK_SHIFT_2) | (HiRank[i | (1 << (j & 0x000F))] << _RANK_SHIFT_2);
                    }
                    if ((j = c & d & h & s) == 0)
                    {
                        // trips and pair (full house) and two non-playing singletons
                        i ^= ranks; /* bit for the pair */
                        if ((j = (c & d) & (~i)) == 0)
                            j = (h & s) & (~i); /* bit for the trips */
                        return FULL_HOUSE | (HiRank[j] << _RANK_SHIFT_4) | (HiRank[i] << _RANK_SHIFT_3);
                    }
                    // quads with singleton kicker and two non-playing singletons
                    return FOUR_OF_A_KIND | (HiRank[j] << _RANK_SHIFT_4) | (HiRank[i] << _RANK_SHIFT_3);

                case 5:
                    /*
                     * flush and/or straight,
                     * or two pair and three singletons,
                     * or trips and four singletons
                     */
                    if ((i = FlushAndOrStraight7(ranks, c, d, h, s)) != 0)
                        return i;
                    i = c ^ d ^ h ^ s; // the bits of the trips, if any, and singletons
                    if (NbrOfRanks[i] != 5)
                        /* two pair and three singletons */
                        return TWO_PAIR | (HiUpTo5Ranks[i ^ ranks] << _RANK_SHIFT_3) | (HiRank[i] << _RANK_SHIFT_2);
                    /* trips and four singletons */
                    if ((j = c & d) == 0)
                        j = h & s;
                    // j has trips bit
                    return THREE_OF_A_KIND | (HiRank[j] << _RANK_SHIFT_4) | (HiUpTo5Ranks[i ^ j] & 0x0FF00);

                case 6:
                    /*
                     * flush and/or straight,
                     * or one pair and three kickers and two non-playing singletons
                     */
                    if ((i = FlushAndOrStraight7(ranks, c, d, h, s)) != 0)
                        return i;
                    i = c ^ d ^ h ^ s; /* the bits of the five singletons */
                    return PAIR | (HiRank[ranks ^ i] << _RANK_SHIFT_4) | ((HiUpTo5Ranks[i] & 0x0FFF00) >> _RANK_SHIFT_1);

                case 7:
                    /*
                     * flush and/or straight or no pair
                     */
                    if ((i = FlushAndOrStraight7(ranks, c, d, h, s)) != 0)
                        return i;
                    return NO_PAIR | HiUpTo5Ranks[ranks];

            } /* end switch */

            return 0; /* never reached, but avoids compiler warning */
        }

        private static int FlushAndOrStraight6(int ranks, int c, int d, int h, int s)
        {

            int i, j;

            if ((j = NbrOfRanks[c]) > 6 - 5)
            {
                // there's either a club flush or no flush
                if (j >= 5)
                    if ((i = StraightValue[c]) == 0)
                        return FLUSH | HiUpTo5Ranks[c];
                    else
                        return (STRAIGHT_FLUSH - STRAIGHT) + i;
            }
            else if ((j += (i = NbrOfRanks[d])) > 6 - 5)
            {
                if (i >= 5)
                    if ((i = StraightValue[d]) == 0)
                        return FLUSH | HiUpTo5Ranks[d];
                    else
                        return (STRAIGHT_FLUSH - STRAIGHT) + i;
            }
            else if ((j += (i = NbrOfRanks[h])) > 6 - 5)
            {
                if (i >= 5)
                    if ((i = StraightValue[h]) == 0)
                        return FLUSH | HiUpTo5Ranks[h];
                    else
                        return (STRAIGHT_FLUSH - STRAIGHT) + i;
            }
            else
              /* total cards in other suits <= N-5: spade flush: */
              if ((i = StraightValue[s]) == 0)
                return FLUSH | HiUpTo5Ranks[s];
            else
                return (STRAIGHT_FLUSH - STRAIGHT) + i;
            return StraightValue[ranks];
        }

        /**
         * Returns the value of the best 5-card high poker hand from 6 cards.
         * @param hand bit mask with one bit set for each of 6 cards.
         * @return the value of the best 5-card high poker hand.
         */
        
        public static int Hand6Eval(long hand)
        {

            var c = (int)hand & 0x1FFF;
            var d = (int)((ulong)hand >> 16) & 0x1FFF;
            var h = (int)((ulong)hand >> 32) & 0x1FFF;
            var s = (int)((ulong)hand >> 48) & 0x1FFF;

            var ranks = c | d | h | s;
            int i;

            switch (NbrOfRanks[ranks])
            {

                case 2: /* quads with pair kicker,
					   or two trips (full house) */
                        /* bits for trips, if any: */
                    if ((NbrOfRanks[i = c ^ d ^ h ^ s]) != 0)
                        /* two trips (full house) */
                        return FULL_HOUSE | (HiUpTo5Ranks[i] << _RANK_SHIFT_3);
                    /* quads with pair kicker */
                    i = c & d & h & s;  /* bit for quads */
                    return FOUR_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[i ^ ranks] << _RANK_SHIFT_3);

                case 3: /* quads with singleton kicker and non-playing singleton,
					   or full house with non-playing singleton,
					   or two pair with non-playing pair */
                    if ((c ^ d ^ h ^ s) == 0)
                        /* no trips or singletons:  three pair */
                        return TWO_PAIR | (HiUpTo5Ranks[ranks] << _RANK_SHIFT_2);
                    if ((i = c & d & h & s) == 0)
                    {
                        /* full house with singleton */
                        if ((i = c & d & h) == 0)
                            if ((i = c & d & s) == 0)
                                if ((i = c & h & s) == 0)
                                    i = d & h & s; /* bit of trips */
                        var j = c ^ d ^ h ^ s;
                        return FULL_HOUSE | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[j ^ ranks] << _RANK_SHIFT_3);
                    }
                    /* quads with kicker and singleton */
                    return FOUR_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[i ^ ranks] << _RANK_SHIFT_3);

                case 4: /* trips and three singletons,
					   or two pair and two singletons */
                    if ((i = c ^ d ^ h ^ s) != ranks)
                        /* two pair and two singletons */
                        return TWO_PAIR | (HiUpTo5Ranks[i ^ ranks] << _RANK_SHIFT_3) | (HiRank[i] << _RANK_SHIFT_2);
                    /* trips and three singletons */
                    if ((i = c & d) == 0)
                        i = h & s; /* bit of trips */
                    return THREE_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4) | ((HiUpTo5Ranks[ranks ^ i] & 0x00FF0) << _RANK_SHIFT_1);

                case 5: /* flush and/or straight,
					   or one pair and three kickers and
					    one non-playing singleton */
                    if ((i = FlushAndOrStraight6(ranks, c, d, h, s)) != 0)
                        return i;
                    i = c ^ d ^ h ^ s; /* the bits of the four singletons */
                    return PAIR | (HiRank[i ^ ranks] << _RANK_SHIFT_4) | (HiUpTo5Ranks[i] & 0x0FFF0);

                case 6: /* flush and/or straight or no pair */
                    if ((i = FlushAndOrStraight6(ranks, c, d, h, s)) != 0)
                        return i;
                    return NO_PAIR | HiUpTo5Ranks[ranks];

            } /* end switch */

            return 0; /* never reached, but avoids compiler warning */
        }

        /**
         * Returns the value of a 5-card poker hand.
         * @param hand bit mask with one bit set for each of 5 cards.
         * @return the value of the hand.
         */
        public static int Hand5Eval(long hand)
        {

            var c = (int)hand & 0x1FFF;
            var d = (int)((ulong)hand >> 16) & 0x1FFF;
            var h = (int)((ulong)hand >> 32) & 0x1FFF;
            var s = (int)((ulong)hand >> 48) & 0x1FFF;

            var ranks = c | d | h | s;
            int i;

            switch (NbrOfRanks[ranks])
            {

                case 2: /* quads or full house */
                    i = c & d;              /* any two suits */
                    if ((i & h & s) == 0)
                    { /* no bit common to all suits */
                        i = c ^ d ^ h ^ s;  /* trips bit */
                        return FULL_HOUSE | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[i ^ ranks] << _RANK_SHIFT_3);
                    }
                    else
                        /* the quads bit must be present in each suit mask,
	                       but the kicker bit in no more than one; so we need
	                       only AND any two suit masks to get the quad bit: */
                        return FOUR_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[i ^ ranks] << _RANK_SHIFT_3);

                case 3: /* trips and two kickers,
	                   or two pair and kicker */
                    if ((i = c ^ d ^ h ^ s) == ranks)
                    {
                        /* trips and two kickers */
                        if ((i = c & d) == 0)
                            if ((i = c & h) == 0)
                                i = d & h;
                        return THREE_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4)
                                | (HiUpTo5Ranks[i ^ ranks] << _RANK_SHIFT_2);
                    }
                    /* two pair and kicker; i has kicker bit */
                    return TWO_PAIR | (HiUpTo5Ranks[i ^ ranks] << _RANK_SHIFT_3) | (HiRank[i] << _RANK_SHIFT_2);

                case 4: /* pair and three kickers */
                    i = c ^ d ^ h ^ s; /* kicker bits */
                    return PAIR | (HiRank[ranks ^ i] << _RANK_SHIFT_4) | (HiUpTo5Ranks[i] << _RANK_SHIFT_1);

                case 5: /* flush and/or straight, or no pair */
                    if ((i = StraightValue[ranks]) == 0)
                        i = HiUpTo5Ranks[ranks];
                    if (c != 0)
                    {           /* if any clubs... */
                        if (c != ranks)     /*   if no club flush... */
                            return i;
                    }       /*      return straight or no pair value */
                    else
                    if (d != 0)
                    {
                        if (d != ranks)
                            return i;
                    }
                    else
                    if (h != 0)
                    {
                        if (h != ranks)
                            return i;
                    }
                    /*	else s == ranks: spade flush */
                    /* There is a flush */
                    if (i < STRAIGHT)
                        /* no straight */
                        return FLUSH | i;
                    else
                        return (STRAIGHT_FLUSH - STRAIGHT) + i;
            }
            return 0; /* never reached, but avoids compiler warning */
        }
        /** ********** Initialization ********************** */

        private const int ACE_RANK = 14;

        private const int A5432 = 0x0000100F; // A5432

        // initializer block
        static BrecherHandEval()
        {
            int mask;
            for (mask = 1; mask < _ARRAY_SIZE; ++mask)
            {
                int ranks;
                var bitCount = ranks = 0;
                var shiftReg = mask;
                int i;
                for (i = ACE_RANK - 2; i >= 0; --i, shiftReg <<= 1)
                    if ((shiftReg & 0x1000) != 0)
                        if (++bitCount <= 5)
                        {
                            ranks <<= _RANK_SHIFT_1;
                            ranks += i;
                            if (bitCount == 1)
                                HiRank[mask] = i;
                        }

                HiUpTo5Ranks[mask] = ranks;
                NbrOfRanks[mask] = bitCount;

                LoMaskOrNo8Low[mask] = NO_8_LOW;
                int value;
                bitCount = value = 0;
                shiftReg = mask;
                /* For the purpose of this loop, Ace is low; it's in the LS bit */
                for (i = 0; i < 8; ++i, shiftReg >>= 1)
                    if ((shiftReg & 1) != 0)
                    {
                        value |= (1 << i); /* undo previous shifts, copy bit */
                        if (++bitCount == 3)
                            Lo3_8ObRanksMask[mask] = value;
                        if (bitCount == 5)
                        {
                            LoMaskOrNo8Low[mask] = value;
                            break;
                        }
                    }
            }

            for (mask = 0x1F00 /* A..T */; mask >= 0x001F /* 6..2 */; mask >>= 1)
                SetStraight(mask);
            SetStraight(A5432); /* A,5..2 */
        }

        private static void SetStraight(int ts)
        {
            /* must call with ts from A..T to 5..A in that order */

            int i, j;

            for (i = 0x1000; i > 0; i >>= 1)
            for (j = 0x1000; j > 0; j >>= 1)
            {
                var es = ts | i | j;
                if (StraightValue[es] == 0)
                    if (ts == A5432)
                        StraightValue[es] = STRAIGHT | ((5 - 2) << _RANK_SHIFT_4);
                    else
                        StraightValue[es] = STRAIGHT | (HiRank[ts] << _RANK_SHIFT_4);
            }
        }
    }
}
