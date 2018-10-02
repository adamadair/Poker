using System;
using TexasHoldemBot.Ai;

namespace TexasHoldemBot
{
    /// <summary>
    /// HoldemBot is responsible for parsing data from the game process, retrieving
    /// moves from the AI brain, and sending the move responses to the game process.
    /// </summary>
    public class HoldemBot
    {
        private readonly IBotBrain _brain;
        private readonly GameState _currentState;

        /// <summary>
        /// The bot requires a brain to be instantiated. 
        /// </summary>
        /// <param name="brain"></param>
        public HoldemBot(IBotBrain brain)
        {
            _brain = brain;
            _currentState = new GameState();
        }

        public GameState GetCurrentState()
        {
            return _currentState;
        }

        private void ClearAllCards()
        {
            _currentState.Table.ClearTableCards();
            _currentState.Me.ClearHand();
            _currentState.Them.ClearHand();
        }

        /// <summary>
        /// Run the bot.
        /// </summary>
        public void Run()
        {
            string line;
            _brain.RegisterGameState(_currentState);
            while ((line = BotIo.In.ReadLine()) != null)
            {
                if (line.Length == 0) continue;
                string[] parts = line.Split(' ');
                switch (parts[0])
                {
                    case "settings":
                        ParseSettings(parts[1], parts[2]);
                        break;
                    case "update":
                        if (parts[1] == "game")
                        {
                            ParseGameData(parts[2], parts[3]);
                        }
                        else
                        {
                            ParsePlayerData(parts[1], parts[2], parts[3]);
                        }

                        break;
                    case "action":
                        if (parts[1] == "move")
                        {
                            _currentState.TimeBank = (int.Parse(parts[2]));
                            try
                            {
                                Move move = _brain.GetMove();

                                BotIo.Out.WriteLine(move != null ? move.ToString() : (_currentState.AmountToCall > 0 ? "fold" : "check"));
                            }
                            catch (Exception ex)
                            {
                                Logger.Error(ex.Message,ex);
                                BotIo.Out.WriteLine(_currentState.AmountToCall > 0 ? "fold" : "check");
                            }
                            
                        }

                        break;
                    case "quit":
                        Logger.Info("Quitting");
                        return;
                }
            }
        }

        /// <summary>
        /// Parses all the game settings given by the engine
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void ParseSettings(string key, string value)
        {
            try
            {
                switch (key)
                {
                    case "timebank":
                        var time = Convert.ToInt32(value);
                        _currentState.MaxTimeBank = time;
                        _currentState.TimeBank = time;
                        break;
                    case "time_per_move":
                        _currentState.TimePerMove = Convert.ToInt32(value);
                        break;
                    case "player_names":
                        _currentState.SetPlayers(value);
                        break;
                    case "your_bot":
                        _currentState.SetMyName(value);
                        break;
                    case "initial_stack":
                        foreach (Player player in _currentState.Players.Values)
                        {
                            player.Chips = int.Parse(value);
                        }

                        break;
                    case "initial_big_blind":
                        _currentState.InitialBigBlind = Convert.ToInt32(value);
                        break;
                    case "hands_per_blind_level":
                        _currentState.HandsPerBlindLevel = Convert.ToInt32(value);
                        break;
                    default:
                        Logger.Error($"Cannot parse settings input with key '{key}'");
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Cannot parse settings value '{value}' for key '{key}'", e);
            }
        }

        /// <summary>
        /// Parse data about the game given by the engine
        /// </summary>
        /// <param name="key">Type of game data given</param>
        /// <param name="value">Value</param>
        private void ParseGameData(string key, string value)
        {
            try
            {
                switch (key)
                {
                    case "round":
                        _currentState.RoundNumber = int.Parse(value);
                        // Clear all the cards at start of round
                        ClearAllCards();
                        break;
                    case "bet_round":
                        _currentState.SetBetRound(value);
                        break;
                    case "small_blind":
                        _currentState.Table.SetSmallBlind(int.Parse(value));
                        break;
                    case "big_blind":
                        _currentState.Table.SetBigBlind(int.Parse(value));
                        break;
                    case "on_button":
                        _currentState.OnButtonPlayer = value;
                        break;
                    case "table":
                        _currentState.Table.ParseTableCards(value);
                        break;
                    default:
                        Logger.Error($"Cannot parse game data input with key '{key}'");
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Cannot parse game data value '{value}' for key '{key}': {e.Message}", e);
            }
        }

        /// <summary>
        /// Parse data about given player that the engine has sent
        /// </summary>
        /// <param name="playerName">Player name that this data is about</param>
        /// <param name="key">Type of player data given</param>
        /// <param name="value">Value</param>
        private void ParsePlayerData(string playerName, string key, string value)
        {
            Player player = _currentState.Players[playerName];

            if (player == null)
            {
                Logger.Error($"Could not find player with name {playerName}");
                return;
            }

            try
            {
                switch (key)
                {
                    case "hand":
                        player.ParseHand(value);
                        break;
                    case "bet":
                        player.Bet = (int.Parse(value));
                        break;
                    case "chips":
                        player.Chips = (int.Parse(value));
                        break;
                    case "pot":
                        _currentState.Pot = (int.Parse(value));
                        break;
                    case "amount_to_call":
                        _currentState.AmountToCall = (int.Parse(value));
                        break;
                    case "move":
                        player.Move = (new Move(value));
                        break;
                    case "wins":
                        // There is a value that goes along with this, but
                        // at this point I don't want to do anything with it.
                        // Just increment a counter for the winning player.
                        player.Wins += 1;
                        try
                        {
                            // However, we should let the brain know that the 
                            // round is over.
                            _brain.HandComplete(playerName);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex.Message, ex);
                        }
                        break;
                    default:
                        Logger.Error($"Cannot parse '{playerName}' data input with key '{key}'");
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Cannot parse {playerName} data value '{value}' for key '{key}'", e);
            }
        }
    }
}
