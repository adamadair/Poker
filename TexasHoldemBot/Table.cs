using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<Card> TableCards { get; set; }

        public void ParseTableCards(string input)
        {
            this.TableCards = new List<Card>();

            string[] split = input.Split(',');

            foreach (string cardString in split)
            {
                this.TableCards.Add(new Card(cardString));
            }
        }

        public void ClearTableCards()
        {
            this.TableCards = new List<Card>();
        }

        public void SetSmallBlind(int value)
        {
            this.SmallBlind = value;
        }

        public void SetBigBlind(int value)
        {
            this.BigBlind = value;
        }

        public List<Card> GetTableCards()
        {
            return this.TableCards;
        }

        public int GetSmallBlind()
        {
            return this.SmallBlind;
        }

        public int GetBigBlind()
        {
            return this.BigBlind;
        }
    }
}
