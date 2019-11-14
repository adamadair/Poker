// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodinGameTests
{


    [TestFixture]
    public class OutputTests
    {
        IPokerHandEvaluator evaluator = new BrecherHandEvaluator();
        [Test]
        public void TestMakingSomeOutput()
        {
            var tests = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("KS 9D 5C 3S 2D 8D 7C", "K9875"), // high card
                new Tuple<string, string>("AD 7C 9D 8C KS 3S 2D", "AK987"), // high card
                new Tuple<string, string>("AD 7C 9D 8C KS 3S 7D", "77AK9"), // pair
                new Tuple<string, string>("2C AC AH 2H 9C 6D 6H", "AA669"), // 2 pair
                new Tuple<string, string>("2D 3D 9D 6S KD 4D 2S", "K9432"), // flush
                new Tuple<string, string>("5C 5S AH 5D 9C 6D QH", "555AQ"), // 3 of a kind
                new Tuple<string, string>("5C 5S AH 5D 9C 5H 2D", "5555A"), // 4 of a kind
                new Tuple<string, string>("6D 5D 9C 6C AH 6H 5C", "66655"), // Full House
                new Tuple<string, string>("9D 6S 3S 4D 5D 7D KC", "76543"), // Straight
                new Tuple<string, string>("2D 3D 9D 6S AD 4D 5D", "5432A"), // Straight Flush

            };

            foreach (var tuple in tests)
            {
                Hand h = new Hand(tuple.Item1);
                Assert.AreEqual(tuple.Item2,Hand.ToCodinGameString(h.GetHandCards(evaluator)));
            }
        }

        [Test]
        public void CountSuitTest()
        {
            Hand h = new Hand("2D 3D 9D 6S KD 4D 2S");
            Assert.AreEqual(5,h.SuitCount(CardSuit.Diamonds));
        }




        [Test]
        public void NewHandComparerTest()
        {
            var tests = new List<Tuple<string, string, int>>
            {
                new Tuple<string, string, int>("5H 5S 2C AC AH 2H 9C", "6D 6H 2C AC AH 2H 9C", -1),
                new Tuple<string, string, int>("5H 2D 5C 5S AS QD 9C", "8D 5D 5C 5S AS QD 9C", 0)
            };
            var hc = new NewHandComparer();

            foreach (var tuple in tests)
            {
                var h1 = new Hand(tuple.Item1);
                var h2 = new Hand(tuple.Item2);
                Assert.AreEqual(tuple.Item3, hc.Compare(h1, h2));
            }
        }
    }
}
