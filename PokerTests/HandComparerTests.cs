using NUnit.Framework;
using System;
using AWA.Poker;

namespace PokerTests
{
    [TestFixture()]
    public class HandComparerTests
    {
        public const string NO_HAND_1 = "2C 3C 8H 9S JD";
        public const string NO_HAND_2 = "4H KD QS 8C 7C";
        public const string NO_HAND_3 = "AS KH 7C 3D 9H";
        public const string NO_HAND_4 = "AH KC 7H 3S 9D";
        [Test()]
        public void HandComparerHighCardTest()
        {
            HandComparer hc = new HandComparer(new PokerHandEvaluator());
            var h1 = new Hand(NO_HAND_1);
            var h2 = new Hand(NO_HAND_2);
            var h3 = new Hand(NO_HAND_3);
            var h4 = new Hand(NO_HAND_4);
            // King of Diamonds beats Jack of Diamond
            Assert.IsTrue(hc.Compare(h1, h2) < 0);
            Assert.IsTrue(hc.Compare(h2, h1) > 0);
            // Ace beats King
            Assert.IsTrue(hc.Compare(h2, h3) < 0);
            Assert.IsTrue(hc.Compare(h3, h2) > 0);
            // These are equal
            Assert.IsTrue(hc.Compare(h3, h4) == 0);
        }

        public const string PAIR_HAND_1 = "4C 4H 8H 9S TD"; // Pair of fours
        public const string PAIR_HAND_2 = "JD JC 5C AC 2H"; // Pair of Jacks
        public const string PAIR_HAND_3 = "QC QH KH 3D 7S";
        public const string PAIR_HAND_4 = "QC QH AH 3S 7H";
        public const string PAIR_HAND_5 = "4D 4S 8C 9D TS";

        [Test()]
        public void HandComparerOnePairTest()
        {
            HandComparer hc = new HandComparer(new PokerHandEvaluator());
            var h1 = new Hand(NO_HAND_1);
            var h2 = new Hand(NO_HAND_2);
            var p1 = new Hand(PAIR_HAND_1);
            var p2 = new Hand(PAIR_HAND_2);
            var p3 = new Hand(PAIR_HAND_3);
            var p4 = new Hand(PAIR_HAND_4);
            var p5 = new Hand(PAIR_HAND_5);
            Assert.IsTrue(hc.Compare(h1, p1) < 0);
            Assert.IsTrue(hc.Compare(p1, h1) > 0);
            Assert.IsTrue(hc.Compare(h2, p2) < 0);
            Assert.IsTrue(hc.Compare(p2, h2) > 0);
            Assert.IsTrue(hc.Compare(p1, p2) < 0);
            Assert.IsTrue(hc.Compare(p2, p1) > 0);
            Assert.IsTrue(hc.Compare(p2, p3) < 0);
            Assert.IsTrue(hc.Compare(p3, p2) > 0);
            Assert.IsTrue(hc.Compare(p3, p4) < 0);
            Assert.IsTrue(hc.Compare(p4, p3) > 0);
            Assert.IsTrue(hc.Compare(p1, p5) == 0);
        }

        public const string E54_1 = "6D 7C 5D 5H 3S"; 
        public const string E54_2 = "5C JC 2H 5S 3D";
        [Test()]
        public void HandComparerEulerErrorFixTest()
        {
            HandComparer hc = new HandComparer(new PokerHandEvaluator());
            var h1 = new Hand(E54_1);
            var h2 = new Hand(E54_2);
            Assert.IsTrue(hc.Compare(h1, h2) < 0);
        }

