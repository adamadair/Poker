/**
          _____
         |A .  | _____
         | /.\ ||A ^  | _____
         |(_._)|| / \ ||A _  | _____
         |  |  || \ / || ( ) ||A_ _ |
         |____V||  .  ||(_'_)||( v )|
                |____V||  |  || \ / |
                       |____V||  .  |
                              |____V|

    KodeMonkey knows Poker
     
 **/

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;


internal class Solution
{
    private static void Main(string[] args)
    {
        string holeCardsPlayer1 = Console.ReadLine();

        string holeCardsPlayer2 = Console.ReadLine();
        string communityCards = Console.ReadLine();

        var table = new Hand(communityCards);
        var h1 = new Hand(holeCardsPlayer1);
        var h2 = new Hand(holeCardsPlayer2);
        h1.Add(table.Cards);
        h2.Add(table.Cards);
        var evaluator = new BrecherHandEvaluator();
        var comparer = new NewHandComparer();
        int result = comparer.Compare(h1, h2);
        if (result < 0)
        {
            WriteOutput(evaluator.Evaluate(h2), 2, h2.GetHandCards(evaluator));
        }
        else if (result > 0)
        {
            WriteOutput(evaluator.Evaluate(h1), 1, h1.GetHandCards(evaluator));
        }
        else
        {
            Console.WriteLine("DRAW");
        }
    }

    private static void WriteOutput(PokerHand h, int playerNum, Card[] c)
    {
        Console.WriteLine($"{playerNum} {PokerHandToString(h)} {Hand.ToCodinGameString(c)}");
    }


/*
 * HANDS
    A player's hand is the best 5 card-card combination from the 7 card combination of their own 2 hole cards with 
    the 5 community cards. A hand is one of the following types, in descending order of value:
    
• STRAIGHT_FLUSH - 5 consecutive cards of the same suit
• FOUR_OF_A_KIND - 4 cards of matching values and 1 kicker*
• FULL_HOUSE - 3 cards of matching values, and 2 of a different matching value
• FLUSH - 5 non-consecutive cards, all the same suit
• STRAIGHT - 5 consecutive cards
• THREE_OF_A_KIND - 3 cards of matching values and 2 kickers*
• TWO_PAIR - 2 cards of a matching value, 2 of a different matching value and 1 kicker*
• PAIR - 2 cards of matching values and 3 kickers*, e.g. 66T42
• HIGH_CARD - The absence of any better hand means that the highest value card counts with 4 kickers*, e.g.
 */
    private static string PokerHandToString(PokerHand h)
    {
        switch (h)
        {
            case PokerHand.HighCard:
                return "HIGH_CARD";
            case PokerHand.OnePair:
                return "PAIR";
            case PokerHand.TwoPair:
                return "TWO_PAIR";
            case PokerHand.ThreeOfAKind:
                return "THREE_OF_A_KIND";
            case PokerHand.Straight:
                return "STRAIGHT";
            case PokerHand.Flush:
                return "FLUSH";
            case PokerHand.FullHouse:
                return "FULL_HOUSE";
            case PokerHand.FourOfAKind:
                return "FOUR_OF_A_KIND";
            case PokerHand.StraightFlush:
                return "STRAIGHT_FLUSH";
            case PokerHand.RoyalFlush:
                return "STRAIGHT_FLUSH";
            default:
                throw new ArgumentOutOfRangeException(nameof(h), h, null);
        }
    }
}

