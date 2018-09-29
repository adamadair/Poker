using System;
using TexasHoldemBot.Poker;

namespace TexasHoldemBot.Ai
{
    public class DumbBrain : IBotBrain
    {
        private readonly IPokerHandEvaluator _evaluator;

        public DumbBrain(IPokerHandEvaluator e)
        {
            _evaluator = e;
        }

        public Move GetMove(GameState state)
        {
            var cardToEvaluate = state.GetMyCards();
            if (cardToEvaluate.Cards.Length < 2)
            {
                throw new Exception("Bad number of cards");
            }

            var h = PokerHand.HighCard;
            if (cardToEvaluate.Cards.Length == 2)
            {
                if (cardToEvaluate.Cards[0].Value == cardToEvaluate.Cards[1].Value)
                    h = PokerHand.OnePair;
            }
            else
            {
                h = _evaluator.Evaluate(cardToEvaluate);
            }

            if (h < PokerHand.OnePair)
            {  // We only have a high card
                if (state.BetRound == BetRound.River )
                {  // Check if we're on the river with high card
                    return new Move(MoveType.Check);
                }
                return new Move(MoveType.Call);
            }

            if (h < PokerHand.Straight)
            {  // We have pair, two pair, or three of a kind
                return new Move(MoveType.Call);
            }

            return new Move(MoveType.Raise, state.Table.BigBlind * 2);
        }
    }
}
