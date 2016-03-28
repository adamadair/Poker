using System;
using System.Text;

namespace AWA.Poker
{
    /// <summary>
    /// Card represents a single playing card from a Deck of playing cards
    /// that are used to play Poker. A card is identified by its Suit and 
    /// Rank.
    /// </summary>
    public class Card
    {
        private CardSuit suit;
        private CardRank rank;

        public Card (CardSuit cs, CardRank cr)
        {
            if (cs == CardSuit.None && cr != CardRank.Joker)
            {
                throw new PokerException("Card intialization error! A rank of " + cr.ToString() + " requires a proper suit.");
            }
            suit = cs;
            rank = cr;
            if (rank == CardRank.Joker)
            {
                this.suit = CardSuit.None;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AWA.Poker.Card"/> class. Takes
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

        public CardSuit Suit{get{ return this.suit; }}
        public CardRank Rank{get{ return this.rank; }}

        /// <summary>
        /// Sets the card values from an input string. 
        /// </summary>
        /// <param name="val">Value.</param>
        private void SetCardValues(string val)
        {
            if(val.Length!=2)
                throw new PokerException("Incorrect length for card string. Must be 2 characters.");
            if(val=="JJ"){
                suit=CardSuit.None;
                rank= CardRank.Joker;
            }
            var cr = val.Substring(0,1).ToUpper();
            var cs = val.Substring(1,1).ToUpper();
            switch (cr)
            {
                case "2": 
                    this.rank = CardRank.Two;
                    break;
                case "3":
                    this.rank = CardRank.Three;
                    break;
                case "4":
                    this.rank = CardRank.Four;
                    break;
                case "5":
                    this.rank = CardRank.Five;
                    break;
                case "6":
                    this.rank = CardRank.Six;
                    break;
                case "7":
                    this.rank = CardRank.Seven;
                    break;
                case "8":
                    this.rank = CardRank.Eight;
                    break;
                case "9":
                    this.rank = CardRank.Nine;
                    break;
                case "T":
                    this.rank = CardRank.Ten;
                    break;
                case "J":
                    this.rank = CardRank.Jack;
                    break;
                case "Q":
                    this.rank = CardRank.Queen;
                    break;
                case "K":
                    this.rank = CardRank.King;
                    break;
                case "A": 
                    this.rank = CardRank.Ace;
                    break;
                default:
                    throw new PokerException(cr + " is an invalid card rank.");
            }
            switch (cs)
            {
                case "C":
                    this.suit = CardSuit.Clubs;
                    break;
                case "D":
                    this.suit = CardSuit.Diamonds;
                    break;
                case "H":
                    this.suit = CardSuit.Hearts;
                    break;
                case "S":
                    this.suit = CardSuit.Spades;
                    break;
                default:
                    throw new PokerException(cs + " is an invalid card suite.");
            }
        }

        /// <summary>
        /// Returns long human readable formatted string representation of card, such as
        /// "2 of Spades" or "Queen of Hearts".
        /// </summary>
        /// <returns>The long string.</returns>
        public string ToLongString()
        {
            if (Rank == CardRank.Joker)
                return"Joker";
            return string.Format ("{1} of {0}", Suit.ToString(), Rank.ToString());  
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="AWA.Poker.Card"/>.
        /// The string returned is in the format [rank][suite] where: <br/>
        /// rank is a character in the set [2,3,4,5,6,7,8,9,T,J,Q,K,A], <br/> 
        /// and suit is a character in the set [C,D,H,S] <br/>
        /// Example: <br/>
        /// 2S => 2 of Spades <br/>
        /// JC => Jack of Clubs <br/>
        /// A joker is a special case string "JJ"
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="AWA.Poker.Card"/>.</returns>
        public override string ToString ()
        {
            if (Rank == CardRank.Joker)
                return "JJ";
            StringBuilder sb = new StringBuilder ();
            switch (this.Rank)
            {
                case CardRank.Two:
                    sb.Append("2");
                    break;
                case CardRank.Three:
                    sb.Append("3");
                    break;
                case CardRank.Four:
                    sb.Append("4");
                    break;
                case CardRank.Five:
                    sb.Append("5");
                    break;
                case CardRank.Six:
                    sb.Append("6");
                    break;
                case CardRank.Seven:
                    sb.Append("7");
                    break;
                case CardRank.Eight:
                    sb.Append("8");
                    break;
                case CardRank.Nine:
                    sb.Append("9");
                    break;
                case CardRank.Ten:
                    sb.Append("T");
                    break;
                case CardRank.Jack:
                    sb.Append("J");
                    break;
                case CardRank.Queen:
                    sb.Append("Q");
                    break;
                case CardRank.King:
                    sb.Append("K");
                    break;
                case CardRank.Ace:
                    sb.Append("A");
                    break;
                default:
                    throw new PokerException("Card to String error, unknown rank");
            }
            switch (this.Suit)
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
    public enum CardSuit { Clubs, Diamonds, Hearts, Spades, None }

    /// <summary>
    /// Card rank.
    /// </summary>
    public enum CardRank { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace, Joker }
}

