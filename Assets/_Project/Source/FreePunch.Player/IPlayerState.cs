namespace FreePunch.Player
{
    public interface IPlayerState
    {
        void EnterState();
        void UpdateState();
        void ExitState();
    }
}
