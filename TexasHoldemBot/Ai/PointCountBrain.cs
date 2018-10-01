using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasHoldemBot.Ai
{
    public class PointCountBrain : IBotBrain
    {
        public Move GetMove(GameState state)
        {
            if (state.BetRound == BetRound.Flop)
                return GetFlopMove(state);
            if (state.BetRound == BetRound.Turn)
                return GetTurnMove(state);
            if (state.BetRound == BetRound.River)
                return GetRiverMove(state);
            return GetPreFlopMove(state);
        }

        public Move GetPreFlopMove(GameState state)
        {
            return new Move(MoveType.Fold);
        }        

        public Move GetFlopMove(GameState state)
        {
            return new Move(MoveType.Fold);
        }

        public Move GetTurnMove(GameState state)
        {
            return new Move(MoveType.Fold);
        }
        public Move GetRiverMove(GameState state)
        {
            return new Move(MoveType.Fold);
        }

    }
}
