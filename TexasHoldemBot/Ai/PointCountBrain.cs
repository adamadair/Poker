﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using TexasHoldemBot.Poker;

namespace TexasHoldemBot.Ai
{

    public class PointCountBrain : BotBrain
    {
        public override void NewHand()
        {
            Logger.Info("***New hand started***");
        }

        public override void HandComplete(string winner)
        {
            Logger.Info("***Hand Complete***");
        }

        public override Move GetMove()
        {

            if (State.BetRound == BetRound.Flop)
                return GetFlopMove();
            if (State.BetRound == BetRound.Turn)
                return GetTurnMove();
            if (State.BetRound == BetRound.River)
                return GetRiverMove();
            return GetPreFlopMove();
        }

        public int StartingPoints => HoldemPointSystem.HoleCardPoints(State.Me.Cards.ToArray());

        /*
           The pre-flop strategy based on page 27-30.

         */
        public Move GetPreFlopMove()
        {
            int points = StartingPoints;
            Hand h = new Hand(State.Me.Cards);
            Logger.Info($"Getting preflop move, {h} = {points}");
            if (AmButton)
            {
                // We are on the button spot, so can be slightly 
                // more aggressive.
                if (points == 0)
                {
                    // If we have shit cards, we can still stay in if the other
                    // player hasn't raised, otherwise fold.
                    if (ToCall == 0)
                    {
                        return CheckMove;
                    }
                    return FoldMove;
                }
                if (points >= 60)
                {
                    // This is a really good start that comes only once in 200 hands or so.
                    // Do not waste. We call anything, but at least try to raise.
                    return RaiseOrCallAny(3); // Three times the normal bet. If we push out the other player
                                     // Oh well.
                }
                if (points >= 50)
                {
                    // This is still a very good start, and we should not put up with shit.                    
                    return RaiseOrCallAny(2); // Two times normal bet.
                }

                if (points >= 40)
                {
                    return LimitRaise(2);
                }
                if (points >= 30)
                {
                    return LimitRaise(1);
                }

                if (points >= 20)
                {
                    return LimitCall(2);
                }

                return LimitCall(1);

            }
            // Not button
            if (points == 0)
            {
                return FoldMove; // Fold shit hands. 
            }
            if (points >= 60)
            {
                // This is a really good start that comes only once in 200 hands or so.
                // Do not waste. We call anything, but at least try to raise.
                return RaiseOrCallAny(3); // Three times the normal bet. If we push out the other player
                // Oh well.
            }
            if (points >= 50)
            {
                // This is still a very good start, and we should not put up with shit.                    
                return RaiseOrCallAny(1); // Two times normal bet.
            }

            if (points >= 40)
            {
                return LimitRaise(1);
            }
            if (points >= 30)
            {
                return LimitCall(3);
            }

            if (points >= 10)
            {
                return LimitCall(1);
            }

            return ToCall == 0 ? CheckMove : FoldMove;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="potLimit"></param>
        /// <returns></returns>
        public Move LimitCall(int potLimit)
        {
            var callLimit = MinimumBet * (potLimit + 1);
            Logger.Info($"Limit Call to {callLimit}");
            if (Pot + ToCall <= callLimit)
            {
                Logger.Info("Calling");
                return CallMove;
            }
            Logger.Info("Folding");
            return FoldMove;
        }

        /// <summary>
        /// Bet up to a certain amount, but fold if the pot is too big
        /// </summary>
        /// <param name="betCount"></param>
        /// <returns></returns>
        public Move LimitRaise(int betCount)
        {            
            int maxPot = MinimumBet * (betCount * 2);
            Logger.Info($"LimitRaise limiting pot to {maxPot}");
            if (Pot+ToCall+MinimumBet < maxPot)
            {
                Logger.Info("Attempting raise");
                AttemptRaise(1);
            }
            else if (Pot < maxPot)
            {
                Logger.Info("Calling");
                return CallMove;
            }
            Logger.Info("Folding.");
            return FoldMove;
        }

        /// <summary>
        /// Attempt to raise the pot, but call anything.
        /// ONLY use this with really good hands!
        /// </summary>
        /// <param name="betCount">number of times the minimum bet to raise</param>
        /// <returns>The move</returns>
        public Move RaiseOrCallAny(int betCount)
        {
            Logger.Info("Raise or call any...");
            if (Pot >= MinimumBet * (betCount + 1))
            {
                Logger.Info("...calling");
                return CallMove;
            }
            Logger.Info("...raising");
            return AttemptRaise(betCount);
        }


        /// <summary>
        /// This function attempts to raise the bet. The bet count is the number of times
        /// the minimum bet amount that the pot should be raised. It checks whether
        /// we have enough chips to make the bet, decreasing the bet count until the either
        /// the pot is raised or simply called.
        /// </summary>
        /// <param name="betCount">The number of times the minimum raise to attempt to raise the pot</param>
        /// <returns>The move</returns>
        public Move AttemptRaise(int betCount)
        {
            while (betCount > 0)
            {
                if (MinimumBet * betCount + ToCall <= Chips) 
                {
                    Logger.Info("Raise");
                    return Raise(MinimumBet * betCount);
                }
                --betCount;
            }
            Logger.Info("Call");
            return CallMove;
        }

        /// <summary>
        /// Attempt to go all-in.
        /// </summary>
        /// <returns>The move</returns>
        public Move AllIn()
        {
            if (Chips>0 && Chips >= ToCall)
            {
                return Raise(Chips - ToCall);
            }

            return CallMove;
        }

        public Move GetFlopMove()
        {

            return ToCall == 0 ? CheckMove : FoldMove;
        }

        public Move GetTurnMove()
        {
            return ToCall == 0 ? CheckMove : FoldMove;
        }
        public Move GetRiverMove()
        {
            return ToCall == 0 ? CheckMove : FoldMove;
        }

    }
}
