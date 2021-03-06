﻿using TexasHoldemBot.Ai;
using TexasHoldemBot.Poker;

namespace TexasHoldemBot
{
    class Program
    {
        public const string BOT_NAME_S = "EvilHoldemBot";
        public const int MAJOR_VERSION_I = 1;
        public const int MINOR_VERSION_I = 7;

        public static string Version => $"{BOT_NAME_S} v{MAJOR_VERSION_I}.{MINOR_VERSION_I}";

        static void Main(string[] args)
        {            
            Logger.Info(Version);
            IPokerHandEvaluator evaluator = new BrecherHandEvaluator();
            IBotBrain brain = new PointCountBrain(evaluator);
            HoldemBot b = new HoldemBot(brain);
            b.Run();
        }        
    }
}