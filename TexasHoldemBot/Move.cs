namespace TexasHoldemBot
{
    /// <summary>
    /// Fold 	Fold the hand and forfeit all bets and the pot.
    /// Check   Bet nothing, only possible when the amount to call is 0.
    /// Call    Match the opponents' bet.
    /// Raise   Raise by amount i.The total bet is call amount plus i.
    /// </summary>
    public enum MoveType
    {
        Fold,
        Check,
        Call,
        Raise
    }

    /// <summary>
    /// Stores a move. Can parse it from string and also convert to string
    /// to for output to the engine.
    /// </summary>
    public class Move
    {
        public Move(MoveType moveType, int amount)
        {
            MoveType = moveType;
            Amount = amount;
        }

        public Move(MoveType moveType)
        {
            MoveType = moveType;
        }

        public Move(string input)
        {
            string[] split = input.Split("_".ToCharArray());

            MoveType = MoveTypeFromString(split[0]);

            if (split.Length > 1)
            {
                Amount = int.Parse(split[1]);
            }
        }

        public override string ToString()
        {
            if (MoveType == MoveType.Raise)
            {
                return $"{MoveType}_{Amount}";                
            }

            return MoveType.ToString().ToLower();
        }

        public MoveType MoveType { get; }


        public int Amount { get; }

        public static MoveType MoveTypeFromString(string s)
        {
            switch (s.ToLower())
            {
                case "fold":
                    return MoveType.Fold;
                case "check":
                    return MoveType.Check;
                case "call":
                    return MoveType.Call;
                case "raise":
                    return MoveType.Raise;
            }
            return MoveType.Check;
        }
    }
}
