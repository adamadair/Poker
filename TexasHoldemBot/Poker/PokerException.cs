using System;

namespace TexasHoldemBot.Poker
{
    public class PokerException : Exception
    {
        public PokerException (string msg):base(msg)
        {
        }
    }
}

