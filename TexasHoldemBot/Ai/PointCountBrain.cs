using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

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

        public int StartingPoints => HoldemPointSystem.HoleCardPoints(State.Me.GetHand().ToArray());

        /*
           The pre-flop strategy based on page 27-30.

         */
        public Move GetPreFlopMove()
        {
            int points = StartingPoints;
            if (AmButton)
            {
                if (points == 0)
                {
                    if (ToCall == 0)
                    {
                        return CheckMove;
                    }
                    return FoldMove;
                }
                if (points >= 60)
                {
                    // This is a really good hand that comes only once in 200 hands or so.
                    // Do not waste. We call anything, but at least try to raise.
                }

            }
            else
            {
                if (points == 0)
                {
                    return FoldMove; // Fold shit hands. 
                }
                
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
            //todo fix this.
            return CallMove;
        }

        /// <summary>
        /// Bet up to a certain amount, but fold if the pot is too big
        /// </summary>
        /// <param name="betCount"></param>
        /// <returns></returns>
        public Move LimitRaise(int betCount)
        {
            //todo fix this.
            return CallMove;
        }

        /// <summary>
        /// Attempt to raise the pot, but call anything.
        /// ONLY use this with really good hands!
        /// </summary>
        /// <param name="betCount">number of times the minimum bet to raise</param>
        /// <returns>The move</returns>
        public Move RaiseOrCallAny(int betCount)
        {
            if (Pot >= MinimumBet * (betCount + 1))
            {
                return CallMove;
            }
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
                    return Raise(MinimumBet * betCount);
                }
                --betCount;
            }
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