        const string TWO_PAIR_HAND_1 = "5C 5S 7C 7H QH";
        const string TWO_PAIR_HAND_2 = "5D 5H 7S 7D QD";
        const string TWO_PAIR_HAND_3 = "5D 5H 7S 7D KD";
        const string TWO_PAIR_HAND_4 = "4D 4H 9S 9D KH";
        [Test()]
        public void HandComparerTwoPairTest()
        {
            HandComparer hc = new HandComparer(new PokerHandEvaluator());
            var h1 = new Hand(NO_HAND_1);
            var h2 = new Hand(NO_HAND_2);
            var p1 = new Hand(PAIR_HAND_1);
            var p2 = new Hand(PAIR_HAND_2);
            var t1 = new Hand(TWO_PAIR_HAND_1);
            var t2 = new Hand(TWO_PAIR_HAND_2);
            var t3 = new Hand(TWO_PAIR_HAND_3);
            var t4 = new Hand(TWO_PAIR_HAND_4);
            Assert.IsTrue(hc.Compare(h1, t1) < 0);
            Assert.IsTrue(hc.Compare(t1, h1) > 0);
            Assert.IsTrue(hc.Compare(h2, t2) < 0);
            Assert.IsTrue(hc.Compare(t2, h2) > 0);
            Assert.IsTrue(hc.Compare(p1, t1) < 0);
            Assert.IsTrue(hc.Compare(t1, p1) > 0);
            Assert.IsTrue(hc.Compare(p2, t2) < 0);
            Assert.IsTrue(hc.Compare(t2, p2) > 0);
            Assert.IsTrue(hc.Compare(t1, t2) == 0);
            Assert.IsTrue(hc.Compare(t1, t3) < 0);
            Assert.IsTrue(hc.Compare(t3, t1) > 0);
            Assert.IsTrue(hc.Compare(t3, t4) < 0);
            Assert.IsTrue(hc.Compare(t4, t3) > 0);
        }

        public const string TOAK_HAND_1 = "6C 6S 6D QD AS";
        public const string TOAK_HAND_2 = "7C 7S 7D JD TS";
        [Test()]
        public void HandComparerThreeOfAKindTest()
        {
            HandComparer hc = new HandComparer(new PokerHandEvaluator());
            var h1 = new Hand(NO_HAND_1);
            var h2 = new Hand(NO_HAND_2);
            var p1 = new Hand(PAIR_HAND_1);
            var p2 = new Hand(PAIR_HAND_2);
            var t1 = new Hand(TWO_PAIR_HAND_1);
            var t2 = new Hand(TWO_PAIR_HAND_2);
            var tk1 = new Hand(TOAK_HAND_1);
            var tk2 = new Hand(TOAK_HAND_2);
            Assert.IsTrue(hc.Compare(h1, tk1) < 0);
            Assert.IsTrue(hc.Compare(tk1, h1) > 0);
            Assert.IsTrue(hc.Compare(h2, tk2) < 0);
            Assert.IsTrue(hc.Compare(tk2, h2) > 0);
            Assert.IsTrue(hc.Compare(p1, tk1) < 0);
            Assert.IsTrue(hc.Compare(tk1, p1) > 0);
            Assert.IsTrue(hc.Compare(p2, tk2) < 0);
            Assert.IsTrue(hc.Compare(tk2, p2) > 0);
            Assert.IsTrue(hc.Compare(t1, tk1) < 0);
            Assert.IsTrue(hc.Compare(tk1, t1) > 0);
            Assert.IsTrue(hc.Compare(t2, tk2) < 0);
            Assert.IsTrue(hc.Compare(tk2, t2) > 0);
            Assert.IsTrue(hc.Compare(tk1, tk2) < 0);
            Assert.IsTrue(hc.Compare(tk2, tk1) > 0);
        }

