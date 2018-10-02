namespace TexasHoldemBot.Ai
{
    public interface IBotBrain
    {
        void RegisterGameState(GameState state);
        void NewHand();
        Move GetMove();
        void HandComplete(string winner);
    }

    public abstract class BotBrain : IBotBrain
    {

        public static Move FoldMove { get; } = new Move(MoveType.Fold);
        public static Move CallMove { get; } = new Move(MoveType.Call);
        public static Move CheckMove { get; } = new Move(MoveType.Check);

        public static Move Raise(int amount)
        {
            return new Move(MoveType.Raise, amount);
        }

        public GameState State { get; private set; }

        public void RegisterGameState(GameState state)
        {
            State = state;
        }

        public abstract void NewHand();
        public abstract Move GetMove();
        public abstract void HandComplete(string winner);

        public bool AmButton => State.MyName == State.OnButtonPlayer;
        public int ToCall => State.AmountToCall;
        public int Chips => State.Me.Chips;
        public int MinimumBet => State.Table.MinimumBet;

        public int Pot => State.Pot;
    }
}