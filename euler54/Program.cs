using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AWA.Poker;

namespace euler54
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] lines = File.ReadAllLines("poker.txt");
            int wins = 0;
            int index = 0;
            foreach(var line in lines)
            {
                ++index;
                wins += Play(line, index);
            }
            Console.WriteLine(wins.ToString());
            //Console.ReadLine();
        }

        public static int Play(string line, int i)
        {
            string p1Hand = new string(line.Take(14).ToArray());
            string p2Hand = new string(line.Skip(15).ToArray());
            var h1 = new Hand(p1Hand);
            var h2 = new Hand(p2Hand);
            int val = h1.CompareTo(h2);
            string result = "LOSE";
            if (val > 0) result = "WIN";
            if (val == 0)
            {
                result = "TIE";
            }
            Console.WriteLine("{0}. {1} {2} {3}",i, HandToString(h1), HandToString(h2), result);
            if (val > 0) return 1;
            return 0;
        }

        public static string HandToString(Hand h)
        {
            var a = h.Cards.OrderBy(c => c.Value).ToArray();
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            sb.Append(CardToString(a[0]));
            for (int i = 1; i < a.Length; ++i)
            {
                sb.Append(", ");
                sb.Append(CardToString(a[i]));
            }
            sb.Append("]");
            return sb.ToString();
        }

        public static string CardToString(Card c)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            switch (c.Value)
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
                    sb.Append("10");
                    break;
                case CardValue.Jack:
                    sb.Append("11");
                    break;
                case CardValue.Queen:
                    sb.Append("12");
                    break;
                case CardValue.King:
                    sb.Append("13");
                    break;
                case CardValue.Ace:
                    sb.Append("14");
                    break;
            }
            sb.Append(", '");
            switch (c.Suit)
            {
                case CardSuit.Clubs:
                    sb.Append("C')");
                    break;
                case CardSuit.Diamonds:
                    sb.Append("D')");
                    break;
                case CardSuit.Hearts:
                    sb.Append("H')");
                    break;
                case CardSuit.Spades:
                    sb.Append("S')");
                    break;
            }
            return sb.ToString();
        }
    }
}