        public const string STRAIGHT_HAND1 = "2C 3D 4C 5C 6C";
        public const string STRAIGHT_HAND2 = "AC 2D 3H 4D 5D";
        public const string STRAIGHT_HAND3 = "TC JD QH KD AD";
        [Test()]
        public void HandComparerStraightTest()
        {
            HandComparer hc = new HandComparer(new PokerHandEvaluator());
            var h1 = new Hand(NO_HAND_1);
            var h2 = new Hand(NO_HAND_2);
            var p1 = new Hand(PAIR_HAND_1);
            var p2 = new Hand(PAIR_HAND_2);
            var t1 = new Hand(TWO_PAIR_HAND_1);
            var t2 = new Hand(TWO_PAIR_HAND_2);
            var tk1 = new Hand(TOAK_HAND_1);
            var tk2 = new Hand(TOAK_HAND_2);
            var s1 = new Hand(STRAIGHT_HAND1);
            var s2 = new Hand(STRAIGHT_HAND2);
            var s3 = new Hand(STRAIGHT_HAND3);
            Assert.IsTrue(hc.Compare(h1, s1) < 0);
            Assert.IsTrue(hc.Compare(s1, h1) > 0);
            Assert.IsTrue(hc.Compare(h2, s2) < 0);
            Assert.IsTrue(hc.Compare(s2, h2) > 0);
            Assert.IsTrue(hc.Compare(p1, s1) < 0);
            Assert.IsTrue(hc.Compare(s1, p1) > 0);
            Assert.IsTrue(hc.Compare(p2, s2) < 0);
            Assert.IsTrue(hc.Compare(s2, p2) > 0);
            Assert.IsTrue(hc.Compare(t1, s1) < 0);
            Assert.IsTrue(hc.Compare(s1, t1) > 0);
            Assert.IsTrue(hc.Compare(t2, s2) < 0);
            Assert.IsTrue(hc.Compare(s2, t2) > 0);
            Assert.IsTrue(hc.Compare(s2, s1) < 0);
            Assert.IsTrue(hc.Compare(s1, s2) > 0);
            Assert.IsTrue(hc.Compare(s2, s3) < 0);
            Assert.IsTrue(hc.Compare(s3, s2) > 0);
            Assert.IsTrue(hc.Compare(tk1, s1) < 0);
            Assert.IsTrue(hc.Compare(s1, tk1) > 0);
            Assert.IsTrue(hc.Compare(tk2, s1) < 0);
            Assert.IsTrue(hc.Compare(s1, tk2) > 0);
        }


        public const string FLUSH_HAND_1 = "3C 4C 7C 9C TC";
        public const string FLUSH_HAND_2 = "4D 7D 9D TD KD";
        public const string FLUSH_HAND_3 = "4S 7S 9S TS KS";

        [Test()]
        public void HandComparerFlushTest()
        {
            HandComparer hc = new HandComparer(new PokerHandEvaluator());
            var h1 = new Hand(NO_HAND_1);
            var h2 = new Hand(NO_HAND_2);
            var p1 = new Hand(PAIR_HAND_1);
            var p2 = new Hand(PAIR_HAND_2);
            var t1 = new Hand(TWO_PAIR_HAND_1);
            var t2 = new Hand(TWO_PAIR_HAND_2);
            var tk1 = new Hand(TOAK_HAND_1);
            var tk2 = new Hand(TOAK_HAND_2);
            var s1 = new Hand(STRAIGHT_HAND1);
            var s2 = new Hand(STRAIGHT_HAND2);
            var s3 = new Hand(STRAIGHT_HAND3);
            var f1 = new Hand(FLUSH_HAND_1);
            var f2 = new Hand(FLUSH_HAND_2);
            var f3 = new Hand(FLUSH_HAND_3);
            Assert.IsTrue(hc.Compare(h1, f1) < 0);
            Assert.IsTrue(hc.Compare(f1, h1) > 0);
            Assert.IsTrue(hc.Compare(h2, f2) < 0);
            Assert.IsTrue(hc.Compare(f2, h2) > 0);
            Assert.IsTrue(hc.Compare(p1, f1) < 0);
            Assert.IsTrue(hc.Compare(f1, p1) > 0);
            Assert.IsTrue(hc.Compare(p2, f2) < 0);
            Assert.IsTrue(hc.Compare(f2, p2) > 0);
            Assert.IsTrue(hc.Compare(t1, f1) < 0);
            Assert.IsTrue(hc.Compare(f1, t1) > 0);
            Assert.IsTrue(hc.Compare(t2, f2) < 0);
            Assert.IsTrue(hc.Compare(f2, t2) > 0);
            Assert.IsTrue(hc.Compare(s2, f1) < 0);
            Assert.IsTrue(hc.Compare(f1, s2) > 0);
            Assert.IsTrue(hc.Compare(s2, f3) < 0);
            Assert.IsTrue(hc.Compare(f3, s2) > 0);
            Assert.IsTrue(hc.Compare(f1, f2) < 0);
            Assert.IsTrue(hc.Compare(f2, f1) > 0);

            Assert.IsTrue(hc.Compare(f1, tk1) > 0);
            Assert.IsTrue(hc.Compare(tk1, f1) < 0);
            Assert.IsTrue(hc.Compare(f1, tk2) > 0);
            Assert.IsTrue(hc.Compare(tk2, f1) < 0);
            Assert.IsTrue(hc.Compare(f1, s1) > 0);
            Assert.IsTrue(hc.Compare(s1, f1) < 0);
            Assert.IsTrue(hc.Compare(f1, s3) > 0);
            Assert.IsTrue(hc.Compare(s3, f1) < 0);
            Assert.IsTrue(hc.Compare(f2, f3) == 0);
        }

