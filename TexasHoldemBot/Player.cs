using System.Collections.Generic;
using TexasHoldemBot.Poker;

namespace TexasHoldemBot
{
    /// <summary>
    /// Stores all the information about a player.
    /// </summary>
    public class Player
    {        
        private List<Card> _hand;

        public Player(string playerName)
        {
            Name = playerName;
        }

        public void ParseHand(string input)
        {
            _hand = new List<Card>();

            string[] split = input.Split(",".ToCharArray());

            foreach (var cardString in split)
            {
                _hand.Add(new Card(cardString));
            }
        }

        public string Name { get; }

        public int Chips { get; set; }

        public int Bet { get; set; }

        public Move Move { get; set; }

        public List<Card> GetHand()
        {
            return _hand;
        }
    }
}
