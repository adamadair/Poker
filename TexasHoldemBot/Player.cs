using System.Collections.Generic;
using TexasHoldemBot.Poker;

namespace TexasHoldemBot
{
    /// <summary>
    /// Stores all the information about a player.
    /// </summary>
    public class Player
    {
        public Player(string playerName)
        {
            Cards = new List<Card>();
            Name = playerName;
        }

        public void ParseHand(string input)
        {
            ClearHand();
            string[] split = input.Split(",".ToCharArray());

            foreach (var cardString in split)
            {
                Cards.Add(Card.Parse(cardString));
            }
        }

        public string Name { get; }

        public int Chips { get; set; }

        public int Bet { get; set; }

        public Move Move { get; set; }

        public void ClearHand()
        {
            Cards.Clear();
        }

        public int Wins { get; set; }
        public int Losses { get; set; }

        public List<Card> Cards { get; }
    }
}