/// <summary>
/// An enumeration of possible poker hands in rank order, so the higher the enumerated
/// value the better the hand. 
/// </summary>
public enum PokerHand
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,
    RoyalFlush
}

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

    public Card(CardSuit cs, CardValue cr)
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
    private Card(string cardString)
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
        if (val.Length != 2)
        {
            throw new PokerException("Incorrect length for card string. Must be 2 characters.");
        }

        string cr = val.Substring(0, 1).ToUpper();
        string cs = val.Substring(1, 1).ToUpper();
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
        var v = c.Value;
        switch (Value)
        {
            case CardValue.Two:
                return v == CardValue.Ace || v == CardValue.Three;
            case CardValue.Ace:
                return v == CardValue.King || v == CardValue.Two;
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
        return string.Format("{1} of {0}", Suit.ToString(), Value.ToString());
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
    public override string ToString()
    {
        var sb = new StringBuilder();
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

        return sb.ToString();
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
public enum CardSuit
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}

/// <summary>
/// Card rank.
/// </summary>
public enum CardValue
{
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

/// <summary>
/// Convenience methods for dealing with an entire deck of cards.
/// </summary>
public static class Deck
{
    /// <summary>
    /// An array of cards that makes up an entire playing deck for
    /// Texas Hold'em. 
    /// </summary>
    private static readonly Card[] _deck = Card.Cards.Values.ToArray();

    /// <summary>
    /// Get the full deck
    /// </summary>
    /// <returns></returns>
    public static List<Card> FullDeck()
    {
        return new List<Card>(_deck);
    }

    /// <summary>
    /// Get a deck, minus some cards.
    /// </summary>
    /// <param name="outs">list of cards NOT in the deck</param>
    /// <returns>The sub deck</returns>
    public static List<Card> GetSubDeck(IEnumerable<Card> outs)
    {
        var l = new List<Card>(_deck);
        foreach (var c in outs) l.Remove(c);
        return l;
    }

    /// <summary>
    /// Gets the paired values in a collection of cards.
    /// </summary>
    /// <param name="cards"></param>
    /// <returns>list of card values</returns>
    public static List<CardValue> Pairs(IEnumerable<Card> cards)
    {
        var varray = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
        var l = new List<CardValue>();
        foreach (var c in cards) varray[(int) c.Value] += 1;

        foreach (var v in Enum.GetValues(typeof(CardValue)))
            if (varray[(int) v] > 1)
            {
                l.Add((CardValue) v);
            }

        return l;
    }

    /// <summary>
    /// Counts the number of each suit in a collection of cards.
    /// </summary>
    /// <param name="cards">The card collection in question</param>
    /// <returns>Dictionary of suit counts</returns>
    public static Dictionary<CardSuit, int> Suited(IEnumerable<Card> cards)
    {
        var r = new Dictionary<CardSuit, int>
        {
            {CardSuit.Clubs, 0},
            {CardSuit.Diamonds, 0},
            {CardSuit.Hearts, 0},
            {CardSuit.Spades, 0}
        };
        foreach (var c in cards) r[c.Suit] += 1;
        return r;
    }

    /// <summary>
    /// Get all two card combinations from a collection of cards
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static IEnumerable<Tuple<Card, Card>> GetTwoCardCombinations(IEnumerable<Card> cards)
    {
        var l = new List<Tuple<Card, Card>>();
        var a = cards.ToArray();
        for (var i = 0; i < a.Length - 1; ++i)
        for (int j = i + 1; j < a.Length; ++j)
            l.Add(new Tuple<Card, Card>(a[i], a[j]));
        return l;
    }
}


public class NewHandComparer : IComparer<Hand>
{
    private readonly BrecherHandEvaluator eval;
    public NewHandComparer()
    {
        eval = new BrecherHandEvaluator();
    }
    public int Compare(Hand x, Hand y)
    {
        var h1 = x.GetHandCards(eval);
        var h2 = y.GetHandCards(eval);
        var s1 = BrecherHandEvaluator.GetHandStrength(new Hand(h1));
        var s2 = BrecherHandEvaluator.GetHandStrength(new Hand(h2));
        if (s1 < s2)
        {
            return -1;
        }

        if (s1 > s2)
        {
            return 1;
        }

        return 0;
    }
}



/// <summary>
/// A poker hand. A valid poker hand must have at least 5 cards.
/// More cards are allowed to be in the hand to allow implementations
/// of game of poker that involve more cards.
/// </summary>
public class Hand : IComparable<Hand>
{
    private readonly List<Card> _cards;

    private static NewHandComparer _comparer;

    public static NewHandComparer Comparer
    {
        get
        {
            if (_comparer == null)
            {
                _comparer = new NewHandComparer();
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
        _cards.Sort((card, card1) => card1.Value.CompareTo(card.Value));
    }

    public Hand(string handCards)
    {
        _cards = new List<Card>();
        foreach (string c in handCards.Split(" ".ToCharArray())) Add(Card.Parse(c));
        _cards.Sort((card, card1) => card1.Value.CompareTo(card.Value));
    }

    public Card[] Cards => _cards.ToArray();

    public void Discard(Card card)
    {
        _cards.Remove(card);
    }

    public int SuitCount(CardSuit s)
    {
        return _cards.Count(card => card.Suit == s);
    }

    public void Add(Card card)
    {
        _cards.Add(card);
        _cards.Sort((card1, card2) => card2.Value.CompareTo(card1.Value));
    }

    public void Add(IEnumerable<Card> cards)
    {
        _cards.AddRange(cards);
        _cards.Sort((card, card1) => card1.Value.CompareTo(card.Value));
    }

    public void Clear()
    {
        _cards.Clear();
    }

    public Card[] TopNot(CardValue v, int num=1)
    {
        var l = new List<Card>();

        foreach (var card in _cards)
        {
            if (card.Value != v)
            {
                l.Add(card);
            }

            if (l.Count == num)
                break;
        }

        return l.ToArray();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var c in _cards)
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

    // Retrieve the cards that make a hand
    public Card[] GetHandCards(IPokerHandEvaluator evaluator)
    {
        if (_cards.Count < 5)
        {
            return new Card[0];
        }

        switch (evaluator.Evaluate(this))
        {
            case PokerHand.HighCard:
                return GetHighCardHand();
            case PokerHand.OnePair:
                return GetPairHand();
            case PokerHand.TwoPair:
                return GetTwoPairHand();
            case PokerHand.ThreeOfAKind:
                return GetThreeOfAKindCards();
            case PokerHand.Straight:
                return GetStraightCards();
            case PokerHand.Flush:
                return GetFlushCards();
            case PokerHand.FullHouse:
                return GetFullHouseCards();
            case PokerHand.FourOfAKind:
                return GetFourOfAKindCards();
            case PokerHand.StraightFlush:
                return GetStraightFlushCards();
            case PokerHand.RoyalFlush:
                return GetStraightFlushCards();
            default:
                throw new ArgumentOutOfRangeException();
        }
        return new Card[0];
    }

    private Card[] GetHighCardHand()
    {
        var l = new List<Card>
        {
            _cards[0], _cards[1], _cards[2], _cards[3], _cards[4]
        };
        // return the first five cards in the list
        return l.ToArray();
    }

    private Card[] GetPairHand()
    {
        var t = new List<Card>();
        t.AddRange(_cards);
        var l = new List<Card>();
        for (var i = 0; i < t.Count - 1; ++i)
        {
            if (t[i].Value == t[i + 1].Value)
            {
                l.Add(t[i]);
                l.Add(t[i+1]);
                t.Remove(l[0]);
                t.Remove(l[1]);
                break;
            }
        }
        l.Add(t[0]);
        l.Add(t[1]);
        l.Add(t[2]);
        return l.ToArray();
    }
    private Card[] GetTwoPairHand()
    {
        var t = new List<Card>();
        t.AddRange(_cards);
        var l = new List<Card>();
        var pcount = 0;
        for (var i = 0; i < t.Count - 1; ++i)
        {
            if (t[i].Value == t[i + 1].Value)
            {
                l.Add(t[i]);
                l.Add(t[i + 1]);
                t.Remove(l[0]);
                t.Remove(l[1]);
                pcount++;
                if(pcount==2)
                    break;
            }
        }
        l.Add(t[0]);
        return l.ToArray();
    }

    private Card[] GetFlushCards()
    {
        var l = new List<Card>();
        var s = CardSuit.Clubs;
        if (SuitCount(CardSuit.Diamonds) >= 5)
        {
            s = CardSuit.Diamonds;
        }
        else if (SuitCount(CardSuit.Hearts) >= 5)
        {
            s = CardSuit.Hearts;
        }
        else if (SuitCount(CardSuit.Spades) >= 5)
        {
            s = CardSuit.Spades;
        }
        foreach (var card in _cards)
        {
            if (card.Suit == s)
            {
                l.Add(card);
            }
            if(l.Count==5)
                break;
        }

        return l.ToArray();
    }

    private Card[] GetThreeOfAKindCards()
    {
        var t = new List<Card>();
        t.AddRange(_cards);
        var l = new List<Card>();
        for (var i = 0; i < t.Count - 2; ++i)
        {
            if (t[i].Value == t[i + 1].Value && t[i + 1].Value == t[i + 2].Value)
            {
                l.Add(t[i]);
                l.Add(t[i + 1]);
                l.Add(t[i + 2]);
                t.Remove(l[0]);
                t.Remove(l[1]);
                t.Remove(l[2]);
                break;
            }
        }
        l.Add(t[0]);
        l.Add(t[1]);
        return l.ToArray();
    }

    private Card[] GetFourOfAKindCards()
    {
        var t = new List<Card>();
        t.AddRange(_cards);
        var l = new List<Card>();
        for (var i = 0; i < t.Count - 3; ++i)
        {
            if (t[i].Value == t[i + 1].Value && t[i + 1].Value == t[i + 2].Value && t[i + 2].Value == t[i + 3].Value)
            {
                l.Add(t[i]);
                l.Add(t[i + 1]);
                l.Add(t[i + 2]);
                l.Add(t[i + 3]);
                t.Remove(l[0]);
                t.Remove(l[1]);
                t.Remove(l[2]);
                t.Remove(l[3]);
                break;
            }
        }
        l.Add(t[0]);
        return l.ToArray();
    }

    public Card[] GetStraightCards()
    {
        var l = new List<Card>();
        l.Add(_cards[0]);
        for (var i = 1; i < _cards.Count; ++i)
        {
            var c = _cards[i];
            if (l.Last().IsConnected(c))
            {
                l.Add(c);
            }
            else
            {
                l.Clear();
                l.Add(c);
            }

            if (l.Count == 5)
            {
                break;
            }
        }

        if (l.Count == 4)
        {
            l.Add(_cards[0]);
        }

        return l.ToArray();
    }

    public Card[] GetFullHouseCards()
    {
        var t = new List<Card>();
        t.AddRange(_cards);
        var l = new List<Card>();
        for (var i = 0; i < t.Count - 2; ++i)
        {
            if (t[i].Value == t[i + 1].Value && t[i + 1].Value == t[i + 2].Value)
            {
                l.Add(t[i]);
                l.Add(t[i + 1]);
                l.Add(t[i + 2]);
                t.Remove(l[0]);
                t.Remove(l[1]);
                t.Remove(l[2]);
                break;
            }
        }
        for (var i = 0; i < t.Count - 1; ++i)
        {
            if (t[i].Value == t[i + 1].Value)
            {
                l.Add(t[i]);
                l.Add(t[i + 1]);
                t.Remove(l[0]);
                t.Remove(l[1]);
                break;
            }
        }
        return l.ToArray();
    }

    public Card[] GetStraightFlushCards()
    {
        var l = new List<Card>();
        var s = CardSuit.Clubs;
        if (SuitCount(CardSuit.Diamonds) >= 5)
        {
            s = CardSuit.Diamonds;
        }
        else if (SuitCount(CardSuit.Hearts) >= 5)
        {
            s = CardSuit.Hearts;
        }
        else if (SuitCount(CardSuit.Spades) >= 5)
        {
            s = CardSuit.Spades;
        }

        var first = _cards.FindIndex(card => card.Suit == s);
        l.Add(_cards[first]);
        for (var i = first + 1; i < _cards.Count; ++i)
        {
            var c = _cards[i];
            if (l.Last().IsConnected(c) && c.Suit==l.Last().Suit)
            {
                l.Add(c);
            }
            else
            {
                l.Clear();
                l.Add(c);
            }

            if (l.Count == 5)
            {
                break;
            }
        }

        if (l.Count == 4)
        {
            l.Add(_cards[0]);
        }

        return l.ToArray();
    }

    public static string ToCodinGameString(IEnumerable<Card> cards)
    {
        var sb = new StringBuilder();
        foreach (var card in cards)
        {
            sb.Append(card.ToString()[0]);
        }
        return sb.ToString();
    }

    
}

public class PokerException : Exception
{
    public PokerException(string msg) : base(msg)
    {
    }
}

/// <summary>
/// The interface for poker hand evaluator. By extracting the interface leaves open the
/// possibility of introducing different and perhaps more efficient methods of evaluating
/// poker hands. The idea is you pass the evaluator a proper <see cref="Hand">hand</see>, and it can tell you 
/// what kind of hand it is. 
/// </summary>
public interface IPokerHandEvaluator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="h"></param>
    /// <returns></returns>
    PokerHand Evaluate(Hand h);
}

/// <summary>
/// BrecherHandEvaluator is an IPokerHandEvaluator which uses the Steve Brecher's
/// method of determining poker hands. 
/// </summary>
public class BrecherHandEvaluator : IPokerHandEvaluator
{
    
    public PokerHand Evaluate(Hand h)
    {
        int strength = GetHandStrength(h);
        if (strength < BrecherHandEval.PAIR)
        {
            return PokerHand.HighCard;
        }

        if (strength < BrecherHandEval.TWO_PAIR)
        {
            return PokerHand.OnePair;
        }

        if (strength < BrecherHandEval.THREE_OF_A_KIND)
        {
            return PokerHand.TwoPair;
        }

        if (strength < BrecherHandEval.STRAIGHT)
        {
            return PokerHand.ThreeOfAKind;
        }

        if (strength < BrecherHandEval.FLUSH)
        {
            return PokerHand.Straight;
        }

        if (strength < BrecherHandEval.FULL_HOUSE)
        {
            return PokerHand.Flush;
        }

        if (strength < BrecherHandEval.FOUR_OF_A_KIND)
        {
            return PokerHand.FullHouse;
        }

        if (strength < BrecherHandEval.STRAIGHT_FLUSH)
        {
            return PokerHand.FourOfAKind;
        }

        if (h.Cards.Any(c => c.Value == CardValue.Ten) &&
            h.Cards.Any(c => c.Value == CardValue.Jack) &&
            h.Cards.Any(c => c.Value == CardValue.Queen) &&
            h.Cards.Any(c => c.Value == CardValue.King) &&
            h.Cards.Any(c => c.Value == CardValue.Ace))
        {
            return PokerHand.RoyalFlush;
        }

        return PokerHand.StraightFlush;
    }

    public static PokerHand StrengthToPokerHand(int strength)
    {
        if (strength < BrecherHandEval.PAIR)
        {
            return PokerHand.HighCard;
        }

        if (strength < BrecherHandEval.TWO_PAIR)
        {
            return PokerHand.OnePair;
        }

        if (strength < BrecherHandEval.THREE_OF_A_KIND)
        {
            return PokerHand.TwoPair;
        }

        if (strength < BrecherHandEval.STRAIGHT)
        {
            return PokerHand.ThreeOfAKind;
        }

        if (strength < BrecherHandEval.FLUSH)
        {
            return PokerHand.Straight;
        }

        if (strength < BrecherHandEval.FULL_HOUSE)
        {
            return PokerHand.Flush;
        }

        if (strength < BrecherHandEval.FOUR_OF_A_KIND)
        {
            return PokerHand.FullHouse;
        }

        if (strength < BrecherHandEval.STRAIGHT_FLUSH)
        {
            return PokerHand.FourOfAKind;
        }

        return PokerHand.StraightFlush;
    }

    /// <summary>
    /// Determine the probability of getting each type of hand when the turn and river cards are turned.
    /// </summary>
    /// <param name="playerCards">The players hole cards.</param>
    /// <param name="tableCards">The cards on the table</param>
    /// <returns>
    /// Probability array
    /// </returns>
    public static float[] FlopProbabilities(IEnumerable<Card> playerCards, IEnumerable<Card> tableCards)
    {
        var p = NewProbabilityArray();
        float[] counts = {0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
        var total = 0.0f;
        var knownCards = new List<Card>(playerCards);
        knownCards.AddRange(tableCards);
        long startingCode = knownCards.Sum(card => CardToCode(card));
        var remainingDeck = Deck.GetSubDeck(knownCards);
        var possibilities = Deck.GetTwoCardCombinations(remainingDeck);
        foreach (var possibility in possibilities)
        {
            long handCode = startingCode + CardToCode(possibility.Item1) + CardToCode(possibility.Item2);
            var hand = StrengthToPokerHand(BrecherHandEval.Hand7Eval(handCode));
            counts[(int) hand] += 1.0f;
            total += 1.0f;
        }

        // avoid division by 0
        if (Math.Abs(total) < 1.0f)
        {
            return p;
        }

        for (var i = 0; i < p.Length; ++i) p[i] = counts[i] / total;
        return p;
    }

    /// <summary>
    /// Determine the probability of getting each type of hand when the river card is turned.
    /// </summary>
    /// <param name="playerCards">The players hole cards.</param>
    /// <param name="tableCards">The cards on the table</param>
    /// <returns>
    /// Probability array
    /// </returns>
    public static float[] TurnProbabilities(IEnumerable<Card> playerCards, IEnumerable<Card> tableCards)
    {
        var p = NewProbabilityArray();
        float[] counts = {0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
        var total = 0.0f;
        var knownCards = new List<Card>(playerCards);
        knownCards.AddRange(tableCards);
        long startingCode = knownCards.Sum(card => CardToCode(card));
        var possibities = Deck.GetSubDeck(knownCards);
        foreach (var possibility in possibities)
        {
            long handCode = startingCode + CardToCode(possibility);
            var hand = StrengthToPokerHand(BrecherHandEval.Hand7Eval(handCode));
            counts[(int) hand] += 1.0f;
            total += 1.0f;
        }

        // avoid division by 0
        if (Math.Abs(total) < 1.0f)
        {
            return p;
        }

        for (var i = 0; i < p.Length; ++i) p[i] = counts[i] / total;
        return p;
    }

    /// <summary>
    /// The probability array keeps track of the probability of getting each type
    /// of poker hand HIGH CARD to STRAIGHT FLUSH.
    /// </summary>
    /// <returns>Newly initialized 9 member float array</returns>
    private static float[] NewProbabilityArray()
    {
        return new[] {0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
    }

    /// <summary>
    /// With all cards turned calculates the percent chance of winning a showdown.
    /// </summary>
    /// <param name="playerCards"></param>
    /// <param name="tableCards"></param>
    /// <returns></returns>
    public static float WinningHandPercent(IEnumerable<Card> playerCards, IEnumerable<Card> tableCards)
    {
        var pc = playerCards.ToArray();
        var tc = tableCards.ToArray();
        var knownCards = new List<Card>(pc);
        knownCards.AddRange(tc);
        long myCode = pc.Sum(card => CardToCode(card));
        long tableCode = tc.Sum(card => CardToCode(card));
        long myHandCode = myCode + tableCode;
        int myStrength = BrecherHandEval.Hand7Eval(myHandCode);
        var remainingDeck = Deck.GetSubDeck(knownCards);
        var possibilities = Deck.GetTwoCardCombinations(remainingDeck);
        var wins = 0.0f;
        var total = 0.0f;
        foreach (var possibility in possibilities)
        {
            total += 1.0f;
            long handCode = tableCode + CardToCode(possibility.Item1) + CardToCode(possibility.Item2);
            int handStrength = BrecherHandEval.Hand7Eval(handCode);
            if (myStrength >= handStrength)
            {
                wins += 1.0f;
            }
        }

        // avoid division by 0
        if (Math.Abs(total) < 1.0f)
        {
            return 0.0f;
        }

        return wins / total;
    }

    public static int GetHandStrength(Hand h)
    {
        return GetHandStrength(HandToCode(h), h.Cards.Length);
    }

    private static int GetHandStrength(long handCode, int handSize)
    {
        switch (handSize)
        {
            case 5:
                return BrecherHandEval.Hand5Eval(handCode);
            case 6:
                return BrecherHandEval.Hand6Eval(handCode);
            case 7:
                return BrecherHandEval.Hand7Eval(handCode);
            default:
                return 0;
        }
    }


    public static long HandToCode(Hand h)
    {
        long handCode = h.Cards.Sum(card => CardToCode(card));
        return handCode;
    }

    public static long CardToCode(Card c)
    {
        return 1L << (16 * SuitToNumber(c.Suit) + ValueToNumber(c.Value));
    }

    private static int SuitToNumber(CardSuit s)
    {
        switch (s)
        {
            case CardSuit.Spades:
                return 0;
            case CardSuit.Hearts:
                return 1;
            case CardSuit.Clubs:
                return 2;
            case CardSuit.Diamonds:
                return 3;
        }

        return 0;
    }

    private static int ValueToNumber(CardValue v)
    {
        switch (v)
        {
            case CardValue.Two: return 0;
            case CardValue.Three: return 1;
            case CardValue.Four: return 2;
            case CardValue.Five: return 3;
            case CardValue.Six: return 4;
            case CardValue.Seven: return 5;
            case CardValue.Eight: return 6;
            case CardValue.Nine: return 7;
            case CardValue.Ten: return 8;
            case CardValue.Jack: return 9;
            case CardValue.Queen: return 10;
            case CardValue.King: return 11;
            case CardValue.Ace: return 12;
        }

        return 0;
    }
}

/**********************************************************************************************************
 * BrecherHandEval is a conversion of the HandEval.java code to C#. The Java code
 * was a conversion of a C library written by Steve Brecher.
 *
 * You can find the original Java code that was the basis for this code here:
 * https://github.com/theaigames/texasholdem-engine/blob/master/com/stevebrecher/HandEval.java
 **********************************************************************************************************
 * I've never seen the C code, I've only heard the rumors.
 *
 * Non-instantiable class containing a variety of static poker hand evaluation and related utility methods.
 * <p>
 * All of the methods are thread-safe.
 * <p>
 * Each evaluation method takes a single parameter representing a hand of five to
 * seven cards represented within a long (64 bits).  The long is considered as
 * composed of four 16-bit fields, one for each suit.  The ordering of these
 * 16-bit fields within the long, i.e., the correspondence of each to a specific
 * suit, is immaterial.  Within each suit's 16-bit field, the least-significant
 * 13 bits (masked by 0x1FFF) are flags representing the presence of ranks in
 * that suit, where bit 0 set (0x0001) for a deuce, ..., bit 12 set (0x1000) for
 * an ace.  The values of the unused most-significant three bits within each
 * 16-bit suit field are immaterial.
 * <p>
 * A hand parameter can be built by encoding a {@link CardSet} or by bitwise
 * OR-ing, or adding, the encoded values of individual {@link Card}s.  These
 * encodings are returned by an {@link #encode encode} method.
 * <p>
 * Different methods are used for high and for lowball evaluation.
 * <p>
 * For high evaluation if results R1 > R2, hand 1 beats hand 2;
 * for lowball evaluation if results R1 > R2, hand 2 beats hand 1.
 * <p>
 * Evaluation result in 32 bits = 0x0V0RRRRR where V, R are
 * hex digits or "nybbles" (half-bytes).
 * <p>
 * V nybble = category code ranging from {@link HandCategory#NO_PAIR}<code>.ordinal()</code>
 *                                    to {@link HandCategory#STRAIGHT_FLUSH}<code>.ordinal()</code>
 * <p>
 * The R nybbles are the significant ranks (0..12), where 0 is the deuce
 * in a high result (Ace is 12, 0xC), and for lowball 0 is the Ace
 * (King is 0xC).  The Rs may be considered to consist of Ps for ranks
 * which determine the primary value of the hand, and Ks for kickers
 * where applicable.  Ordering is left-to-right:  first the Ps, then
 * any Ks, then padding with 0s.  Because 0 is a valid rank, to
 * interpret a result you must know how many ranks are significant,
 * which is a function of the hand category and whether high or lowball.
 * Examples: for a one-pair hand there are four significant ranks,
 * that of the pair and of the three kickers; for a straight, there is
 * one significant rank, that of the highest in the hand.
 * <p>
 * Common-card (board) games are assumed in determining the number of
 * significant ranks.  For example, a kicker value is returned for quads even
 * though it wouldn't be significant in a draw game.
 * <p><pre>
 * Examples of ...Eval method results (high where not indicated):
 *  Royal flush: 0x080C0000
 *  Four of a kind, Queens, with a 5 kicker:  0x070A3000
 *  Threes full of eights:  0x06016000
 *  Straight to the five (wheel): 0x04030000 (high)
 *  Straight to the five (wheel): 0x04040000 (lowball)
 *  One pair, deuces (0x0), with A65: 0x0100C430 (high)
 *  One pair, deuces (0x1), with 65A: 0x01015400 (lowball)
 *  No pair, KJT85: 0x000B9863
 *  Razz, wheel:  0x00043210</pre>
 * For the eight-or-better lowball ..._Eval functions, the result is
 * either as above or the constant {@link #NO_8_LOW}.  NO_8_LOW > any other
 * ..._Eval function result.
 * <p>
 * @version 2010Jun25.1
 * @author Steve Brecher
 *
 */
internal static class BrecherHandEval
{
    private const int _RANK_SHIFT_1 = 4;
    private const int _RANK_SHIFT_2 = _RANK_SHIFT_1 + 4;
    private const int _RANK_SHIFT_3 = _RANK_SHIFT_2 + 4;
    private const int _RANK_SHIFT_4 = _RANK_SHIFT_3 + 4;
    public const int VALUE_SHIFT = _RANK_SHIFT_4 + 8;

    public const int NO_PAIR = 0;
    public const int PAIR = NO_PAIR + (1 << VALUE_SHIFT);
    public const int TWO_PAIR = PAIR + (1 << VALUE_SHIFT);
    public const int THREE_OF_A_KIND = TWO_PAIR + (1 << VALUE_SHIFT);
    public const int STRAIGHT = THREE_OF_A_KIND + (1 << VALUE_SHIFT);
    public const int FLUSH = STRAIGHT + (1 << VALUE_SHIFT);
    public const int FULL_HOUSE = FLUSH + (1 << VALUE_SHIFT);
    public const int FOUR_OF_A_KIND = FULL_HOUSE + (1 << VALUE_SHIFT);
    public const int STRAIGHT_FLUSH = FOUR_OF_A_KIND + (1 << VALUE_SHIFT);
    public const int NO_8_LOW = STRAIGHT_FLUSH + (1 << VALUE_SHIFT);

    private const int _ARRAY_SIZE = 0x1FC0 + 1; // all combos of up to 7 of LS 13 bits on

    /* Arrays for which index is bit mask of card ranks in hand: */
    private static readonly int[]
        StraightValue =
            new int[_ARRAY_SIZE]; // Value(STRAIGHT) | (straight's high card rank-2 (3..12) << RANK_SHIFT_4); 0 if no straight

    private static readonly int[] NbrOfRanks = new int[_ARRAY_SIZE]; // count of bits set
    private static readonly int[] HiRank = new int[_ARRAY_SIZE]; // 4-bit card rank of highest bit set, right justified

    private static readonly int[]
        HiUpTo5Ranks = new int[_ARRAY_SIZE]; // 4-bit card ranks of highest (up to) 5 bits set, right-justified

    private static readonly int[]
        LoMaskOrNo8Low = new int[_ARRAY_SIZE]; // low-order 5 of the low-order 8 bits set, or NO_8_LOW; Ace is LS bit.

    private static readonly int[]
        Lo3_8ObRanksMask = new int[_ARRAY_SIZE]; // bits other than lowest 3 8-or-better reset; Ace is LS bit.

    private static int FlushAndOrStraight7(int ranks, int c, int d, int h, int s)
    {
        int i, j;

        if ((j = NbrOfRanks[c]) > 7 - 5)
        {
            // there's either a club flush or no flush
            if (j >= 5)
            {
                if ((i = StraightValue[c]) == 0)
                {
                    return FLUSH | HiUpTo5Ranks[c];
                }
                else
                {
                    return STRAIGHT_FLUSH - STRAIGHT + i;
                }
            }
        }
        else if ((j += i = NbrOfRanks[d]) > 7 - 5)
        {
            if (i >= 5)
            {
                if ((i = StraightValue[d]) == 0)
                {
                    return FLUSH | HiUpTo5Ranks[d];
                }
                else
                {
                    return STRAIGHT_FLUSH - STRAIGHT + i;
                }
            }
        }
        else if ((j += i = NbrOfRanks[h]) > 7 - 5)
        {
            if (i >= 5)
            {
                if ((i = StraightValue[h]) == 0)
                {
                    return FLUSH | HiUpTo5Ranks[h];
                }
                else
                {
                    return STRAIGHT_FLUSH - STRAIGHT + i;
                }
            }
        }
        else
            /* total cards in other suits <= 7-5: spade flush: */
        if ((i = StraightValue[s]) == 0)
        {
            return FLUSH | HiUpTo5Ranks[s];
        }
        else
        {
            return STRAIGHT_FLUSH - STRAIGHT + i;
        }

        return StraightValue[ranks];
    }

    public static int Hand7Eval(long hand)
    {
        int i, j, ranks;

        /*
         * The parameter contains four 16-bit fields; in each, the low-order
         * 13 bits are significant.  Get the respective fields into variables.
         * We don't care which suit is which; we arbitrarily call them c,d,h,s.
         */
        int c = (int) hand & 0x1FFF;
        int d = (int) ((ulong) hand >> 16) & 0x1FFF;
        int h = (int) ((ulong) hand >> 32) & 0x1FFF;
        int s = (int) ((ulong) hand >> 48) & 0x1FFF;

        switch (NbrOfRanks[ranks = c | d | h | s])
        {
            case 2:
                /*
                 * quads with trips kicker
                 */
                i = c & d & h & s; /* bit for quads */
                return FOUR_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[i ^ ranks] << _RANK_SHIFT_3);

            case 3:
                /*
                 * trips and pair (full house) with non-playing pair,
                 * or two trips (full house) with non-playing singleton,
                 * or quads with pair and singleton
                 */
                /* bits for singleton, if any, and trips, if any: */
                if (NbrOfRanks[i = c ^ d ^ h ^ s] == 3)
                {
                    /* two trips (full house) with non-playing singleton */
                    if (NbrOfRanks[i = c & d] != 2)
                    {
                        if (NbrOfRanks[i = c & h] != 2)
                        {
                            if (NbrOfRanks[i = c & s] != 2)
                            {
                                if (NbrOfRanks[i = d & h] != 2)
                                {
                                    if (NbrOfRanks[i = d & s] != 2)
                                    {
                                        i = h & s; /* bits for the trips */
                                    }
                                }
                            }
                        }
                    }

                    return FULL_HOUSE | (HiUpTo5Ranks[i] << _RANK_SHIFT_3);
                }

                if ((j = c & d & h & s) != 0) /* bit for quads */
                    /* quads with pair and singleton */
                {
                    return FOUR_OF_A_KIND | (HiRank[j] << _RANK_SHIFT_4) | (HiRank[ranks ^ j] << _RANK_SHIFT_3);
                }

                /* trips and pair (full house) with non-playing pair */
                return FULL_HOUSE | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[ranks ^ i] << _RANK_SHIFT_3);

            case 4:
                /*
                 * three pair and singleton,
                 * or trips and pair (full house) and two non-playing singletons,
                 * or quads with singleton kicker and two non-playing singletons
                 */
                i = c ^ d ^ h ^ s; // the bit(s) of the trips, if any, and singleton(s)
                if (NbrOfRanks[i] == 1)
                {
                    /* three pair and singleton */
                    j = HiUpTo5Ranks[ranks ^ i]; /* ranks of the three pairs */
                    return TWO_PAIR | ((j & 0x0FF0) << _RANK_SHIFT_2) |
                           (HiRank[i | (1 << (j & 0x000F))] << _RANK_SHIFT_2);
                }

                if ((j = c & d & h & s) == 0)
                {
                    // trips and pair (full house) and two non-playing singletons
                    i ^= ranks; /* bit for the pair */
                    if ((j = c & d & ~i) == 0)
                    {
                        j = h & s & ~i; /* bit for the trips */
                    }

                    return FULL_HOUSE | (HiRank[j] << _RANK_SHIFT_4) | (HiRank[i] << _RANK_SHIFT_3);
                }

                // quads with singleton kicker and two non-playing singletons
                return FOUR_OF_A_KIND | (HiRank[j] << _RANK_SHIFT_4) | (HiRank[i] << _RANK_SHIFT_3);

            case 5:
                /*
                 * flush and/or straight,
                 * or two pair and three singletons,
                 * or trips and four singletons
                 */
                if ((i = FlushAndOrStraight7(ranks, c, d, h, s)) != 0)
                {
                    return i;
                }

                i = c ^ d ^ h ^ s; // the bits of the trips, if any, and singletons
                if (NbrOfRanks[i] != 5)
                    /* two pair and three singletons */
                {
                    return TWO_PAIR | (HiUpTo5Ranks[i ^ ranks] << _RANK_SHIFT_3) | (HiRank[i] << _RANK_SHIFT_2);
                }

                /* trips and four singletons */
                if ((j = c & d) == 0)
                {
                    j = h & s;
                }

                // j has trips bit
                return THREE_OF_A_KIND | (HiRank[j] << _RANK_SHIFT_4) | (HiUpTo5Ranks[i ^ j] & 0x0FF00);

            case 6:
                /*
                 * flush and/or straight,
                 * or one pair and three kickers and two non-playing singletons
                 */
                if ((i = FlushAndOrStraight7(ranks, c, d, h, s)) != 0)
                {
                    return i;
                }

                i = c ^ d ^ h ^ s; /* the bits of the five singletons */
                return PAIR | (HiRank[ranks ^ i] << _RANK_SHIFT_4) | ((HiUpTo5Ranks[i] & 0x0FFF00) >> _RANK_SHIFT_1);

            case 7:
                /*
                 * flush and/or straight or no pair
                 */
                if ((i = FlushAndOrStraight7(ranks, c, d, h, s)) != 0)
                {
                    return i;
                }

                return NO_PAIR | HiUpTo5Ranks[ranks];
        } /* end switch */

        return 0; /* never reached, but avoids compiler warning */
    }

    private static int FlushAndOrStraight6(int ranks, int c, int d, int h, int s)
    {
        int i, j;

        if ((j = NbrOfRanks[c]) > 6 - 5)
        {
            // there's either a club flush or no flush
            if (j >= 5)
            {
                if ((i = StraightValue[c]) == 0)
                {
                    return FLUSH | HiUpTo5Ranks[c];
                }
                else
                {
                    return STRAIGHT_FLUSH - STRAIGHT + i;
                }
            }
        }
        else if ((j += i = NbrOfRanks[d]) > 6 - 5)
        {
            if (i >= 5)
            {
                if ((i = StraightValue[d]) == 0)
                {
                    return FLUSH | HiUpTo5Ranks[d];
                }
                else
                {
                    return STRAIGHT_FLUSH - STRAIGHT + i;
                }
            }
        }
        else if ((j += i = NbrOfRanks[h]) > 6 - 5)
        {
            if (i >= 5)
            {
                if ((i = StraightValue[h]) == 0)
                {
                    return FLUSH | HiUpTo5Ranks[h];
                }
                else
                {
                    return STRAIGHT_FLUSH - STRAIGHT + i;
                }
            }
        }
        else
            /* total cards in other suits <= N-5: spade flush: */
        if ((i = StraightValue[s]) == 0)
        {
            return FLUSH | HiUpTo5Ranks[s];
        }
        else
        {
            return STRAIGHT_FLUSH - STRAIGHT + i;
        }

        return StraightValue[ranks];
    }

    /**
     * Returns the value of the best 5-card high poker hand from 6 cards.
     * @param hand bit mask with one bit set for each of 6 cards.
     * @return the value of the best 5-card high poker hand.
     */

    public static int Hand6Eval(long hand)
    {
        int c = (int) hand & 0x1FFF;
        int d = (int) ((ulong) hand >> 16) & 0x1FFF;
        int h = (int) ((ulong) hand >> 32) & 0x1FFF;
        int s = (int) ((ulong) hand >> 48) & 0x1FFF;

        int ranks = c | d | h | s;
        int i;

        switch (NbrOfRanks[ranks])
        {
            case 2: /* quads with pair kicker,
				   or two trips (full house) */
                /* bits for trips, if any: */
                if (NbrOfRanks[i = c ^ d ^ h ^ s] != 0)
                    /* two trips (full house) */
                {
                    return FULL_HOUSE | (HiUpTo5Ranks[i] << _RANK_SHIFT_3);
                }

                /* quads with pair kicker */
                i = c & d & h & s; /* bit for quads */
                return FOUR_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[i ^ ranks] << _RANK_SHIFT_3);

            case 3: /* quads with singleton kicker and non-playing singleton,
				   or full house with non-playing singleton,
				   or two pair with non-playing pair */
                if ((c ^ d ^ h ^ s) == 0)
                    /* no trips or singletons:  three pair */
                {
                    return TWO_PAIR | (HiUpTo5Ranks[ranks] << _RANK_SHIFT_2);
                }

                if ((i = c & d & h & s) == 0)
                {
                    /* full house with singleton */
                    if ((i = c & d & h) == 0)
                    {
                        if ((i = c & d & s) == 0)
                        {
                            if ((i = c & h & s) == 0)
                            {
                                i = d & h & s; /* bit of trips */
                            }
                        }
                    }

                    int j = c ^ d ^ h ^ s;
                    return FULL_HOUSE | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[j ^ ranks] << _RANK_SHIFT_3);
                }

                /* quads with kicker and singleton */
                return FOUR_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[i ^ ranks] << _RANK_SHIFT_3);

            case 4: /* trips and three singletons,
				   or two pair and two singletons */
                if ((i = c ^ d ^ h ^ s) != ranks)
                    /* two pair and two singletons */
                {
                    return TWO_PAIR | (HiUpTo5Ranks[i ^ ranks] << _RANK_SHIFT_3) | (HiRank[i] << _RANK_SHIFT_2);
                }

                /* trips and three singletons */
                if ((i = c & d) == 0)
                {
                    i = h & s; /* bit of trips */
                }

                return THREE_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4) |
                       ((HiUpTo5Ranks[ranks ^ i] & 0x00FF0) << _RANK_SHIFT_1);

            case 5: /* flush and/or straight,
				   or one pair and three kickers and
					one non-playing singleton */
                if ((i = FlushAndOrStraight6(ranks, c, d, h, s)) != 0)
                {
                    return i;
                }

                i = c ^ d ^ h ^ s; /* the bits of the four singletons */
                return PAIR | (HiRank[i ^ ranks] << _RANK_SHIFT_4) | (HiUpTo5Ranks[i] & 0x0FFF0);

            case 6: /* flush and/or straight or no pair */
                if ((i = FlushAndOrStraight6(ranks, c, d, h, s)) != 0)
                {
                    return i;
                }

                return NO_PAIR | HiUpTo5Ranks[ranks];
        } /* end switch */

        return 0; /* never reached, but avoids compiler warning */
    }

    /**
     * Returns the value of a 5-card poker hand.
     * @param hand bit mask with one bit set for each of 5 cards.
     * @return the value of the hand.
     */
    public static int Hand5Eval(long hand)
    {
        int c = (int) hand & 0x1FFF;
        int d = (int) ((ulong) hand >> 16) & 0x1FFF;
        int h = (int) ((ulong) hand >> 32) & 0x1FFF;
        int s = (int) ((ulong) hand >> 48) & 0x1FFF;

        int ranks = c | d | h | s;
        int i;

        switch (NbrOfRanks[ranks])
        {
            case 2: /* quads or full house */
                i = c & d; /* any two suits */
                if ((i & h & s) == 0)
                {
                    /* no bit common to all suits */
                    i = c ^ d ^ h ^ s; /* trips bit */
                    return FULL_HOUSE | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[i ^ ranks] << _RANK_SHIFT_3);
                }
                else
                    /* the quads bit must be present in each suit mask,
	                   but the kicker bit in no more than one; so we need
	                   only AND any two suit masks to get the quad bit: */
                {
                    return FOUR_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4) | (HiRank[i ^ ranks] << _RANK_SHIFT_3);
                }

            case 3: /* trips and two kickers,
	               or two pair and kicker */
                if ((i = c ^ d ^ h ^ s) == ranks)
                {
                    /* trips and two kickers */
                    if ((i = c & d) == 0)
                    {
                        if ((i = c & h) == 0)
                        {
                            i = d & h;
                        }
                    }

                    return THREE_OF_A_KIND | (HiRank[i] << _RANK_SHIFT_4)
                                           | (HiUpTo5Ranks[i ^ ranks] << _RANK_SHIFT_2);
                }

                /* two pair and kicker; i has kicker bit */
                return TWO_PAIR | (HiUpTo5Ranks[i ^ ranks] << _RANK_SHIFT_3) | (HiRank[i] << _RANK_SHIFT_2);

            case 4: /* pair and three kickers */
                i = c ^ d ^ h ^ s; /* kicker bits */
                return PAIR | (HiRank[ranks ^ i] << _RANK_SHIFT_4) | (HiUpTo5Ranks[i] << _RANK_SHIFT_1);

            case 5: /* flush and/or straight, or no pair */
                if ((i = StraightValue[ranks]) == 0)
                {
                    i = HiUpTo5Ranks[ranks];
                }

                if (c != 0)
                {
                    /* if any clubs... */
                    if (c != ranks) /*   if no club flush... */
                    {
                        return i;
                    }
                } /*      return straight or no pair value */
                else if (d != 0)
                {
                    if (d != ranks)
                    {
                        return i;
                    }
                }
                else if (h != 0)
                {
                    if (h != ranks)
                    {
                        return i;
                    }
                }

                /*	else s == ranks: spade flush */
                /* There is a flush */
                if (i < STRAIGHT)
                    /* no straight */
                {
                    return FLUSH | i;
                }
                else
                {
                    return STRAIGHT_FLUSH - STRAIGHT + i;
                }
        }

        return 0; /* never reached, but avoids compiler warning */
    }
    /** ********** Initialization ********************** */

    private const int ACE_RANK = 14;

    private const int A5432 = 0x0000100F; // A5432

    // initializer block
    static BrecherHandEval()
    {
        int mask;
        for (mask = 1; mask < _ARRAY_SIZE; ++mask)
        {
            int ranks;
            int bitCount = ranks = 0;
            int shiftReg = mask;
            int i;
            for (i = ACE_RANK - 2; i >= 0; --i, shiftReg <<= 1)
                if ((shiftReg & 0x1000) != 0)
                {
                    if (++bitCount <= 5)
                    {
                        ranks <<= _RANK_SHIFT_1;
                        ranks += i;
                        if (bitCount == 1)
                        {
                            HiRank[mask] = i;
                        }
                    }
                }

            HiUpTo5Ranks[mask] = ranks;
            NbrOfRanks[mask] = bitCount;

            LoMaskOrNo8Low[mask] = NO_8_LOW;
            int value;
            bitCount = value = 0;
            shiftReg = mask;
            /* For the purpose of this loop, Ace is low; it's in the LS bit */
            for (i = 0; i < 8; ++i, shiftReg >>= 1)
                if ((shiftReg & 1) != 0)
                {
                    value |= 1 << i; /* undo previous shifts, copy bit */
                    if (++bitCount == 3)
                    {
                        Lo3_8ObRanksMask[mask] = value;
                    }

                    if (bitCount == 5)
                    {
                        LoMaskOrNo8Low[mask] = value;
                        break;
                    }
                }
        }

        for (mask = 0x1F00 /* A..T */; mask >= 0x001F /* 6..2 */; mask >>= 1)
            SetStraight(mask);
        SetStraight(A5432); /* A,5..2 */
    }

    private static void SetStraight(int ts)
    {
        /* must call with ts from A..T to 5..A in that order */

        int i, j;

        for (i = 0x1000; i > 0; i >>= 1)
        for (j = 0x1000; j > 0; j >>= 1)
        {
            int es = ts | i | j;
            if (StraightValue[es] == 0)
            {
                if (ts == A5432)
                {
                    StraightValue[es] = STRAIGHT | ((5 - 2) << _RANK_SHIFT_4);
                }
                else
                {
                    StraightValue[es] = STRAIGHT | (HiRank[ts] << _RANK_SHIFT_4);
                }
            }
        }
    }
}

