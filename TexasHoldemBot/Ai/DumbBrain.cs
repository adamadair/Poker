using System;
using TexasHoldemBot.Poker;

namespace TexasHoldemBot.Ai
{
    public class DumbBrain : BotBrain
    {
        private readonly IPokerHandEvaluator _evaluator;

        public DumbBrain(IPokerHandEvaluator e)
        {
            _evaluator = e;
        }

        public override void NewHand()
        {
            
        }

        public override Move GetMove()
        {
            var cardToEvaluate = State.GetMyCards();
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
                if (State.BetRound == BetRound.River)
                {  // Check if we're on the river with high card
                    return new Move(MoveType.Check);
                }
                return new Move(MoveType.Call);
            }
            if (h < PokerHand.Straight)
            {  // We have pair, two pair, or three of a kind
                return new Move(MoveType.Call);
            }
            return new Move(MoveType.Raise, State.Table.BigBlind * 2);

        }

        public override void HandComplete(string winner)
        {
            
        }
    }
}
