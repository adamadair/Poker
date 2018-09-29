namespace TexasHoldemBot.Ai
{
    public interface IBotBrain
    {
        Move GetMove(GameState state);
    }
}