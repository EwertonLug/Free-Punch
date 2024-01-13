namespace FreePunch.Player
{
    public interface IPlayerState
    {
        public string Name { get; }
        void EnterState();
        void UpdateState();
        void ExitState();
    }
}