        public const string FULL_HOUSE_HAND_1 = "3C 3H 9H 9C 9S";
        public const string FULL_HOUSE_HAND_2 = "4C 4H TH TC TS";

        [Test()]
        public void HandComparerFullHouseTest()
        {
            HandComparer hc = new HandComparer(new PokerHandEvaluator());
            var h1 = new Hand(NO_HAND_1);
            var h2 = new Hand(NO_HAND_2);
            var p1 = new Hand(PAIR_HAND_1);
            var p2 = new Hand(PAIR_HAND_2);
            var t1 = new Hand(TWO_PAIR_HAND_1);
            var t2 = new Hand(TWO_PAIR_HAND_2);
            var tk1 = new Hand(TOAK_HAND_1);
            var tk2 = new Hand(TOAK_HAND_2);
            var s1 = new Hand(STRAIGHT_HAND1);
            var s2 = new Hand(STRAIGHT_HAND2);
            var s3 = new Hand(STRAIGHT_HAND3);
            var f1 = new Hand(FLUSH_HAND_1);
            var f2 = new Hand(FLUSH_HAND_2);
            var f3 = new Hand(FLUSH_HAND_3);
            var fh1 = new Hand(FULL_HOUSE_HAND_1);
            var fh2 = new Hand(FULL_HOUSE_HAND_2);
            Assert.IsTrue(hc.Compare(h1, fh1) < 0);
            Assert.IsTrue(hc.Compare(fh1, h1) > 0);
            Assert.IsTrue(hc.Compare(h2, fh2) < 0);
            Assert.IsTrue(hc.Compare(fh2, h2) > 0);
            Assert.IsTrue(hc.Compare(p1, fh1) < 0);
            Assert.IsTrue(hc.Compare(fh1, p1) > 0);
            Assert.IsTrue(hc.Compare(p2, fh2) < 0);
            Assert.IsTrue(hc.Compare(fh2, p2) > 0);
            Assert.IsTrue(hc.Compare(t1, fh1) < 0);
            Assert.IsTrue(hc.Compare(fh1, t1) > 0);
            Assert.IsTrue(hc.Compare(t2, fh2) < 0);
            Assert.IsTrue(hc.Compare(fh2, t2) > 0);
            Assert.IsTrue(hc.Compare(s2, fh1) < 0);
            Assert.IsTrue(hc.Compare(fh1, s2) > 0);
            Assert.IsTrue(hc.Compare(f1, fh1) < 0);
            Assert.IsTrue(hc.Compare(fh1, f1) > 0);
            Assert.IsTrue(hc.Compare(f2, fh2) < 0);
            Assert.IsTrue(hc.Compare(fh2, f2) > 0);
            Assert.IsTrue(hc.Compare(fh1, fh2) < 0);
            Assert.IsTrue(hc.Compare(fh2, fh1) > 0);

            Assert.IsTrue(hc.Compare(fh1, tk1) > 0);
            Assert.IsTrue(hc.Compare(tk1, fh1) < 0);
            Assert.IsTrue(hc.Compare(fh1, tk2) > 0);
            Assert.IsTrue(hc.Compare(tk2, fh1) < 0);
            Assert.IsTrue(hc.Compare(fh1, s1) > 0);
            Assert.IsTrue(hc.Compare(s1, fh1) < 0);
            Assert.IsTrue(hc.Compare(fh1, s3) > 0);
            Assert.IsTrue(hc.Compare(s3, fh1) < 0);
            Assert.IsTrue(hc.Compare(fh1, f3) > 0);
            Assert.IsTrue(hc.Compare(f3, fh1) < 0);
        }

