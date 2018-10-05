using System;
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
        private IPokerHandEvaluator _evaluator;

        public PointCountBrain()
        {
            _evaluator = new BrecherHandEvaluator(); // This seems to be the best evaluator for texas holdem
        }
        
        public PointCountBrain(IPokerHandEvaluator evaluator)
        {
            _evaluator = evaluator;
        }
        
        public override void NewHand()
        {
            Logger.Info("***New hand started***");
        }

        public override void HandComplete(string winner)
        {
            Logger.Info($"***Hand Complete, {winner} won ***");
        }

        public override Move GetMove()
        {
            Move m;
            if (State.BetRound == BetRound.Flop)
                m = GetFlopMove();
            else if (State.BetRound == BetRound.Turn)
                m = GetTurnMove();
            else if (State.BetRound == BetRound.River)
                m = GetRiverMove();
            else
                m = GetPreFlopMove();

            Logger.Info($"Move = {m}");
            return m;
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
            if (ToCall == 0)
            {
                return CheckMove;
            }
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
            if (Pot + ToCall + MinimumBet < maxPot)
            {
                Logger.Info("Attempting raise");
                return AttemptRaise(1);
            }

            if (Pot < maxPot)
            {
                Logger.Info("Calling");
                return CallMove;
            }

            Logger.Info("Folding.");
            if (ToCall > 0)
                return FoldMove;
            return CheckMove;
        }

        /// <summary>
        /// Attempt to raise the pot, but call anything.
        /// ONLY use this with really good hands!
        /// </summary>
        /// <param name="betCount">number of times the minimum bet to raise</param>
        /// <returns>The move</returns>
        public Move RaiseOrCallAny(int betCount)
        {
            Logger.Info($"Raise or call any x{betCount}...");
            if (Pot >= MinimumBet * (betCount + 1))
            {
                Logger.Info($"{Pot} too rich...calling");
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
            Logger.Info($"Attempt raise x{betCount}");
            while (betCount > 0)
            {
                if (MinimumBet * betCount + ToCall <= Chips) 
                {
                    Logger.Info("Raise");
                    return Raise(MinimumBet * betCount);
                }
                else
                {
                    Logger.Info($"x{betCount} too rich.");
                }
                --betCount;
            }
            Logger.Info("Fuck!!!! No money, No Raise, Call!");
            return CallMove;
        }

        /// <summary>
        /// Attempt to go all-in.
        /// </summary>
        /// <returns>The move</returns>
        public Move AllIn()
        {
            Logger.Info("!!! A L L   I N   B I T C H E S !!!");
            if (Chips > 0 && Chips >= ToCall)
            {
                return Raise(Chips - ToCall);
            }
            Logger.Info("oops! no money...call :(");
            return CallMove;
        }

        /// <summary>
        /// On the flop the strategy is as follows:
        /// 1) If current hand is high card and there is a bet, check and see 
        /// </summary>
        /// <returns>Move</returns>
        /// <exception cref="Exception">
        /// Exceptions will be thrown if the improper number of cards are
        /// </exception>
        public Move GetFlopMove()
        {
            
            Logger.Info("");
            Logger.Info(":: EVALUATE FLOP ::");
            int points = StartingPoints;
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
            
            Logger.Info($"Current hand: {h}; points = {points}");            
            var probs = BrecherHandEvaluator.FlopProbabilities(State.Me.Cards, State.Table.TableCards);
            LogProbabilityArray(probs, "Flop");
            
            // High card situation 
            if (h == PokerHand.HighCard)
            {                
                if (ToCall == 0)
                {
                    var pairProb = HandOrBetterProbability(PokerHand.OnePair, probs);
                    if (AmButton)
                    {                                                
                        if (points > 20 && pairProb > 0.45f)
                        {
                            Logger.Info($"On button, Raising a high card hand, points = {points}, pair_prob = {pairProb}");
                            return AttemptRaise(1);
                        }                        
                    }
                    else
                    {
                        // the other player checked, so there is a chance to bluff here.
                        if (points > 15 && pairProb > 0.35f)
                        {
                            Logger.Info($"Off button, Raising a high card hand, points = {points}, pair_prob = {pairProb}");
                            return AttemptRaise(1);
                        }                                                
                    }
                    Logger.Info("Check!");
                    return CheckMove;
                }
                else
                {
                    // The opponent raised on the flop while we only hold a high card
                    // hand at the moment. We only stay in if there a good chance to 
                    // get something. 
                    var pairProb = HandOrBetterProbability(PokerHand.OnePair, probs);                                            
                    if (points > 20 && pairProb > 0.45f && ToCall <= MinimumBet)
                    {
                        Logger.Info($"Feeling lucky, calling {ToCall}");
                        return CallMove;
                    }                    
                }
                
                Logger.Info("Fold!");
                return FoldMove;
            } // END HIGH CARD
            
            // ONE PAIR
            if (h == PokerHand.OnePair)
            {
                /*
                 * Pair Strategy:                 
                 */
            }
            if (h == PokerHand.TwoPair)
            {
                /*
                 * Two Pair Strategy:
                 */
                
            }
            if (h == PokerHand.ThreeOfAKind)
            {
                /*
                 * Three of a kind strategy:
                 */                
            }
            if (h > PokerHand.Straight)
            {
                /*
                 * Flush strategy:
                 *   1) If no bet:
                 *      a) Check %25
                 *      b) Raise 1 %50
                 *      c) Raise 2 %25
                 */                
            }                                   
            return FoldMove;
        }
        
        

        public Move GetTurnMove()
        {
            return GetNextMove();
        }
        public Move GetRiverMove()
        {
            return GetNextMove();
        }

        private float HandOrBetterProbability(PokerHand h, float[] probs)
        {
            int i = (int) h;
            float prob = 0.0f;
            while (i < 9)
            {
                prob += probs[i];
                ++i;
            }

            return prob;
        }
        
        private void LogProbabilityArray(float[] p, string turn)
        {
            Logger.Info($"{turn} probabilities:");
            Logger.Info($"HC = {p[0]}; P = {p[1]}; 2P = {p[2]}");
            Logger.Info($"3K = {p[3]}; ST = {p[4]}; F = {p[5]}");
            Logger.Info($"FH = {p[6]}; FK = {p[7]}; SF = {p[8]}");   
        }
        private Move GetNextMove()
        {
            int points = StartingPoints;
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

            if (h == PokerHand.HighCard)
            {
                if (ToCall == 0)
                {
                    return CheckMove;
                }
                if (State.BetRound<BetRound.River && points >= 20 && ToCall <= MinimumBet)
                {
                    return CallMove;
                }
                return FoldMove;
            }
            if (h == PokerHand.OnePair)
            {
                if (ToCall == 0)
                {
                    return CheckMove;
                }

                return CallMove;
            }
            if (h == PokerHand.ThreeOfAKind || h == PokerHand.TwoPair)
            {
                return LimitRaise(4);
            }

            if (h == PokerHand.Straight)
            {
                return RaiseOrCallAny(1);
            }

            if (h == PokerHand.Flush)
            {
                return RaiseOrCallAny(1);
            }

            if (h >= PokerHand.FourOfAKind)
            {
                return AllIn();
            }

            return CallMove;
        }
    }
}
