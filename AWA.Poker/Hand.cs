using System;
using System.Collections.Generic;
using System.Text;

namespace AWA.Poker
{
    /// <summary>
    /// A poker hand. A valid poker hand must have at least 5 cards.
    /// More cards are allowed to be in the hand to allow implementations
    /// of game sof poker than involve cards.
    /// </summary>
    public class Hand : IComparable<Hand>
    {
        private List<Card> cards;

        private static HandComparer _comparer;
        private static HandComparer Comparer
        {
            get
            {
                if (_comparer == null)
                    _comparer = new HandComparer(new PokerHandEvaluator());
                return _comparer;
            }
        }

        public Hand()
        {
            cards = new List<Card>();
        }

        public Hand(string handCards)
        {
            cards = new List<Card>();
            foreach (var c in handCards.Split(" ".ToCharArray()))
            {
                Add(new Card(c));
            }
        }

        public Card[] Cards { get { return cards.ToArray(); } }

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
            StringBuilder sb = new StringBuilder();
            foreach (var c in cards)
            {
                sb.Append(" ");
                sb.Append(c.ToString());
            }
            return "[" + sb.ToString().TrimStart() + "]";
        }

        public int CompareTo(Hand other)
        {
            return Comparer.Compare(this, other);
        }
    }
}

