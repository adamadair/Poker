using System;
using System.Collections.Generic;
using System.Text;

namespace TexasHoldemBot.Poker
{
    /// <summary>
    /// A poker hand. A valid poker hand must have at least 5 cards.
    /// More cards are allowed to be in the hand to allow implementations
    /// of game of poker that involve more cards.
    /// </summary>
    public class Hand : IComparable<Hand>
    {
        private readonly List<Card> _cards;

        private static HandComparer _comparer;
        public static HandComparer Comparer {
            get
            {
                if (_comparer == null)
                {
                    _comparer = new HandComparer(new BrecherHandEvaluator());
                }
                return _comparer;
            }
            set => _comparer = value;
        }

        public Hand()
        {
            _cards = new List<Card>();
        }

        public Hand(IEnumerable<Card> cards)
        {
            _cards = new List<Card>(cards);
        }

        public Hand(string handCards)
        {
            _cards = new List<Card>();
            foreach (var c in handCards.Split(" ".ToCharArray()))
            {
                Add(Card.Parse(c));
            }
        }

        public Card[] Cards => _cards.ToArray();

        public void Discard(Card card)
        {
            _cards.Remove(card);
        }

        public void Add(Card card)
        {
            _cards.Add(card);
        }

        public void Add(IEnumerable<Card> cards)
        {
            _cards.AddRange(cards);
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (Card c in _cards)
            {
                sb.Append(" ");
                sb.Append(c);
            }
            return "[" + sb.ToString().TrimStart() + "]";
        }

        public int CompareTo(Hand other)
        {
            return Comparer.Compare(this, other);
        }
    }
}

