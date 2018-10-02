using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemBot.Poker;

namespace TexasHoldemBot
{
    public enum BetRound
    {
        PreFlop,
        Flop,
        Turn,
        River
    }

    /// <summary>
    /// GameState is a pure data object that is used to track the state of the
    /// contest. 
    /// </summary>
    public class GameState
    {
        public GameState() : this(0, BetRound.PreFlop, 0, 0, 0, 0, 0, "", "", 0, 0)
        {
        }

        public GameState(int timeBank, BetRound betRound, int roundNumber, int handsPerBlindLevel, int initialBigBlind, int timePerMove, int maxTimeBank, string myName, string onButtonPlayer, int pot, int amountToCall)
        {
            TimeBank = timeBank;
            BetRound = betRound;
            RoundNumber = roundNumber;
            HandsPerBlindLevel = handsPerBlindLevel;
            InitialBigBlind = initialBigBlind;
            TimePerMove = timePerMove;
            MaxTimeBank = maxTimeBank;
            MyName = myName;
            OtherName = "";
            OnButtonPlayer = onButtonPlayer;
            Pot = pot;
            AmountToCall = amountToCall;
            Players=new Dictionary<string, Player>();
            Table = new Table();
            PlayerNames = new[] {"", ""};
        }

        public string[] PlayerNames { get; private set; }

        /// <summary>
        /// Maximum time in milliseconds that your bot can have in its time bank.
        /// </summary>
        public int MaxTimeBank { get; set; }

        /// <summary>
        /// Time in milliseconds that is added to your bot's time bank each move.
        /// </summary>
        public int TimePerMove { get; set; }

        /// <summary>
        /// The initial height of the big blind.
        /// </summary>
        public int InitialBigBlind { get; set; }

        /// <summary>
        /// The amount of hands before the blinds are increased.
        /// </summary>
        public int HandsPerBlindLevel { get; set; }

        /// <summary>
        /// The current round number.
        /// </summary>
        public int RoundNumber { get; set; }

        /// <summary>
        /// The current bet round.
        /// </summary>
        public BetRound BetRound { get; set; }

        public void SetBetRound(string value)
        {
            switch (value)
            {
                case "preflop":
                    BetRound = BetRound.PreFlop;
                    break;
                case "flop":
                    BetRound = BetRound.Flop;
                    break;
                case "turn":
                    BetRound = BetRound.Turn;
                    break;
                case "river":
                    BetRound = BetRound.River;
                    break;
                default:
                    throw new Exception($"Unknown bet round '{value}'.");
            }
        }

        /// <summary>
        /// Maximum time in milliseconds that your bot can have in its time bank.
        /// </summary>
        public int TimeBank { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MyName { get; private set; }
        public string OtherName { get; private set; }

        public void SetMyName(string name)
        {
            MyName = name;
            OtherName = (MyName == PlayerNames[0]) ? PlayerNames[1] : PlayerNames[2];
        }

        public Player Me => Players[MyName];

        public Player Them => Players[OtherName];

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Player> Players { get; }

        public void SetPlayers(string players)
        {
            PlayerNames = players.Split(',');            
            foreach (var playerName in PlayerNames)
            {
                var player = new Player(playerName);
                Players.Add(playerName, player);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OnButtonPlayer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Pot { get; set; }  // Pot that this bot can currently win

        /// <summary>
        /// 
        /// </summary>
        public int AmountToCall { get; set; }  // Amount this bot will bet when calling

        public Table Table { get; }

        public Hand GetMyCards()
        {
            var h = new Hand();
            h.Add(Players[MyName].Cards);
            h.Add(Table.TableCards);
            return h;
        }
    }
}
