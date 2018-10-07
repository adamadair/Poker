using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        #region Static Card Members
        /*
         * All the cards that ever need to be made get initialized as static
         * so we never have to make new ones, reducing garbage collection.
         */
        public static Card S2 = new Card("2S");
        public static Card S3 = new Card("3S");
        public static Card S4 = new Card("4S");
        public static Card S5 = new Card("5S");
        public static Card S6 = new Card("6S");
        public static Card S7 = new Card("7S");
        public static Card S8 = new Card("8S");
        public static Card S9 = new Card("9S");
        public static Card ST = new Card("TS");
        public static Card SJ = new Card("JS");
        public static Card SQ = new Card("QS");
        public static Card SK = new Card("KS");
        public static Card SA = new Card("AS");
        public static Card H2 = new Card("2H");
        public static Card H3 = new Card("3H");
        public static Card H4 = new Card("4H");
        public static Card H5 = new Card("5H");
        public static Card H6 = new Card("6H");
        public static Card H7 = new Card("7H");
        public static Card H8 = new Card("8H");
        public static Card H9 = new Card("9H");
        public static Card HT = new Card("TH");
        public static Card HJ = new Card("JH");
        public static Card HQ = new Card("QH");
        public static Card HK = new Card("KH");
        public static Card HA = new Card("AH");
        public static Card C2 = new Card("2C");
        public static Card C3 = new Card("3C");
        public static Card C4 = new Card("4C");
        public static Card C5 = new Card("5C");
        public static Card C6 = new Card("6C");
        public static Card C7 = new Card("7C");
        public static Card C8 = new Card("8C");
        public static Card C9 = new Card("9C");
        public static Card CT = new Card("TC");
        public static Card CJ = new Card("JC");
        public static Card CQ = new Card("QC");
        public static Card CK = new Card("KC");
        public static Card CA = new Card("AC");
        public static Card D2 = new Card("2D");
        public static Card D3 = new Card("3D");
        public static Card D4 = new Card("4D");
        public static Card D5 = new Card("5D");
        public static Card D6 = new Card("6D");
        public static Card D7 = new Card("7D");
        public static Card D8 = new Card("8D");
        public static Card D9 = new Card("9D");
        public static Card DT = new Card("TD");
        public static Card DJ = new Card("JD");
        public static Card DQ = new Card("QD");
        public static Card DK = new Card("KD");
        public static Card DA = new Card("AD");
        public static Dictionary<string, Card> Cards { get; }
        static Card()
        {
            Cards = new Dictionary<string, Card>
            {
                {"2S", S2},
                {"3S", S3},
                {"4S", S4},
                {"5S", S5},
                {"6S", S6},
                {"7S", S7},
                {"8S", S8},
                {"9S", S9},
                {"TS", ST},
                {"JS", SJ},
                {"QS", SQ},
                {"KS", SK},
                {"AS", SA},
                {"2H", H2},
                {"3H", H3},
                {"4H", H4},
                {"5H", H5},
                {"6H", H6},
                {"7H", H7},
                {"8H", H8},
                {"9H", H9},
                {"TH", HT},
                {"JH", HJ},
                {"QH", HQ},
                {"KH", HK},
                {"AH", HA},
                {"2C", C2},
                {"3C", C3},
                {"4C", C4},
                {"5C", C5},
                {"6C", C6},
                {"7C", C7},
                {"8C", C8},
                {"9C", C9},
                {"TC", CT},
                {"JC", CJ},
                {"QC", CQ},
                {"KC", CK},
                {"AC", CA},
                {"2D", D2},
                {"3D", D3},
                {"4D", D4},
                {"5D", D5},
                {"6D", D6},
                {"7D", D7},
                {"8D", D8},
                {"9D", D9},
                {"TD", DT},
                {"JD", DJ},
                {"QD", DQ},
                {"KD", DK},
                {"AD", DA}
            };         
        }

        public static Card Parse(string card)
        {
            return Cards[card.ToUpper()];
        }
        #endregion

        private readonly Tuple<CardSuit, CardValue> _v;
        public Card (CardSuit cs, CardValue cr)
        {
            _v = new Tuple<CardSuit, CardValue>(cs, cr);            
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
        private Card (string cardString)
        {
            _v = SetCardValues(cardString);
        }

        public CardSuit Suit => _v.Item1;
        public CardValue Value => _v.Item2;

        /// <summary>
        /// Sets the card values from an input string. 
        /// </summary>
        /// <param name="val">Value.</param>
        private Tuple<CardSuit, CardValue> SetCardValues(string val)
        {
            if(val.Length!=2)
                throw new PokerException("Incorrect length for card string. Must be 2 characters.");
            var cr = val.Substring(0,1).ToUpper();
            var cs = val.Substring(1,1).ToUpper();
            CardSuit suit;
            CardValue value;
            switch (cr)
            {
                case "2": 
                    value = CardValue.Two;
                    break;
                case "3":
                    value = CardValue.Three;
                    break;
                case "4":
                    value = CardValue.Four;
                    break;
                case "5":
                    value = CardValue.Five;
                    break;
                case "6":
                    value = CardValue.Six;
                    break;
                case "7":
                    value = CardValue.Seven;
                    break;
                case "8":
                    value = CardValue.Eight;
                    break;
                case "9":
                    value = CardValue.Nine;
                    break;
                case "T":
                    value = CardValue.Ten;
                    break;
                case "J":
                    value = CardValue.Jack;
                    break;
                case "Q":
                    value = CardValue.Queen;
                    break;
                case "K":
                    value = CardValue.King;
                    break;
                case "A":
                    value = CardValue.Ace;
                    break;
                default:
                    throw new PokerException(cr + " is an invalid card rank.");
            }
            switch (cs)
            {
                case "C":
                    suit = CardSuit.Clubs;
                    break;
                case "D":
                    suit = CardSuit.Diamonds;
                    break;
                case "H":
                    suit = CardSuit.Hearts;
                    break;
                case "S":
                    suit = CardSuit.Spades;
                    break;
                default:
                    throw new PokerException(cs + " is an invalid card suite.");
            }

            return new Tuple<CardSuit, CardValue>(suit, value);
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

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Card) && _v.Equals(((Card) obj)._v);            
        }

        public override int GetHashCode()
        {
            return _v.GetHashCode();
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

