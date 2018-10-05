namespace TexasHoldemBot.Ai
{

    /// <summary>
    /// 
    /// </summary>
    public enum ChipStatus
    {
        lost,
        losing,
        even,
        winning,
        won
    };
    
    public interface IBotBrain
    {
        void RegisterGameState(GameState state);
        void NewHand();
        Move GetMove();
        void HandComplete(string winner);
    }

    public abstract class BotBrain : IBotBrain
    {
        protected static Move FoldMove { get; } = new Move(MoveType.Fold);
        protected static Move CallMove { get; } = new Move(MoveType.Call);
        protected static Move CheckMove { get; } = new Move(MoveType.Check);

        protected static Move Raise(int amount)
        {
            return new Move(MoveType.Raise, amount);
        }

        protected GameState State { get; private set; }

        public void RegisterGameState(GameState state)
        {
            State = state;
        }

        public abstract void NewHand();
        public abstract Move GetMove();
        public abstract void HandComplete(string winner);

        protected bool AmButton => State.MyName == State.OnButtonPlayer;
        protected int ToCall => State.AmountToCall;
        protected int Chips => State.Me.Chips;
        protected int MinimumBet => State.Table.MinimumBet;

        protected int Pot => State.Pot;

        /// <summary>
        /// Chip status is a quick way to evaluate how well the game is currently going.
        /// It divides the total number of chips into fifths, so if you are in the
        /// 3rd fifth, the game is considered even, even though you could have fewer chips
        /// than the opponent. 
        /// </summary>
        protected ChipStatus ChipStatus
        {
            get
            {
                var p = Chips / (float) (State.Me.Chips + State.Them.Chips);
                if (p < 0.2f)
                    return ChipStatus.lost;
                if (p < 0.4f)
                    return ChipStatus.losing;
                if (p < 0.6f)
                    return ChipStatus.even;
                return p < 0.8f ? ChipStatus.winning : ChipStatus.won;
            }
        }
    }
}