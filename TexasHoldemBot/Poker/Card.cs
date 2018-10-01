using System;
using System.Text;

namespace TexasHoldemBot.Poker
{
    /// <summary>
    /// Card represents a single playing card from a Deck of playing cards
    /// that are used to play Poker. A card is identified by its Suit and 
    /// Rank.
    /// </summary>
    public class Card
    {
        public Card (CardSuit cs, CardValue cr)
        {
            Suit = cs;
            Value = cr;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class. Takes
        /// an input string in the format [rank][suite] where: <br/>
        /// rank is a character in the set [2,3,4,5,6,7,8,9,T,J,Q,K,A], <br/> 
        /// and suit is a character in the set [C,D,H,S] <br/>
        /// Example: <br/>
        /// 2S => 2 of Spades <br/>
        /// JC => Jack of Clubs <br/>
        /// A joker is a special case which has no rank or suit and can be initialized with
        /// the string "JJ"
        /// </summary>
        /// <param name="cardString">Card string.</param>
        public Card (string cardString)
        {
            SetCardValues (cardString);
        }

        public CardSuit Suit { get; private set; }
        public CardValue Value { get; private set; }

        /// <summary>
        /// Sets the card values from an input string. 
        /// </summary>
        /// <param name="val">Value.</param>
        private void SetCardValues(string val)
        {
            if(val.Length!=2)
                throw new PokerException("Incorrect length for card string. Must be 2 characters.");
            var cr = val.Substring(0,1).ToUpper();
            var cs = val.Substring(1,1).ToUpper();
            switch (cr)
            {
                case "2": 
                    Value = CardValue.Two;
                    break;
                case "3":
                    Value = CardValue.Three;
                    break;
                case "4":
                    Value = CardValue.Four;
                    break;
                case "5":
                    Value = CardValue.Five;
                    break;
                case "6":
                    Value = CardValue.Six;
                    break;
                case "7":
                    Value = CardValue.Seven;
                    break;
                case "8":
                    Value = CardValue.Eight;
                    break;
                case "9":
                    Value = CardValue.Nine;
                    break;
                case "T":
                    Value = CardValue.Ten;
                    break;
                case "J":
                    Value = CardValue.Jack;
                    break;
                case "Q":
                    Value = CardValue.Queen;
                    break;
                case "K":
                    Value = CardValue.King;
                    break;
                case "A": 
                    Value = CardValue.Ace;
                    break;
                default:
                    throw new PokerException(cr + " is an invalid card rank.");
            }
            switch (cs)
            {
                case "C":
                    Suit = CardSuit.Clubs;
                    break;
                case "D":
                    Suit = CardSuit.Diamonds;
                    break;
                case "H":
                    Suit = CardSuit.Hearts;
                    break;
                case "S":
                    Suit = CardSuit.Spades;
                    break;
                default:
                    throw new PokerException(cs + " is an invalid card suite.");
            }
        }

        /// <summary>
        /// Returns true if connected to card c
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool IsConnected(Card c)
        {
            CardValue v = c.Value;
            switch (Value)
            {
                case CardValue.Two:
                    return (v == CardValue.Ace || v == CardValue.Three);
                case CardValue.Ace:
                    return (v == CardValue.King || v == CardValue.Two);
                default:
                    return Math.Abs((int) Value - (int) v) == 1;
            }
        }

        /// <summary>
        /// Returns long human readable formatted string representation of card, such as
        /// "2 of Spades" or "Queen of Hearts".
        /// </summary>
        /// <returns>The long string.</returns>
        public string ToLongString()
        {
            return string.Format ("{1} of {0}", Suit.ToString(), Value.ToString());  
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="Card"/>.
        /// The string returned is in the format [rank][suite] where: <br/>
        /// rank is a character in the set [2,3,4,5,6,7,8,9,T,J,Q,K,A], <br/> 
        /// and suit is a character in the set [C,D,H,S] <br/>
        /// Example: <br/>
        /// 2S => 2 of Spades <br/>
        /// JC => Jack of Clubs <br/>
        /// A joker is a special case string "JJ"
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="Card"/>.</returns>
        public override string ToString ()
        {            
            StringBuilder sb = new StringBuilder ();
            switch (Value)
            {
                case CardValue.Two:
                    sb.Append("2");
                    break;
                case CardValue.Three:
                    sb.Append("3");
                    break;
                case CardValue.Four:
                    sb.Append("4");
                    break;
                case CardValue.Five:
                    sb.Append("5");
                    break;
                case CardValue.Six:
                    sb.Append("6");
                    break;
                case CardValue.Seven:
                    sb.Append("7");
                    break;
                case CardValue.Eight:
                    sb.Append("8");
                    break;
                case CardValue.Nine:
                    sb.Append("9");
                    break;
                case CardValue.Ten:
                    sb.Append("T");
                    break;
                case CardValue.Jack:
                    sb.Append("J");
                    break;
                case CardValue.Queen:
                    sb.Append("Q");
                    break;
                case CardValue.King:
                    sb.Append("K");
                    break;
                case CardValue.Ace:
                    sb.Append("A");
                    break;
                default:
                    throw new PokerException("Card to String error, unknown rank");
            }
            switch (Suit)
            {
                case CardSuit.Clubs:
                    sb.Append("C");
                    break;
                case CardSuit.Diamonds:
                    sb.Append("D");
                    break;
                case CardSuit.Hearts:
                    sb.Append("H");
                    break;
                case CardSuit.Spades:
                    sb.Append("S");
                    break;
                default:
                    throw new PokerException("Card to string error, unknown suit");
            }
            return sb.ToString ();
        }
    }

    /// <summary>
    /// Card suit.
    /// </summary>
    public enum CardSuit { Clubs, Diamonds, Hearts, Spades }

    /// <summary>
    /// Card rank.
    /// </summary>
    public enum CardValue { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
}

