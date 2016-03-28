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

        }


    }
}
