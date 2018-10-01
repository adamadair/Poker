using System.Collections.Generic;
using TexasHoldemBot.Poker;

namespace TexasHoldemBot
{
    public class Table
    {
        public Table()
        {
            ClearTableCards();
        }

        public int SmallBlind { get; set; }

        public int BigBlind { get; set; }

        public int MinimumBet => 2 * BigBlind;

        public List<Card> TableCards { get; set; }

        public void ParseTableCards(string input)
        {
            TableCards = new List<Card>();

            string[] split = input.Split(',');

            foreach (var cardString in split)
            {
                TableCards.Add(new Card(cardString));
            }
        }

        public void ClearTableCards()
        {
            TableCards = new List<Card>();
        }

        public void SetSmallBlind(int value)
        {
            SmallBlind = value;
        }

        public void SetBigBlind(int value)
        {
            BigBlind = value;
        }

    }
}