        public const string FOK_HAND_1 = "5C 5D 5H 5S 4C";
        public const string FOK_HAND_2 = "6C 6D 6H 6S 7C";
        [Test()]
        public void HandComparerFourOfAKindTest()
        {
            HandComparer hc = new HandComparer(new PokerHandEvaluator());
            var h1 = new Hand(NO_HAND_1);
            var h2 = new Hand(NO_HAND_2);
            var p1 = new Hand(PAIR_HAND_1);
            var p2 = new Hand(PAIR_HAND_2);
            var t1 = new Hand(TWO_PAIR_HAND_1);
            var t2 = new Hand(TWO_PAIR_HAND_2);
            var tk1 = new Hand(TOAK_HAND_1);
            var tk2 = new Hand(TOAK_HAND_2);
            var s1 = new Hand(STRAIGHT_HAND1);
            var s2 = new Hand(STRAIGHT_HAND2);
            var s3 = new Hand(STRAIGHT_HAND3);
            var f1 = new Hand(FLUSH_HAND_1);
            var f2 = new Hand(FLUSH_HAND_2);
            var f3 = new Hand(FLUSH_HAND_3);
            var fh1 = new Hand(FULL_HOUSE_HAND_1);
            var fh2 = new Hand(FULL_HOUSE_HAND_2);
            var fk1 = new Hand(FOK_HAND_1);
            var fk2 = new Hand(FOK_HAND_2);
            Assert.IsTrue(hc.Compare(h1, fk1) < 0);
            Assert.IsTrue(hc.Compare(fk1, h1) > 0);
            Assert.IsTrue(hc.Compare(h2, fk2) < 0);
            Assert.IsTrue(hc.Compare(fk2, h2) > 0);
            Assert.IsTrue(hc.Compare(p1, fk1) < 0);
            Assert.IsTrue(hc.Compare(fk1, p1) > 0);
            Assert.IsTrue(hc.Compare(p2, fk2) < 0);
            Assert.IsTrue(hc.Compare(fk2, p2) > 0);
            Assert.IsTrue(hc.Compare(t1, fk1) < 0);
            Assert.IsTrue(hc.Compare(fk1, t1) > 0);
            Assert.IsTrue(hc.Compare(t2, fk2) < 0);
            Assert.IsTrue(hc.Compare(fk2, t2) > 0);
            Assert.IsTrue(hc.Compare(s2, fk1) < 0);
            Assert.IsTrue(hc.Compare(fk1, s2) > 0);
            Assert.IsTrue(hc.Compare(f1, fk1) < 0);
            Assert.IsTrue(hc.Compare(fk1, f1) > 0);
            Assert.IsTrue(hc.Compare(f2, fk2) < 0);
            Assert.IsTrue(hc.Compare(fk2, f2) > 0);
            Assert.IsTrue(hc.Compare(fh1, fk2) < 0);
            Assert.IsTrue(hc.Compare(fk2, fh1) > 0);
            Assert.IsTrue(hc.Compare(fk1, fk2) < 0);
            Assert.IsTrue(hc.Compare(fk2, fk1) > 0);

            Assert.IsTrue(hc.Compare(fk1, tk1) > 0);
            Assert.IsTrue(hc.Compare(tk1, fk1) < 0);
            Assert.IsTrue(hc.Compare(fk1, tk2) > 0);
            Assert.IsTrue(hc.Compare(tk2, fk1) < 0);
            Assert.IsTrue(hc.Compare(fk1, s1) > 0);
            Assert.IsTrue(hc.Compare(s1, fk1) < 0);
            Assert.IsTrue(hc.Compare(fk1, s3) > 0);
            Assert.IsTrue(hc.Compare(s3, fk1) < 0);
            Assert.IsTrue(hc.Compare(fk1, f3) > 0);
            Assert.IsTrue(hc.Compare(f3, fk1) < 0);
            Assert.IsTrue(hc.Compare(fk1, fh2) > 0);
            Assert.IsTrue(hc.Compare(fh2, fk1) < 0);
        }

        public const string STRT_FLUSH_HAND_1 = "AC 2C 3C 4C 5C";
        public const string STRT_FLUSH_HAND_2 = "4S 5S 6S 7S 8S";
        public const string STRT_FLUSH_HAND_3 = "AD 2D 3D 4D 5D";

