using System;
using TexasHoldemBot.Ai;

namespace TexasHoldemBot
{
    public class HoldemBot
    {
        private readonly IBotBrain _brain;
        private readonly GameState _currentState;
        public HoldemBot(IBotBrain brain)
        {
            _brain = brain;
            _currentState = new GameState();
        }

        public void Run()
        {
            string line;

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
                            Move move = _brain.GetMove(_currentState);

                            BotIo.Out.WriteLine(move != null ? move.ToString() : "fold");
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
                        string[] playerNames = value.Split(',');
                        foreach (var playerName in playerNames)
                        {
                            var player = new Player(playerName);
                            _currentState.Players.Add(playerName, player);
                        }

                        break;
                    case "your_bot":
                        _currentState.MyName = value;
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

        /**
     * Parse data about the game given by the engine
     * @param key Type of game data given
     * @param value Value
     */
        private void ParseGameData(string key, string value)
        {
            try
            {
                switch (key)
                {
                    case "round":
                        _currentState.RoundNumber = int.Parse(value);

                        // Reset the table at start of round
                        _currentState.Table.ClearTableCards();
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

        /**
         * Parse data about given player that the engine has sent
         * @param playerName Player name that this data is about
         * @param key Type of player data given
         * @param value Value
         */
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
                        // not stored
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
