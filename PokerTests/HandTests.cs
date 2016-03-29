using NUnit.Framework;
using System;
using AWA.Poker;


namespace PokerTests
{
    [TestFixture()]
    public class HandTests
    {
        private const string TEST_HAND = "2C 3D JH JS KS";  
              
        [Test()]
        public void HandConstructorTest()
        {
            var hand = new Hand();
            Assert.IsNotNull(hand);
            var hand2 = new Hand(TEST_HAND);
            Assert.IsNotNull(hand2);
            Assert.AreEqual(hand2.Cards.Length, 5);
        }

        [Test()]
        public void HandToStringTest()
        {
            var hand = new Hand(TEST_HAND);
            Assert.AreEqual(hand.ToString(), "[" + TEST_HAND + "]");
        }

        [Test()]
        public void HandAddTest()
        {
            var h = new Hand();
            Assert.IsTrue(h.Cards.Length == 0);
            var c1 = new Card("JS");
            h.Add(c1);
            Assert.IsTrue(h.Cards.Length == 1);
            Assert.AreSame(h.Cards[0], c1);
        }

        [Test()]
        public void HandDiscardTest()
        {
            var h = new Hand(TEST_HAND);
            Assert.AreEqual(h.Cards.Length, 5);
            var c = h.Cards[0];
            h.Discard(c);
            Assert.AreEqual(h.Cards.Length, 4);
        }

        private const string TEST_HAND2 = "4C 4D 6H 6S QS";
        private const string TEST_HAND3 = "6D 7C 8H 9S TS";

        [Test()]
        public void HandComparerTest()
        {
            var h1 = new Hand(TEST_HAND);
            var h2 = new Hand(TEST_HAND2);
            var h3 = new Hand(TEST_HAND3);

            Assert.IsTrue(h1.CompareTo(h2) < 0);
            Assert.IsTrue(h2.CompareTo(h1) > 0);
            Assert.IsTrue(h2.CompareTo(h3) < 0);
            Assert.IsTrue(h3.CompareTo(h2) > 0);
            Assert.IsTrue(h1.CompareTo(h3) < 0);
            Assert.IsTrue(h3.CompareTo(h1) > 0);            
            Assert.IsTrue(h1.CompareTo(h1) == 0);
            Assert.IsTrue(h2.CompareTo(h2) == 0);
            Assert.IsTrue(h3.CompareTo(h3) == 0);
        }
    }
}