        [Test()]
        public void HandComparerStraightFlushTest()
        {
            HandComparer hc = new HandComparer(new PokerHandEvaluator());
            var h1 = new Hand(NO_HAND_1);
            var h2 = new Hand(NO_HAND_2);
            var p1 = new Hand(PAIR_HAND_1);
            var p2 = new Hand(PAIR_HAND_2);
            var t1 = new Hand(TWO_PAIR_HAND_1);
            var t2 = new Hand(TWO_PAIR_HAND_2);
            var tk1 = new Hand(TOAK_HAND_1);
            var tk2 = new Hand(TOAK_HAND_2);
            var s1 = new Hand(STRAIGHT_HAND1);
            var s2 = new Hand(STRAIGHT_HAND2);
            var s3 = new Hand(STRAIGHT_HAND3);
            var f1 = new Hand(FLUSH_HAND_1);
            var f2 = new Hand(FLUSH_HAND_2);
            var f3 = new Hand(FLUSH_HAND_3);
            var fh1 = new Hand(FULL_HOUSE_HAND_1);
            var fh2 = new Hand(FULL_HOUSE_HAND_2);
            var fk1 = new Hand(FOK_HAND_1);
            var fk2 = new Hand(FOK_HAND_2);
            var sf1 = new Hand(STRT_FLUSH_HAND_1);
            var sf2 = new Hand(STRT_FLUSH_HAND_2);
            var sf3 = new Hand(STRT_FLUSH_HAND_3);

            Assert.IsTrue(hc.Compare(sf1, tk1) > 0);
            Assert.IsTrue(hc.Compare(tk1, sf1) < 0);
            Assert.IsTrue(hc.Compare(sf1, tk2) > 0);
            Assert.IsTrue(hc.Compare(tk2, sf1) < 0);
            Assert.IsTrue(hc.Compare(sf1, s1) > 0);
            Assert.IsTrue(hc.Compare(s1, sf1) < 0);
            Assert.IsTrue(hc.Compare(sf1, s3) > 0);
            Assert.IsTrue(hc.Compare(s3, sf1) < 0);
            Assert.IsTrue(hc.Compare(sf1, f3) > 0);
            Assert.IsTrue(hc.Compare(f3, sf1) < 0);
            Assert.IsTrue(hc.Compare(sf1, fh2) > 0);
            Assert.IsTrue(hc.Compare(fh2, sf1) < 0);
            Assert.IsTrue(hc.Compare(sf1, fk2) > 0);
            Assert.IsTrue(hc.Compare(fk2, sf1) < 0);

            Assert.IsTrue(hc.Compare(h1, sf1) < 0);
            Assert.IsTrue(hc.Compare(sf1, h1) > 0);
            Assert.IsTrue(hc.Compare(h2, sf2) < 0);
            Assert.IsTrue(hc.Compare(sf2, h2) > 0);
            Assert.IsTrue(hc.Compare(p1, sf1) < 0);
            Assert.IsTrue(hc.Compare(sf1, p1) > 0);
            Assert.IsTrue(hc.Compare(p2, sf2) < 0);
            Assert.IsTrue(hc.Compare(sf2, p2) > 0);
            Assert.IsTrue(hc.Compare(t1, sf1) < 0);
            Assert.IsTrue(hc.Compare(sf1, t1) > 0);
            Assert.IsTrue(hc.Compare(t2, sf2) < 0);
            Assert.IsTrue(hc.Compare(sf2, t2) > 0);
            Assert.IsTrue(hc.Compare(s2, sf1) < 0);
            Assert.IsTrue(hc.Compare(sf1, s2) > 0);
            Assert.IsTrue(hc.Compare(f1, sf1) < 0);
            Assert.IsTrue(hc.Compare(sf1, f1) > 0);
            Assert.IsTrue(hc.Compare(f2, sf2) < 0);
            Assert.IsTrue(hc.Compare(sf2, f2) > 0);
            Assert.IsTrue(hc.Compare(fh1, sf2) < 0);
            Assert.IsTrue(hc.Compare(sf2, fh1) > 0);
            Assert.IsTrue(hc.Compare(fk1, sf2) < 0);
            Assert.IsTrue(hc.Compare(sf2, fk1) > 0);
            Assert.IsTrue(hc.Compare(sf1, sf2) < 0);
            Assert.IsTrue(hc.Compare(sf2, sf1) > 0);
            Assert.IsTrue(hc.Compare(sf1, sf3) == 0);
        }

