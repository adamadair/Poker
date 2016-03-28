using System;
using System.Collections.Generic;
using System.Text;

namespace AWA.Poker
{
    /// <summary>
    /// Hand.
    /// </summary>
    public class Hand
    {
        private List<Card> cards;
       
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
    }
}