        public const string ROYAL_HAND_1 = "TS JS QS KS AS";
        public const string ROYAL_HAND_2 = "AC KC QC JC TC";

        [Test()]
        public void HandComparerRoyalFlushTest()
        {
            HandComparer hc = new HandComparer(new PokerHandEvaluator());
            var h1 = new Hand(NO_HAND_1);
            var h2 = new Hand(NO_HAND_2);
            var p1 = new Hand(PAIR_HAND_1);
            var p2 = new Hand(PAIR_HAND_2);
            var t1 = new Hand(TWO_PAIR_HAND_1);
            var t2 = new Hand(TWO_PAIR_HAND_2);
            var tk1 = new Hand(TOAK_HAND_1);
            var tk2 = new Hand(TOAK_HAND_2);
            var s1 = new Hand(STRAIGHT_HAND1);
            var s2 = new Hand(STRAIGHT_HAND2);
            var s3 = new Hand(STRAIGHT_HAND3);
            var f1 = new Hand(FLUSH_HAND_1);
            var f2 = new Hand(FLUSH_HAND_2);
            var f3 = new Hand(FLUSH_HAND_3);
            var fh1 = new Hand(FULL_HOUSE_HAND_1);
            var fh2 = new Hand(FULL_HOUSE_HAND_2);
            var fk1 = new Hand(FOK_HAND_1);
            var fk2 = new Hand(FOK_HAND_2);
            var sf1 = new Hand(STRT_FLUSH_HAND_1);
            var sf2 = new Hand(STRT_FLUSH_HAND_2);
            var sf3 = new Hand(STRT_FLUSH_HAND_3);
            var r1 = new Hand(ROYAL_HAND_1);
            var r2 = new Hand(ROYAL_HAND_2);

            Assert.IsTrue(hc.Compare(r1, tk1) > 0);
            Assert.IsTrue(hc.Compare(tk1, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, tk2) > 0);
            Assert.IsTrue(hc.Compare(tk2, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, s1) > 0);
            Assert.IsTrue(hc.Compare(s1, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, s3) > 0);
            Assert.IsTrue(hc.Compare(s3, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, f3) > 0);
            Assert.IsTrue(hc.Compare(f3, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, fh2) > 0);
            Assert.IsTrue(hc.Compare(fh2, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, fk2) > 0);
            Assert.IsTrue(hc.Compare(fk2, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, sf2) > 0);
            Assert.IsTrue(hc.Compare(sf2, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, sf3) > 0);
            Assert.IsTrue(hc.Compare(sf3, r1) < 0);

            Assert.IsTrue(hc.Compare(h1, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, h1) > 0);
            Assert.IsTrue(hc.Compare(h2, r2) < 0);
            Assert.IsTrue(hc.Compare(r2, h2) > 0);
            Assert.IsTrue(hc.Compare(p1, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, p1) > 0);
            Assert.IsTrue(hc.Compare(p2, r2) < 0);
            Assert.IsTrue(hc.Compare(r2, p2) > 0);
            Assert.IsTrue(hc.Compare(t1, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, t1) > 0);
            Assert.IsTrue(hc.Compare(t2, r2) < 0);
            Assert.IsTrue(hc.Compare(r2, t2) > 0);
            Assert.IsTrue(hc.Compare(s2, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, s2) > 0);
            Assert.IsTrue(hc.Compare(f1, r1) < 0);
            Assert.IsTrue(hc.Compare(r1, f1) > 0);
            Assert.IsTrue(hc.Compare(f2, r2) < 0);
            Assert.IsTrue(hc.Compare(r2, f2) > 0);
            Assert.IsTrue(hc.Compare(fh1, r2) < 0);
            Assert.IsTrue(hc.Compare(r2, fh1) > 0);
            Assert.IsTrue(hc.Compare(fk1, r2) < 0);
            Assert.IsTrue(hc.Compare(r2, fk1) > 0);
            Assert.IsTrue(hc.Compare(sf1, r2) < 0);
            Assert.IsTrue(hc.Compare(r2, sf1) > 0);
            Assert.IsTrue(hc.Compare(r1, r2) == 0);
        }
    }
}
